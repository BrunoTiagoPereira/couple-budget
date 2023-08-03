namespace Couple.Budget.Core.Services
{
    public interface IDatesManager
    {
        public int GetCurrentMonth();

        public IEnumerable<DateTime> GetAllDaysOfWeekDatesFromMonth(int month, IEnumerable<DayOfWeek> daysOfWeek);

    }
}