using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller.Test.TestPoco.ListTest;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    using ObjectFiller.Test.TestPoco;

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
            Assert.IsNotNull(entity.EntityHashset);

        }

        [TestMethod]
        public void TestUseEnumerable()
        {
            Filler<EntityCollection> eFiller = new Filler<EntityCollection>();
            eFiller.Setup()
                .ListItemCount(20)
                .OnProperty(x => x.EntityICollection,
                            x => x.EntityIList, x => x.ObservableCollection,
                            x => x.EntityIEnumerable).IgnoreIt()
            .OnProperty(x => x.EntityArray).IgnoreIt()
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
            Assert.IsNotNull(entity.EntityHashset);
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
            Assert.IsNotNull(entity.EntityHashset);
        }

        [TestMethod]
        public void TestIgnoreAllUnknownTypesWithException()
        {
            Filler<EntityCollection> filler = new Filler<EntityCollection>();
            Assert.ThrowsException<TypeInitializationException>(()=>filler.Create());
        }

        [TestMethod]
        public void GenerateTestDataForASortedList()
        {
            Filler<SortedList<int, string>> filler = new Filler<SortedList<int, string>>();
            filler.Setup().OnType<int>().Use(Enumerable.Range(1, 1000));
            var result = filler.Create(10).ToList();

            Assert.AreEqual(10, result.Count);
            foreach (var sortedList in result)
            {
                Assert.IsTrue(sortedList.Any());
            }
        }

        [TestMethod]
        public void GenerateTestDataForASimpleList()
        {
            Filler<IList<EntityCollection>> filler = new Filler<IList<EntityCollection>>();
            filler.Setup().IgnoreAllUnknownTypes();
            var createdList = filler.Create();

            Assert.IsTrue(createdList.Any());

            foreach (EntityCollection entityCollection in createdList)
            {
                Assert.IsTrue(entityCollection.EntityICollection.Any());
                Assert.IsTrue(entityCollection.EntityIEnumerable.Any());
                Assert.IsTrue(entityCollection.EntityIList.Any());
                Assert.IsTrue(entityCollection.EntityList.Any());
                Assert.IsNotNull(entityCollection.EntityHashset.Any());
            }
        }

        [TestMethod]
        public void GenerateTestDataForADictionary()
        {
            Filler<Dictionary<int, string>> filler = new Filler<Dictionary<int, string>>();
            var result = filler.Create(10).ToList();

            Assert.AreEqual(10, result.Count);
            foreach (var sortedList in result)
            {
                Assert.IsTrue(sortedList.Any());
            }
        }

        [TestMethod]
        public void GenerateDictionaryWithEnumeration()
        {
            var amountOfEnumValues = Enum.GetValues(typeof(TestEnum)).Length;
            var filler = new Filler<Dictionary<TestEnum, string>>();
            var result = filler.Create();

            Assert.AreEqual(amountOfEnumValues, result.Count);
        }

        private Entity[,] GetArray()
        {
            Filler<Entity> of = new Filler<Entity>();
            var entity = new Entity[,] { { of.Create() } };

            return entity;
        }
    }
}
