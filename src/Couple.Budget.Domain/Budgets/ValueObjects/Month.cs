using Couple.Budget.Core.Exceptions;
using Couple.Budget.Core.ValueObjects;
using System.Globalization;

namespace Couple.Budget.Domain.Budgets.ValueObjects
{
    public class Month : ValueObject
    {
        public int MonthNumber { get; private set; }

        public string MonthName { get; private set; }

        protected Month() { }
        private Month(int monthNumber)
        {
            if(monthNumber < 1 || monthNumber > 12)
            {
                throw new ValidationException(nameof(monthNumber));
            }

            MonthNumber = monthNumber;
            MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber);
        }

        public static Month FromNumber(int number)
        {
            return new Month(number);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MonthNumber;
            yield return MonthName;
        }
    }
}