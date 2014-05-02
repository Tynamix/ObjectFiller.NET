using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.ListTest;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class ListFillingTest
    {
        [TestMethod]
        public void TestFillAllListsExceptArray()
        {
            Filler<EntityCollection> eFiller = new Filler<EntityCollection>();
            eFiller.Setup()
                .OnProperty(x => x.EntityArray).IgnoreIt();

            EntityCollection entity = eFiller.Create();

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.EntityList);
            Assert.IsNotNull(entity.EntityICollection);
            Assert.IsNotNull(entity.EntityIEnumerable);
            Assert.IsNotNull(entity.EntityIList);
        }

        [TestMethod]
        public void TestFillList()
        {
            Filler<EntityCollection> eFiller = new Filler<EntityCollection>();
            eFiller.Setup()
                .OnProperty(ec => ec.EntityArray).Use(GetArray);
            EntityCollection entity = eFiller.Create();

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.EntityList);
            Assert.IsNotNull(entity.EntityICollection);
            Assert.IsNotNull(entity.EntityIEnumerable);
            Assert.IsNotNull(entity.EntityIList);
            Assert.IsNotNull(entity.EntityArray);

        }

        private Entity[] GetArray()
        {
            Filler<Entity> of = new Filler<Entity>();

            List<Entity> entities = new List<Entity>();
            entities.Add(of.Create());
            entities.Add(of.Create());
            entities.Add(of.Create());
            entities.Add(of.Create());
            entities.Add(of.Create());
            entities.Add(of.Create());
            entities.Add(of.Create());
            entities.Add(of.Create());
            entities.Add(of.Create());


            return entities.ToArray();
        }
    }
}
