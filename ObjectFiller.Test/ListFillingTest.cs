using System;
using System.Collections.Generic;
using System.Linq;
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
        public void TestUseEnumerable()
        {
            Filler<EntityCollection> eFiller = new Filler<EntityCollection>();
            eFiller.Setup()
                .ListItemCount(20)
                .OnProperty(x => x.EntityArray, x => x.EntityICollection,
                            x => x.EntityIList, x => x.ObservableCollection,
                            x => x.EntityIEnumerable).IgnoreIt()
                .SetupFor<Entity>()
                .OnProperty(x => x.Id).Use(Enumerable.Range(1, 22).Select(x => (int)Math.Pow(2, x)));

            EntityCollection ec = eFiller.Create();

            for (int i = 0; i < ec.EntityList.Count; i++)
            {
                int lastPowNum = (int)Math.Pow(2, i + 1);
                Assert.AreEqual(lastPowNum, ec.EntityList[i].Id);
            }
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

        [TestMethod]
        public void TestIgnoreAllUnknownTypesWithOutException()
        {
            Filler<EntityCollection> filler = new Filler<EntityCollection>();
            filler.Setup().IgnoreAllUnknownTypes();
            var entity = filler.Create();
            Assert.IsNull(entity.EntityArray);
            Assert.IsNotNull(entity);
            Assert.IsNotNull(entity.EntityList);
            Assert.IsNotNull(entity.EntityICollection);
            Assert.IsNotNull(entity.EntityIEnumerable);
            Assert.IsNotNull(entity.EntityIList);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeInitializationException))]
        public void TestIgnoreAllUnknownTypesWithException()
        {
            Filler<EntityCollection> filler = new Filler<EntityCollection>();
            filler.Create();
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
