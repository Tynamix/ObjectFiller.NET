// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Randomizer.cs" company="Tynamix">
//   ©2015 by Roman Köhler
// </copyright>
// <summary>
//   This class is a easy way to get random values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class is a easy way to get random values. 
    /// </summary>
    /// <typeparam name="T">Target type of whom the random value shall been generated</typeparam>
    public static class Randomizer<T>
    {
        /// <summary>
        /// The default setup item
        /// </summary>
        private static readonly FillerSetupItem Setup;

        /// <summary>
        /// Initializes static members of the <see cref="Randomizer{T}"/> class.
        /// </summary>
        static Randomizer()
        {
            Setup = new FillerSetupItem();
        }

        /// <summary>
        /// Creates a random value of type <see cref="T"/>.
        /// </summary>
        /// <returns>A value of type <see cref="T"/></returns>
        public static T Create()
        {
            return Create(null as FillerSetup);
        }

        /// <summary>
        /// Creates a set of random items of the given type.
        /// </summary>
        /// <param name="amount">Amount of items created.</param>
        /// <returns>Set of random items of the given type.</returns>
        public static IEnumerable<T> Create(int amount)
        {
            return Create(amount, Create);
        }

        /// <summary>
        /// Creates the specified amount of elements using the given factory.
        /// </summary>
        /// <param name="amount">The amount to create.</param>
        /// <param name="factory">The factory which provides the instance to add.</param>
        /// <returns>Set of items created by the factory.</returns>
        public static IEnumerable<T> Create(int amount, Func<T> factory)
        {
            var resultSet = new List<T>();
            for (int i = 0; i < amount; i++)
            {
                resultSet.Add(factory());
            }

            return resultSet;
        }

        /// <summary>
        /// Creates a set of random items of the given type and will use a <see cref="IRandomizerPlugin{T}"/> for that.
        /// </summary>
        /// <param name="randomizerPlugin">Plugin to use.</param>
        /// <param name="amount">Amount of items created.</param>
        /// <returns>Set of random items of the given type.</returns>
        public static IEnumerable<T> Create(IRandomizerPlugin<T> randomizerPlugin, int amount)
        {
            return Create(amount, () => Create(randomizerPlugin));
        }

        /// <summary>
        /// Creates a set of random items of the given type and will use a <see cref="FillerSetup"/> for that.
        /// </summary>
        /// <param name="setup">Setup to use.</param>
        /// <param name="amount">Amount of items created.</param>
        /// <returns>Set of random items of the given type.</returns>
        public static IEnumerable<T> Create(FillerSetup setup, int amount)
        {
            return Create(amount, () => Create(setup));
        }

        /// <summary>
        /// Creates a random value of the target type. It will use a <see cref="IRandomizerPlugin{T}"/> for that
        /// </summary>
        /// <param name="randomizerPlugin"><see cref="IRandomizerPlugin{T}"/> to use</param>
        /// <returns>A random value of type <see cref="T"/></returns>
        public static T Create(IRandomizerPlugin<T> randomizerPlugin)
        {
            Setup.TypeToRandomFunc[typeof(T)] = () => randomizerPlugin.GetValue();
            return randomizerPlugin.GetValue();
        }

        /// <summary>
        /// Creates a value base on a filler setup
        /// </summary>
        /// <param name="setup">Setup for the objectfiller</param>
        /// <returns>Created value</returns>
        public static T Create(FillerSetup setup)
        {
            var creationMethod = CreateFactoryMethod(setup);

            T result;
            try
            {
                result = creationMethod();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "The type " + typeof(T).FullName + " needs additional information to get created. "
                    + "Please use the Filler class and call \"Setup\" to create a setup for that type. See Innerexception for more details.",
                    ex);
            }

            return result;
        }

        /// <summary>
        /// Creates a factory method for the given type.
        /// </summary>
        /// <param name="setup">The setup which is used for the type.</param>
        /// <returns>A function with which the given type can be instantiated.</returns>
        internal static Func<T> CreateFactoryMethod(FillerSetup setup)
        {
            Type targetType = typeof(T);
            if (!Setup.TypeToRandomFunc.ContainsKey(targetType))
            {

                if (targetType.IsClass())
                {
                    var fillerType = typeof(Filler<>).MakeGenericType(typeof(T));
                    var objectFiller = Activator.CreateInstance(fillerType);

                    if (setup != null)
                    {
                        var setupMethod = objectFiller.GetType().GetMethods()
                                                      .Where(x => x.Name == "Setup" && x.GetParameters().Length == 1)
                                                      .Single(x => x.GetParameters().First().ParameterType == typeof(FillerSetup));

                        setupMethod.Invoke(objectFiller, new[] { setup });
                    }

                    var createMethod = objectFiller.GetType().GetMethods().Single(x => !x.GetParameters().Any() && x.Name == "Create");
                    return () => (T)createMethod.Invoke(objectFiller, null);
                }

                return () => default(T);
            }

            return () => (T)Setup.TypeToRandomFunc[typeof(T)]();
        }
    }
}