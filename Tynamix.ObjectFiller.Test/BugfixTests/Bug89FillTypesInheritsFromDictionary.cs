using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tynamix.ObjectFiller.Test.BugfixTests
{

    [TestClass]
    public class Bug89FillTypesInheritsFromDictionary
    {
        public class EntityA
        {
            public string Name { get; set; }
            public int ID { get; set; }
            public EntityBList Bs { get; set; }
        }
        public class EntityB
        {
            public string Name { get; set; }
            public Guid ID { get; set; }
        }
        public class EntityBList : Dictionary<string, EntityB>
        {
            public DateTime SomeDate { get; set; }
        }

        [TestMethod]
        public void ADerivedDictionaryShallGetFilledAllProperties()
        {
            Filler<EntityA> filler = new Filler<EntityA>();

            var result = filler.Create();

            Assert.IsNotNull(result.Bs);
            Assert.IsTrue(result.Bs.SomeDate > DateTime.MinValue.AddSeconds(1) && result.Bs.SomeDate < DateTime.MaxValue.AddSeconds(-1));
            Assert.IsTrue(result.Bs.Any());
        }
    }
}
