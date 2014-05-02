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
            _earliestDate = earliestDate;
            _latestDate = latestDate;
        }

        public DateTime GetValue()
        {
            int seconds = Random.Next(0, 60);
            int minute = Random.Next(0, 60);
            int hour = Random.Next(0, 24);
            int month = Random.Next(_earliestDate.Month, _latestDate.Month + 1);
            int day = Random.Next(_earliestDate.Day, month == 2 && _latestDate.Day > 28 ? 29 : _latestDate.Day);
            int year = Random.Next(_earliestDate.Year, _latestDate.Year + 1);

            return new DateTime(year, month, day, hour, minute, seconds);
        }

        DateTime? IRandomizerPlugin<DateTime?>.GetValue()
        {
            return GetValue();
        }
    }
}
