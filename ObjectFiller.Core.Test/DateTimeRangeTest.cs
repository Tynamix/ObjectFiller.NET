namespace ObjectFiller.Test
{
    using System;
    using System.Linq;

    using Xunit;

    using Tynamix.ObjectFiller;

    public class DateRangeTestClass
    {
        public DateTime Date { get; set; }
    }


    public class DateTimeRangeTest
    {
        [Fact]
        public void WhenGettingDatesBetweenNowAnd31DaysAgo()
        {
            Filler<DateRangeTestClass> filler = new Filler<DateRangeTestClass>();

            filler.Setup().OnType<DateTime>().Use(
                new DateTimeRange(DateTime.Now.AddDays(-31)));

            var d = filler.Create(1000);

            Assert.True(d.All(x => x.Date < DateTime.Now && x.Date > DateTime.Now.AddDays(-31)));
        }

        [Fact]
        public void WhenStartDateIsBiggerThenEndDateTheDatesShouldBeSwitched()
        {
            Filler<DateRangeTestClass> filler = new Filler<DateRangeTestClass>();

            filler.Setup().OnType<DateTime>().Use(
                new DateTimeRange(DateTime.Now, DateTime.Now.AddDays(-31)));

            var d = filler.Create(1000);

            Assert.True(d.All(x => x.Date < DateTime.Now && x.Date > DateTime.Now.AddDays(-31)));
        }

        [Fact]
        public void WhenStartDateAndEndDateIsSetItShouldFindOnlyDatesInBetweenThisTwoDates()
        {
            var startDate = new DateTime(2000, 11, 10);
            var endDate = new DateTime(2006, 1, 30);

            Filler<DateRangeTestClass> filler = new Filler<DateRangeTestClass>();

            filler.Setup().OnType<DateTime>().Use(new DateTimeRange(startDate, endDate));

            var d = filler.Create(1000);
            d.ToList().ForEach(x => Console.WriteLine(x.Date));
            Assert.True(d.All(x => x.Date < endDate && x.Date > startDate));
        }

        [Fact]
        public void WhenConstructorWithDateTimeNowWillBeCalledNoExceptionShouldBeThrown()
        {
            Filler<DateRangeTestClass> filler = new Filler<DateRangeTestClass>();

            filler.Setup().OnProperty(x => x.Date).Use(new DateTimeRange(DateTime.Now));

            var dateTime = filler.Create();

            Assert.True(dateTime.Date > DateTime.MinValue);
            Assert.True(dateTime.Date < DateTime.MaxValue);
        }
    }
}