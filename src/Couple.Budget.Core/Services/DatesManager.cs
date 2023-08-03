namespace Couple.Budget.Core.Services
{
    public class DatesManager : IDatesManager
    {
        public IEnumerable<DateTime> GetAllDaysOfWeekDatesFromMonth(int month, IEnumerable<DayOfWeek> daysOfWeek)
        {
            var currentYear = DateTime.Now.Year;
            var datesInMonth = new List<DateTime>();
            var daysInMonth = DateTime.DaysInMonth(currentYear, month);

            for (int day = 1; day < daysInMonth; day++)
            {
                var date = new DateTime(currentYear, month, day);

                if (daysOfWeek.Contains(date.DayOfWeek))
                {
                    datesInMonth.Add(new DateTime(date.Year, date.Month, date.Day));
                }
            }

            return datesInMonth;
        }

        public int GetCurrentMonth()
        {
            return DateTime.Now.Month;
        }
    }
}