namespace Tynamix.ObjectFiller
{
    using System;
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
        private static readonly FillerSetupItem setup;

        /// <summary>
        /// Initializes static members of the <see cref="Randomizer{T}"/> class.
        /// </summary>
        static Randomizer()
        {
            setup = new FillerSetupItem();
        }

        /// <summary>
        /// Creates a random value of type <see cref="T"/>.
        /// </summary>
        /// <returns>A value of type <see cref="T"/></returns>
        public static T Create()
        {
            Type targetType = typeof(T);
            if (!setup.TypeToRandomFunc.ContainsKey(targetType))
            {
                if (targetType.IsClass)
                {
                    var fillerType = typeof(Filler<>).MakeGenericType(typeof(T));
                    var oFiller = Activator.CreateInstance(fillerType);
                    var methodInfo = oFiller.GetType().GetMethods().Single(x => !x.GetParameters().Any() && x.Name == "Create");

                    try
                    {
                        return (T)methodInfo.Invoke(oFiller, null);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("The type " + typeof(T).FullName + " needs additional information to get created. " +
                            "Please use the Filler class and call \"Setup\" to create a setup for that type. See Innerexception for more details.", ex);
                    }
                }

                return default(T);
            }

            return (T)setup.TypeToRandomFunc[typeof(T)]();
        }

        /// <summary>
        /// Creates a random value of the target type. It will use a <see cref="IRandomizerPlugin{T}"/> for that
        /// </summary>
        /// <param name="randomizerPlugin"><see cref="IRandomizerPlugin{T}"/> to use</param>
        /// <returns>A random value of type <see cref="T"/></returns>
        public static T Create(IRandomizerPlugin<T> randomizerPlugin)
        {
            return randomizerPlugin.GetValue();
        }
    }
}