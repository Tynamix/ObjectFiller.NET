using System;
using System.Collections.Generic;
using System.Linq;
    using Xunit;
using ObjectFiller.Test.TestPoco.ListTest;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    using ObjectFiller.Test.TestPoco;


    public class ListFillingTest
    {
        [Fact]
        public void TestFillAllListsExceptArray()
        {
            Filler<EntityCollection> eFiller = new Filler<EntityCollection>();
            eFiller.Setup()
                .OnProperty(x => x.EntityArray).IgnoreIt();

            EntityCollection entity = eFiller.Create();

            Assert.NotNull(entity);
            Assert.NotNull(entity.EntityList);
            Assert.NotNull(entity.EntityICollection);
            Assert.NotNull(entity.EntityIEnumerable);
            Assert.NotNull(entity.EntityIList);
        }

        [Fact]
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
                Assert.Equal(lastPowNum, ec.EntityList[i].Id);
            }
        }

        [Fact]
        public void TestFillList()
        {
            Filler<EntityCollection> eFiller = new Filler<EntityCollection>();
            eFiller.Setup()
                .OnProperty(ec => ec.EntityArray).Use(GetArray);
            EntityCollection entity = eFiller.Create();

            Assert.NotNull(entity);
            Assert.NotNull(entity.EntityList);
            Assert.NotNull(entity.EntityICollection);
            Assert.NotNull(entity.EntityIEnumerable);
            Assert.NotNull(entity.EntityIList);
            Assert.NotNull(entity.EntityArray);

        }

        [Fact]
        public void TestIgnoreAllUnknownTypesWithOutException()
        {
            Filler<EntityCollection> filler = new Filler<EntityCollection>();
            filler.Setup().IgnoreAllUnknownTypes();
            var entity = filler.Create();
            Assert.Null(entity.EntityArray);
            Assert.NotNull(entity);
            Assert.NotNull(entity.EntityList);
            Assert.NotNull(entity.EntityICollection);
            Assert.NotNull(entity.EntityIEnumerable);
            Assert.NotNull(entity.EntityIList);
        }

        [Fact]
        public void TestIgnoreAllUnknownTypesWithException()
        {
            Filler<EntityCollection> filler = new Filler<EntityCollection>();
            Assert.Throws<TypeInitializationException>(()=>filler.Create());
        }

        [Fact]
        public void GenerateTestDataForASortedList()
        {
            Filler<SortedList<int, string>> filler = new Filler<SortedList<int, string>>();
            filler.Setup().OnType<int>().Use(Enumerable.Range(1, 1000));
            var result = filler.Create(10).ToList();

            Assert.Equal(10, result.Count);
            foreach (var sortedList in result)
            {
                Assert.True(sortedList.Any());
            }
        }

        [Fact]
        public void GenerateTestDataForASimpleList()
        {
            Filler<IList<EntityCollection>> filler = new Filler<IList<EntityCollection>>();
            filler.Setup().IgnoreAllUnknownTypes();
            var createdList = filler.Create();

            Assert.True(createdList.Any());

            foreach (EntityCollection entityCollection in createdList)
            {
                Assert.True(entityCollection.EntityICollection.Any());
                Assert.True(entityCollection.EntityIEnumerable.Any());
                Assert.True(entityCollection.EntityIList.Any());
                Assert.True(entityCollection.EntityList.Any());
            }
        }

        [Fact]
        public void GenerateTestDataForADictionary()
        {
            Filler<Dictionary<int, string>> filler = new Filler<Dictionary<int, string>>();
            var result = filler.Create(10).ToList();

            Assert.Equal(10, result.Count);
            foreach (var sortedList in result)
            {
                Assert.True(sortedList.Any());
            }
        }

        [Fact]
        public void GenerateDictionaryWithEnumeration()
        {
            var amountOfEnumValues = Enum.GetValues(typeof(TestEnum)).Length;
            var filler = new Filler<Dictionary<TestEnum, string>>();
            var result = filler.Create();

            Assert.Equal(amountOfEnumValues, result.Count);
        }

        private Entity[,] GetArray()
        {
            Filler<Entity> of = new Filler<Entity>();
            var entity = new Entity[,] { { of.Create() } };

            return entity;
        }
    }
}
