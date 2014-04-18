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
            ObjectFiller<EntityCollection> eFiller = new ObjectFiller<EntityCollection>();
            eFiller.Setup()
                .IgnoreProperties(ec => ec.EntityArray);
            EntityCollection entity = eFiller.Fill();

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.EntityList);
            Assert.IsNotNull(entity.EntityICollection);
            Assert.IsNotNull(entity.EntityIEnumerable);
            Assert.IsNotNull(entity.EntityIList);
        }

        [TestMethod]
        public void TestFillList()
        {
            ObjectFiller<EntityCollection> eFiller = new ObjectFiller<EntityCollection>();
            eFiller.Setup()
                .RandomizerForProperty(GetArray, ec => ec.EntityArray);
            EntityCollection entity = eFiller.Fill();

            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.EntityList);
            Assert.IsNotNull(entity.EntityICollection);
            Assert.IsNotNull(entity.EntityIEnumerable);
            Assert.IsNotNull(entity.EntityIList);
            Assert.IsNotNull(entity.EntityArray);

        }

        private Entity[] GetArray()
        {
            ObjectFiller<Entity> of = new ObjectFiller<Entity>();

            List<Entity> entities = new List<Entity>();
            entities.Add(of.Fill());
            entities.Add(of.Fill());
            entities.Add(of.Fill());
            entities.Add(of.Fill());
            entities.Add(of.Fill());
            entities.Add(of.Fill());
            entities.Add(of.Fill());
            entities.Add(of.Fill());
            entities.Add(of.Fill());


            return entities.ToArray();


        }
    }
}