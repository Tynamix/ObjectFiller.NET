using System;

namespace Tynamix.ObjectFiller
{
    public class DateTimeRange : IRandomizerPlugin<DateTime>, IRandomizerPlugin<DateTime?>
    {
        private readonly DateTime _earliestDate;
        private readonly DateTime _latestDate;

        public DateTimeRange(DateTime earliestDate)
            : this(earliestDate, DateTime.Now)
        {
        }

        public DateTimeRange(DateTime earliestDate, DateTime latestDate)
        {

            if (earliestDate >= latestDate)
            {
                latestDate = latestDate.AddMonths(Random.Next(1, 120));
            }
            _earliestDate = earliestDate;
            _latestDate = latestDate;
        }

        public DateTime GetValue()
        {
            var timeSpan = _latestDate.Subtract(_earliestDate);

            var diff = Random.NextLong(0, timeSpan.Ticks);

            return _latestDate.AddTicks(diff * -1);
        }

        DateTime? IRandomizerPlugin<DateTime?>.GetValue()
        {
            return GetValue();
        }
    }
}
