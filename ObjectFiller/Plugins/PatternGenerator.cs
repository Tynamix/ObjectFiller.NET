using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tynamix.ObjectFiller.Plugins
{
	/// <summary>
	/// Creates values based on a pattern. <para/>
	/// <para/>
	/// Character patterns:<para/>
	/// <list>
	/// <item><description>{CharClass} generates exactly 1 character, e.g. {a}.</description></item>
	/// <item><description>{CharClass:Count} generates exactly Count characters, e.g. {A:3}.</description></item>
	/// <item><description>{CharClass:MinCount-MaxCount} generates between MinCount and MaxCount character, e.g. {a:3-6}.</description></item>
	/// </list>
	/// The character patterns can refer to these character classes: <para/>
	/// <list type="bullet">
	/// <item><description>a: lower-case ascii character, range is 'a' to 'a'</description></item>
	/// <item><description>A: upper-case ascii character, range is 'a' to 'a'</description></item>
	/// <item><description>N: numbers from '0' to '9'</description></item>
	/// <item><description>X: hexadecimal digit from '0' to 'F'</description></item>
	/// </list>
	/// Counter patterns:
	/// <list>
	/// <item><description>{C} generates numbers, starting with 1, incremented by 1</description></item>
	/// <item><description>{C:StartValue} generates numbers, starting with StartValue, incremented by 1</description></item>
	/// <item><description>{C:StartValue,Increment} generates numbers, starting with StartValue, incremented by Increment</description></item>
	/// </list>
	/// </summary>
	public class PatternGenerator : IRandomizerPlugin<string>
	{
		#region Fields

		private static readonly Random _random = new Random();

		// Static list of all known factories. 
		// Will be used to create the concrete expression handlers per instance.
		// Important - do not add the default expression handler here, will be done in code instead,
		private static readonly List<Func<string, IExpressionGenerator>> _expressionGeneratorFactories =
			new List<Func<string, IExpressionGenerator>>
				{
					CharacterExpressionGenerator.TryCreateInstance,
					CounterExpressionGenerator.TryCreateInstanceInstance,
				};

		// The user-provided pattern for this instance.
		private readonly string _pattern;

		// All expression handlers, which are needed to generate a value according the the "_pattern"
		private readonly List<IExpressionGenerator> _expressionGenerators = new List<IExpressionGenerator>();

		#endregion

		#region PatternGenerator class implementation

		/// <summary>
		/// Creates values based on a pattern. <para/>
		/// <para/>
		/// Character patterns:<para/>
		/// <list>
		/// <item><description>{CharClass} generates exactly 1 character, e.g. {a}.</description></item>
		/// <item><description>{CharClass:Count} generates exactly Count characters, e.g. {A:3}.</description></item>
		/// <item><description>{CharClass:MinCount-MaxCount} generates between MinCount and MaxCount character, e.g. {a:3-6}.</description></item>
		/// </list>
		/// The character patterns can refer to these character classes: <para/>
		/// <list type="bullet">
		/// <item><description>a: lower-case ascii character, range is 'a' to 'a'</description></item>
		/// <item><description>A: upper-case ascii character, range is 'a' to 'a'</description></item>
		/// <item><description>N: numbers from '0' to '9'</description></item>
		/// <item><description>X: hexadecimal digit from '0' to 'F'</description></item>
		/// </list>
		/// Counter patterns:
		/// <list>
		/// <item><description>{C} generates numbers, starting with 1, incremented by 1</description></item>
		/// <item><description>{C:StartValue} generates numbers, starting with StartValue, incremented by 1</description></item>
		/// <item><description>{C:StartValue,Increment} generates numbers, starting with StartValue, incremented by Increment</description></item>
		/// </list>
		/// </summary>
		public PatternGenerator(string pattern)
		{
			_pattern = pattern ?? string.Empty;
			_expressionGenerators.AddRange(CreateExpressionGenerators(pattern));
		}

		/// <summary>
		/// Allows integrating new pattern expression generators.
		/// </summary>
		/// <example>
		/// <![CDATA[
		/// PatternGenerator.ExpressionGeneratorFactories.Add( 
		///		(expression) => 
		///			{
		///				if (expression == "{U:fr}")
		///					return new FrenchUnicodeExpressionGenerator(expression);
		///				else 
		///					return null;
		///			});
		///	]]>
		/// </example>
		public static IList<Func<string, IExpressionGenerator>> ExpressionGeneratorFactories
		{
			get { return _expressionGeneratorFactories; }
		}

		/// <summary>
		/// Gets a random string according to the specified pattern.
		/// </summary>
		public string GetValue()
		{
			var sb = new StringBuilder(_pattern.Length * 3);

			foreach (var expressionGenerator in _expressionGenerators)
			{
				expressionGenerator.AppendNextValue(sb);
			}

			return sb.ToString();
		}

		/// <summary>
		/// Interface for a concrete pattern expression generator.
		/// Can be used to add custom generators.
		/// See also <seealso cref="ExpressionGeneratorFactories"/>.
		/// </summary>
		public interface IExpressionGenerator
		{
			void AppendNextValue(StringBuilder sb);
		}

		/// <summary>
		/// Parses the given pattern and returns a collection of generators.
		/// </summary>
		private static IEnumerable<IExpressionGenerator> CreateExpressionGenerators(string pattern)
		{
			// All patterns must be defined like {A*}.
			// We split the pattern into expressions and create one ExpressionHandler per expression.
			// The split operation may return a few empty expression strings, they should be skipped.
			var expressions = Regex
				.Split(pattern, "({[A-Z].*?})")
				.Where(s => s.Length > 0);
			
			foreach (var expression in expressions)
			{
				var generator = _expressionGeneratorFactories
					.Select(factory => factory.Invoke(expression))
					.FirstOrDefault(g => g != null);

				if (generator == null)
					generator = DefaultExpressionGenerator.TryCreateInstance(expression);

				if (generator != null)
					yield return generator;
			}
		}

		#endregion

		private class DefaultExpressionGenerator : IExpressionGenerator
		{
			public static IExpressionGenerator TryCreateInstance(string expression)
			{
				if (string.IsNullOrEmpty(expression)) return null;
				return new DefaultExpressionGenerator(expression);
			}

			private readonly string _expression;

			[DebuggerStepThrough]
			private DefaultExpressionGenerator(string expression)
			{
				_expression = expression;
			}

			public void AppendNextValue(StringBuilder sb)
			{
				// simply copy the expression into the output
				sb.Append(_expression);
			}

			[DebuggerStepThrough]
			public override string ToString()
			{
				return GetType().Name + ":  " + _expression;
			}
		}

		private class CharacterExpressionGenerator : IExpressionGenerator
		{
			public static IExpressionGenerator TryCreateInstance(string expression)
			{
				if (string.IsNullOrEmpty(expression)) return null;

				// {CharClass} generates exactly 1 random character
				Match m = Regex.Match(expression, "^{[aANX]}$");
				if (m.Success)
				{
					return new CharacterExpressionGenerator(expression, expression[1], 1, 1);
				}

				// {CharClass:Count} generates exactly Count random characters
				m = Regex.Match(expression, @"^{[aANX]:(?<Count>\d+)}$");
				if (m.Success)
				{
					var charClass = expression[1];
					var count = int.Parse(m.Groups["Count"].Value);
					return new CharacterExpressionGenerator(expression, charClass, count, count);
				}

				// {CharClass:MinCount-MaxCount} generates between MinCount and MaxCount
				m = Regex.Match(expression, @"^{[aANX]:(?<MinCount>\d+)-(?<MaxCount>\d+)}$");
				if (m.Success)
				{
					var charClass = expression[1];
					int minCount = int.Parse(m.Groups["MinCount"].Value);
					int maxCount = int.Parse(m.Groups["MaxCount"].Value);
					return new CharacterExpressionGenerator(expression, charClass, minCount, maxCount); ;
				}

				return null;
			}

			private static readonly Random _random = new Random();
			private readonly string _expression;
			private readonly char _charClass;
			private readonly int _minCount;
			private readonly int _maxCount;

			[DebuggerStepThrough]
			private CharacterExpressionGenerator(string expression, char charClass, int minCount, int maxCount)
			{
				_expression = expression;
				_charClass = charClass;
				_minCount = minCount;
				_maxCount = maxCount;
			}

			public void AppendNextValue(StringBuilder sb)
			{
				var count = _random.Next(_minCount, _maxCount + 1);
				for (int n = 0; n < count; n++)
				{
					sb.Append(NextChar(_charClass));
				}
			}

			#region Concrete char generator functions

			private static char NextChar(char charClass)
			{
				if (charClass == 'a') return NextLowerCaseChar();
				if (charClass == 'A') return NextUpperCaseChar();
				if (charClass == 'N') return NextDecimalDigit();
				if (charClass == 'X') return NextHexDigit();
				throw new InvalidOperationException();
			}

			private static char NextUpperCaseChar()
			{
				return (char)_random.Next('A', 'Z' + 1);
			}

			private static char NextLowerCaseChar()
			{
				return (char)_random.Next('a', 'z' + 1);
			}

			private static char NextDecimalDigit()
			{
				return (char)_random.Next('0', '9' + 1);
			}

			private static char NextHexDigit()
			{
				var c = (char)_random.Next('0', '9' + 6 + 1);
				return (char)((c > '9') ? (c + 7) : c);
			}

			#endregion

			[DebuggerStepThrough]
			public override string ToString()
			{
				return GetType().Name + ":  " + _expression;
			}
		}

		private class CounterExpressionGenerator : IExpressionGenerator
		{
			public static IExpressionGenerator TryCreateInstanceInstance(string expression)
			{
				// {C} generate numbers, starting with 1, incremented by 1
				Match m = Regex.Match(expression, "^{C}$");
				if (m.Success)
				{
					return new CounterExpressionGenerator(expression, 1, 1);
				}

				// {C:StartValue} generates numbers, starting with StartValue, incremented by 1
				m = Regex.Match(expression, @"^{C:(?<StartValue>[+-]?\d+)}$");
				if (m.Success)
				{
					var startValue = int.Parse(m.Groups["StartValue"].Value);
					return new CounterExpressionGenerator(expression, startValue, 1);
				}

				// {C:StartValue,Increment} generates numbers, starting with StartValue, incremented by Increment
				m = Regex.Match(expression, @"^{C:(?<StartValue>-?\d+),(?<Increment>[+-]?\d+)}$");
				if (m.Success)
				{
					var startValue = int.Parse(m.Groups["StartValue"].Value);
					var increment = int.Parse(m.Groups["Increment"].Value); ;
					return new CounterExpressionGenerator(expression, startValue, increment);
				}

				return null;
			}

			private readonly string _expression;
			private readonly int _increment;
			private int _value;

			[DebuggerStepThrough]
			private CounterExpressionGenerator(string expression, int startValue, int increment)
			{
				_expression = expression;
				_value = startValue;
				_increment = increment;
			}

			public void AppendNextValue(StringBuilder sb)
			{
				sb.Append(_value);
				_value += _increment;
			}

			[DebuggerStepThrough]
			public override string ToString()
			{
				return GetType().Name + ":  " + _expression;
			}
		}

	}
}
