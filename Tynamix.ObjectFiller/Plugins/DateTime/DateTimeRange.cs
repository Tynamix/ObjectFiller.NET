using System;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// The date time range plugin.
    /// </summary>
    public class DateTimeRange : IRandomizerPlugin<DateTime>, IRandomizerPlugin<DateTime?>
    {
        /// <summary>
        /// The earliest date.
        /// </summary>
        private readonly DateTime earliestDate;

        /// <summary>
        /// The latest date.
        /// </summary>
        private readonly DateTime latestDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> class.
        /// </summary>
        public DateTimeRange()
            : this(DateTime.MinValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> class.
        /// </summary>
        /// <param name="earliestDate">
        /// The earliest date.
        /// </param>
        public DateTimeRange(DateTime earliestDate)
            : this(earliestDate, DateTime.Now)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeRange"/> class.
        /// </summary>
        /// <param name="earliestDate">
        /// The earliest date.
        /// </param>
        /// <param name="latestDate">
        /// The latest date.
        /// </param>
        public DateTimeRange(DateTime earliestDate, DateTime latestDate)
        {
            if (earliestDate > latestDate)
            {
                this.latestDate = earliestDate;
                this.earliestDate = latestDate;
            }
            else if (earliestDate == latestDate)
            {
                this.latestDate = latestDate.AddMonths(Random.Next(1, 120));
                this.earliestDate = earliestDate;
            }
            else
            {
                this.earliestDate = earliestDate;
                this.latestDate = latestDate;
            }
        }

        /// <summary>
        /// Gets random data for type <see cref="DateTime"/>
        /// </summary>
        /// <returns>Random data for type <see cref="DateTime"/></returns>
        public DateTime GetValue()
        {
            var timeSpan = this.latestDate.Subtract(this.earliestDate);

            var diff = Random.NextLong(0, timeSpan.Ticks);

            var generatedDate = this.latestDate.AddTicks(diff * -1);
            return TimeZoneInfo.Utc.IsInvalidTime(generatedDate) ? GetValue() : generatedDate;
        }

        /// <summary>
        /// Gets random data for type <see cref="Nullable{DateTime}"/>
        /// </summary>
        /// <returns>Random data for type <see cref="Nullable{DateTime}"/></returns>
        DateTime? IRandomizerPlugin<DateTime?>.GetValue()
        {
            return this.GetValue();
        }
    }
}
