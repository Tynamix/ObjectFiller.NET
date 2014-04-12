using System;
using System.Collections.Generic;

namespace ObjectFiller
{
    public static class SetupManager
    {
        private static ObjectFillerSetup _mainSetup;
        private static Dictionary<Type, ObjectFillerSetup> _typeToSetup;


        static SetupManager()
        {
            Clear();
        }



        public static ObjectFillerSetup GetFor<TTargetObject>()
            where TTargetObject : class
        {
            return GetFor(typeof(TTargetObject));
        }

        public static ObjectFillerSetup GetFor(Type targetType)
        {
            if (_typeToSetup.ContainsKey(targetType))
            {
                return _typeToSetup[targetType];
            }

            return _mainSetup;
        }


        public static void SetNewFor<TTargetObject>(bool overrideSettings)
            where TTargetObject : class
        {
            _typeToSetup[typeof(TTargetObject)] = overrideSettings ? new ObjectFillerSetup() : _mainSetup;
        }

        public static void SetMain(ObjectFillerSetup setup)
        {
            _mainSetup = setup;
        }

        public static void Clear()
        {
            _mainSetup = new ObjectFillerSetup();
            _typeToSetup = new Dictionary<Type, ObjectFillerSetup>();
        }
    }
}
