using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tynamix.ObjectFiller.Test.BugfixTests
{
    [TestClass]
    public class Bug129WrongDateTimeGeneration
    {
        public class TestEntity
        {
            public DateTime Date { get; set; }
        }

        [TestMethod]
        public void InvalidDateTimeValuesDueToDaylightSavingsTime()
        {
            Filler<TestEntity> filler = new Filler<TestEntity>();

            filler.Setup().OnType<DateTime>().Use(new DateTimeRange(new DateTime(2007, 3, 11, 1, 0, 0, DateTimeKind.Unspecified), new DateTime(2007, 3, 11, 4, 00, 0, DateTimeKind.Unspecified)));
            
            for (int i = 0; i < 1000; i++)
            {
                var result = filler.Create();
                Assert.IsFalse(TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time").IsInvalidTime(result.Date));
            }



        }
    }
}
