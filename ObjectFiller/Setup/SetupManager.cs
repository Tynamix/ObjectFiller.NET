using System;
using System.Collections.Generic;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Responsible to get the right <see cref="ObjectFillerSetup"/> for a given type.
    /// </summary>
    internal static class SetupManager
    {
        private static ObjectFillerSetup _mainSetup;
        private static Dictionary<Type, ObjectFillerSetup> _typeToSetup;

        /// <summary>
        /// static ctor
        /// </summary>
        static SetupManager()
        {
            Clear();
        }

        /// <summary>
        /// Gets the <see cref="ObjectFillerSetup"/> for a given type
        /// </summary>
        /// <typeparam name="TTargetObject">Type for which a <see cref="ObjectFillerSetup"/> will be get</typeparam>
        /// <returns><see cref="ObjectFillerSetup"/> for type <see cref="TTargetObject"/></returns>
        internal static ObjectFillerSetup GetFor<TTargetObject>()
            where TTargetObject : class
        {
            return GetFor(typeof(TTargetObject));
        }

        /// <summary>
        /// Gets the <see cref="ObjectFillerSetup"/> for a given type
        /// </summary>
        /// <param name="targetType">Type for which a <see cref="ObjectFillerSetup"/> will be get</param>
        /// <returns><see cref="ObjectFillerSetup"/> for type <see cref="targetType"/></returns>
        internal static ObjectFillerSetup GetFor(Type targetType)
        {
            if (_typeToSetup.ContainsKey(targetType))
            {
                return _typeToSetup[targetType];
            }

            return _mainSetup;
        }

        /// <summary>
        /// Sets a new <see cref="ObjectFillerSetup"/> for the given <see cref="TTargetObject"/>
        /// </summary>
        /// <typeparam name="TTargetObject">Type of target object for which a new <see cref="ObjectFillerSetup"/> will be set.</typeparam>
        /// <param name="useDefaultSettings">FALSE if the target object will take the settings of the parent object</param>
        internal static void SetNewFor<TTargetObject>(bool useDefaultSettings)
            where TTargetObject : class
        {
            _typeToSetup[typeof(TTargetObject)] = useDefaultSettings ? new ObjectFillerSetup() : _mainSetup;
        }

        /// <summary>
        /// Set the main <see cref="ObjectFillerSetup"/>. This will be the root setup.
        /// </summary>
        /// <param name="setup">Main setup</param>
        internal static void SetMain(ObjectFillerSetup setup)
        {
            _mainSetup = setup;
        }

        /// <summary>
        /// Clears all the settings which was made.
        /// </summary>
        internal static void Clear()
        {
            _mainSetup = new ObjectFillerSetup();
            _typeToSetup = new Dictionary<Type, ObjectFillerSetup>();
        }
    }
}
