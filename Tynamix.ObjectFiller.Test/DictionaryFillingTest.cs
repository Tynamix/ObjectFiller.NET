using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test
{
    [TestClass]
    public class DictionaryFillingTest
    {
        public class EntityA
        {
            public string Name { get; set; }
            public int ID { get; set; }
            public IDictionary<string, string> InterfaceDictionary { get; set; }
            public Dictionary<string, string> ConcreteDictionary { get; set; }

            public NestedEntity NestedEntity { get; set; }
        }

        public class NestedEntity
        {
            public IDictionary<string, string> InterfaceDictionary { get; set; }
            public Dictionary<string, string> ConcreteDictionary { get; set; }
        }


        [TestMethod]
        public void TestDictionaryType()
        {
            Filler<EntityA> filler = new Filler<EntityA>();

            var result = filler.Create();

            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.ID);
            Assert.IsNotNull(result.InterfaceDictionary);
            Assert.IsTrue(result.InterfaceDictionary.Any());
            Assert.IsNotNull(result.ConcreteDictionary);
            Assert.IsTrue(result.ConcreteDictionary.Any());

            Assert.IsNotNull(result.NestedEntity);
            Assert.IsNotNull(result.NestedEntity.InterfaceDictionary);
            Assert.IsTrue(result.NestedEntity.InterfaceDictionary.Any());
            Assert.IsNotNull(result.NestedEntity.ConcreteDictionary);
            Assert.IsTrue(result.NestedEntity.ConcreteDictionary.Any());

        }
    }
}
