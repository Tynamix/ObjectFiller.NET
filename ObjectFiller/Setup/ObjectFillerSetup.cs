using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tynamix.ObjectFiller
{
    public class ObjectFillerSetup
    {
        public ObjectFillerSetup()
        {
            ListMinCount = 1;
            ListMaxCount = 25;
            DictionaryKeyMinCount = 1;
            DictionaryKeyMaxCount = 10;
            TypeToRandomFunc = new Dictionary<Type, Func<object>>();
            PropertyToRandomFunc = new Dictionary<PropertyInfo, Func<object>>();
            PropertiesToIgnore = new List<PropertyInfo>();
            PropertyOrder = new Dictionary<PropertyInfo, At>();
            TypesToIgnore = new List<Type>();
            PropertiesWritePrivateSetter = new List<PropertyInfo>();

            InterfaceToImplementation = new Dictionary<Type, Type>();

            SetDefaultRandomizer();
        }

        private void SetDefaultRandomizer()
        {
            var mnemonic = new MnemonicString(20);
            var doublePlugin = new DoubleRange();
            var dateTimeRandomizer = new DateTimeRange(new System.DateTime(1970, 1, 1));
            TypeToRandomFunc[typeof(string)] = mnemonic.GetValue;
            TypeToRandomFunc[typeof(bool)] = () => Random.Next(0, 2) == 1;
            TypeToRandomFunc[typeof(short)] = () => (short)Random.Next(-32767, 32767);
            TypeToRandomFunc[typeof(int)] = () => Random.Next();
            TypeToRandomFunc[typeof(int?)] = () => Random.Next();
            TypeToRandomFunc[typeof(long)] = () => Random.Next();
            TypeToRandomFunc[typeof(long?)] = () => Random.Next();
            TypeToRandomFunc[typeof(double)] = () => doublePlugin.GetValue();
            TypeToRandomFunc[typeof(double?)] = () => doublePlugin.GetValue();
            TypeToRandomFunc[typeof(decimal)] = () => doublePlugin.GetValue();
            TypeToRandomFunc[typeof(Guid)] = () => Guid.NewGuid();
            TypeToRandomFunc[typeof(Guid?)] = () => Guid.NewGuid();
            TypeToRandomFunc[typeof(System.DateTime)] = () => dateTimeRandomizer.GetValue();
            TypeToRandomFunc[typeof(System.DateTime?)] = () => dateTimeRandomizer.GetValue();

        }

        /// <summary>
        /// Defines in which order the properties get handled.
        /// </summary>
        public Dictionary<PropertyInfo, At> PropertyOrder { get; private set; }

        /// <summary>
        /// Contains the Type to random data generator func
        /// </summary>
        public Dictionary<Type, Func<object>> TypeToRandomFunc { get; private set; }

        /// <summary>
        /// Contains the Property to random data generator func
        /// </summary>
        public Dictionary<PropertyInfo, Func<object>> PropertyToRandomFunc { get; private set; }

        /// <summary>
        /// Contains the type of interface with the corresponding implementation
        /// </summary>
        public Dictionary<Type, Type> InterfaceToImplementation { get; private set; }

        /// <summary>
        /// List with all properties which will be ignored while generating test data
        /// </summary>
        public List<PropertyInfo> PropertiesToIgnore { get; private set; }

        /// <summary>
        /// All types which will be ignored completly
        /// </summary>
        public List<Type> TypesToIgnore { get; private set; }

        /// <summary>
        /// Minimum count of list items which will be generated 
        /// </summary>
        public int ListMinCount { get; set; }

        /// <summary>
        /// Maximum count of list items which will be generated 
        /// </summary>
        public int ListMaxCount { get; set; }

        /// <summary>
        /// Minimum count of key items within a dictionary which will be generated 
        /// </summary>
        public int DictionaryKeyMinCount { get; set; }

        /// <summary>
        /// Maximum count of key items within a dictionary which will be generated 
        /// </summary>
        public int DictionaryKeyMaxCount { get; set; }

        /// <summary>
        /// Interface Mocker for interface generation
        /// </summary>
        public IInterfaceMocker InterfaceMocker { get; set; }

        /// <summary>
        /// Properties for which it is allowed to write the private setter
        /// </summary>
        public List<PropertyInfo> PropertiesWritePrivateSetter { get; private set; }
    }
}
