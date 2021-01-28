// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FillerSetupItem.cs" company="Tynamix">
//   © 2015 by Roman Köhler
// </copyright>
// <summary>
//   The filler setup item contains the setup for the object filler.
//   The setup can be made per type, property and so on
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;

namespace Tynamix.ObjectFiller
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// The filler setup item contains the setup for the object filler.
    /// The setup can be made per type, property and so on
    /// </summary>
    internal class FillerSetupItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FillerSetupItem"/> class.
        /// </summary>
        internal FillerSetupItem()
        {
            this.ListMinCount = 1;
            this.ListMaxCount = 25;
            this.DictionaryKeyMinCount = 1;
            this.DictionaryKeyMaxCount = 10;
            this.TypeToRandomFunc = new Dictionary<Type, Func<object>>();
            this.PropertyToRandomFunc = new Dictionary<PropertyInfo, Func<object>>();
            this.PropertiesToIgnore = new List<PropertyInfo>();
            this.PropertyOrder = new Dictionary<PropertyInfo, At>();
            this.TypesToIgnore = new List<Type>();
            this.InterfaceToImplementation = new Dictionary<Type, Type>();
            this.IgnoreAllUnknownTypes = false;
            this.IgnoreInheritance = false;

            this.SetDefaultRandomizer();
        }

        /// <summary>
        /// Gets the order in which the properties get handled.
        /// </summary>
        internal Dictionary<PropertyInfo, At> PropertyOrder { get; private set; }

        /// <summary>
        /// Gets the randomizer <see cref="Func{TResult}"/> for a specific type
        /// </summary>
        internal Dictionary<Type, Func<object>> TypeToRandomFunc { get; private set; }

        /// <summary>
        /// Gets the randomizer <see cref="Func{TResult}"/> for a specific property
        /// </summary>
        internal Dictionary<PropertyInfo, Func<object>> PropertyToRandomFunc { get; private set; }

        /// <summary>
        /// Gets the type of interface with the corresponding implementation
        /// </summary>
        internal Dictionary<Type, Type> InterfaceToImplementation { get; private set; }

        /// <summary>
        /// Gets a list with all properties which will be ignored while generating test data
        /// </summary>
        internal List<PropertyInfo> PropertiesToIgnore { get; private set; }

        /// <summary>
        /// Gets all types which will be ignored completely
        /// </summary>
        internal List<Type> TypesToIgnore { get; private set; }

        /// <summary>
        /// Gets or sets the minimum count of list items which will be generated 
        /// </summary>
        internal int ListMinCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum count of list items which will be generated 
        /// </summary>
        internal int ListMaxCount { get; set; }

        /// <summary>
        /// Gets or sets the minimum count of key items within a dictionary which will be generated 
        /// </summary>
        internal int DictionaryKeyMinCount { get; set; }

        /// <summary>
        /// Gets or sets the maximum count of key items within a dictionary which will be generated 
        /// </summary>
        internal int DictionaryKeyMaxCount { get; set; }

        /// <summary>
        /// Gets or sets the interface Mocker for interface generation
        /// </summary>
        internal IInterfaceMocker InterfaceMocker { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether all unknown types shall be ignored.
        /// </summary>
        internal bool IgnoreAllUnknownTypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the properties of the base type will be filled or not.
        /// </summary>
        internal bool IgnoreInheritance { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a e exception shall be thrown on circular reference.
        /// </summary>
        internal bool ThrowExceptionOnCircularReference { get; set; }

        /// <summary>
        /// Sets the default randomizer for all simple types.
        /// </summary>
        private void SetDefaultRandomizer()
        {
            var mnemonic = new MnemonicString(1);
            var doublePlugin = new DoubleRange();
            var dateTimeRandomizer = new DateTimeRange(new DateTime(1970, 1, 1));
            var uriRandomizer = new RandomUri();
            this.TypeToRandomFunc[typeof(string)] = mnemonic.GetValue;
            this.TypeToRandomFunc[typeof(bool)] = () => Random.Next(0, 2) == 1;
            this.TypeToRandomFunc[typeof(bool?)] = () => new RandomListItem<bool?>(true, false, null).GetValue();
            this.TypeToRandomFunc[typeof(short)] = () => (short)Random.Next(-32767, 32767);
            this.TypeToRandomFunc[typeof(short?)] = () => (short)Random.Next(-32767, 32767);
            this.TypeToRandomFunc[typeof(int)] = () => Random.Next();
            this.TypeToRandomFunc[typeof(int?)] = () => Random.Next();
            this.TypeToRandomFunc[typeof(long)] = () => Random.NextLong();
            this.TypeToRandomFunc[typeof(long?)] = () => Random.NextLong();
            this.TypeToRandomFunc[typeof(float)] = () => (float)doublePlugin.GetValue();
            this.TypeToRandomFunc[typeof(float?)] = () => (float?)doublePlugin.GetValue();
            this.TypeToRandomFunc[typeof(double)] = () => doublePlugin.GetValue();
            this.TypeToRandomFunc[typeof(double?)] = () => doublePlugin.GetValue();
            this.TypeToRandomFunc[typeof(decimal)] = () => (decimal)Random.Next();
            this.TypeToRandomFunc[typeof(decimal?)] = () => (decimal)Random.Next();
            this.TypeToRandomFunc[typeof(Guid)] = () => Guid.NewGuid();
            this.TypeToRandomFunc[typeof(Guid?)] = () => Guid.NewGuid();
            this.TypeToRandomFunc[typeof(DateTime)] = () => dateTimeRandomizer.GetValue();
            this.TypeToRandomFunc[typeof(DateTime?)] = () => dateTimeRandomizer.GetValue();
            this.TypeToRandomFunc[typeof(byte)] = () => (byte)Random.Next();
            this.TypeToRandomFunc[typeof(byte?)] = () => (byte?)Random.Next();
            this.TypeToRandomFunc[typeof(char)] = () => (char)Random.Next();
            this.TypeToRandomFunc[typeof(char?)] = () => (char)Random.Next();
            this.TypeToRandomFunc[typeof(ushort)] = () => (ushort)Random.Next();
            this.TypeToRandomFunc[typeof(ushort?)] = () => (ushort)Random.Next();
            this.TypeToRandomFunc[typeof(uint)] = () => (uint)Random.Next();
            this.TypeToRandomFunc[typeof(uint?)] = () => (uint)Random.Next();
            this.TypeToRandomFunc[typeof(ulong)] = () => (ulong)Random.Next();
            this.TypeToRandomFunc[typeof(ulong?)] = () => (ulong)Random.Next();
            this.TypeToRandomFunc[typeof(IntPtr)] = () => default(IntPtr);
            this.TypeToRandomFunc[typeof(IntPtr?)] = () => default(IntPtr);
            this.TypeToRandomFunc[typeof(TimeSpan)] = () => new TimeSpan(Random.Next());
            this.TypeToRandomFunc[typeof(TimeSpan?)] = () => new TimeSpan(Random.Next());
            this.TypeToRandomFunc[typeof(Uri)] = () => uriRandomizer.GetValue();
#if !NETSTANDARD
            this.TypeToRandomFunc[typeof(ArrayList)] = () => ((IRandomizerPlugin<ArrayList>)new Collectionizer<string, MnemonicString>()).GetValue();
#endif
        }
    }
}
