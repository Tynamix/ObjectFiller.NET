using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectFiller.FillerPlugins.DateTime
{
    public class DateTimeRandomizer : IRandomizerPlugin<System.DateTime>, IRandomizerPlugin<System.DateTime?>
    {
        private readonly System.DateTime _earliestDate;
        private readonly System.DateTime _latestDate;
        private readonly Random _random;

        public DateTimeRandomizer(System.DateTime earliestDate)
            : this(earliestDate, System.DateTime.Now)
        {
        }

        public DateTimeRandomizer(System.DateTime earliestDate, System.DateTime latestDate)
        {
            _earliestDate = earliestDate;
            _latestDate = latestDate;
            _random = new Random();
        }

        public System.DateTime GetValue()
        {
            int seconds = _random.Next(0, 60);
            int minute = _random.Next(0, 60);
            int hour = _random.Next(0, 24);
            int month = _random.Next(_earliestDate.Month, _latestDate.Month + 1);
            int day = _random.Next(_earliestDate.Day, month == 2 && _latestDate.Day > 28 ? 29 : _latestDate.Day);
            int year = _random.Next(_earliestDate.Year, _latestDate.Year + 1);

            return new System.DateTime(year, month, day, hour, minute, seconds);
        }

        System.DateTime? IRandomizerPlugin<System.DateTime?>.GetValue()
        {
            return GetValue();
        }
    }
}
