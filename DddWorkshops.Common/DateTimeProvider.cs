using System;

namespace DddWorkshops.Common
{
    /// <summary>
    ///     Provides specific implementations for determining <see cref="DateTime"/>s.
    /// </summary>
    public static class DateTimeProvider
    {
        private static readonly Func<DateTime> DateTimeNowDefaultImplementation = () => DateTime.Now;

        private static readonly Func<DateTime> DateTimeUtcNowDefaultImplementation = () => DateTime.UtcNow;

        private static Func<DateTime> nowImplementation = DateTimeNowDefaultImplementation;

        private static Func<DateTime> utcNowImplementation = DateTimeUtcNowDefaultImplementation;

        /// <summary>
        ///     Gets a <see cref="DateTime"/> object that is set to current date and time of this computer, expressed as local time.
        /// </summary>
        public static DateTime Now => nowImplementation();

        /// <summary>
        ///     Gets a <see cref="DateTime"/> object that is set to current date and time of this computer, expressed as Coordinated Universal Time (UTC).
        /// </summary>
        public static DateTime UtcNow => utcNowImplementation();

        /// <summary>
        ///     Sets implementation of <see cref="Now"/>.
        /// </summary>
        /// <param name="dateTimeNowImplementation">New implementation of <see cref="Now"/> property.</param>
        /// <remarks>Should be used only in test methods, where specific dates have to be compared.</remarks>
        public static void SetNowImplementation(Func<DateTime> dateTimeNowImplementation) => nowImplementation = dateTimeNowImplementation;

        /// <summary>
        ///     Sets implementation of <see cref="UtcNow"/>.
        /// </summary>
        /// <param name="dateTimeUtcNowImplementation">New implementation of <see cref="UtcNow"/> property.</param>
        /// <remarks>Should be used only in test methods, where specific dates have to be compared.</remarks>
        public static void SetUtcNowImplementation(Func<DateTime> dateTimeUtcNowImplementation) =>
            utcNowImplementation = dateTimeUtcNowImplementation;

        /// <summary>
        ///     Resets all provided implementations (<see cref="Now"/> and <see cref="UtcNow"/>) to default ones.
        /// </summary>
        public static void ResetImplementations()
        {
            nowImplementation = DateTimeNowDefaultImplementation;
            utcNowImplementation = DateTimeUtcNowDefaultImplementation;
        }
    }
}