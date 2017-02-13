using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectFiller.Test.BugfixTests
{
    using Tynamix.ObjectFiller;

    using Xunit;


    public class Bug89FillTypesInheritsFromList
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
        public class EntityBList : List<EntityB>
        {
            public DateTime SomeDate { get; set; }
        }

        [Fact]
        public void ADerivedListShallGetFilledAllProperties()
        {
            Filler<EntityA> filler = new Filler<EntityA>();

            var result = filler.Create();

            Assert.NotNull(result.Bs);
            Assert.InRange(result.Bs.SomeDate, DateTime.MinValue.AddSeconds(1), DateTime.MaxValue.AddSeconds(-1));
            Assert.True(result.Bs.Any());
        }
    }
}
