using Couple.Budget.Core.DomainObjects;
using Couple.Budget.Core.Exceptions;

namespace Couple.Budget.Domain.Budgets.Entities
{
    public class BudgetDayExpense : Entity
    {
        protected BudgetDayExpense() { }
        public BudgetDayExpense(BudgetDay budgetDay, string name, decimal value)
        {
            UpdateName(name);
            UpdateValue(value);
            UpdateBudgetDay(budgetDay);
        }

        public string Name { get; private set; }
        public decimal Value { get; private set; }

        public Guid BudgetDayId { get; private set; }
        public BudgetDay BudgetDay { get; private set; }

        private void UpdateBudgetDay(BudgetDay budgetDay)
        {
            if (budgetDay is null)
            {
                throw new ValidationException(nameof(budgetDay));
            }

            BudgetDay = budgetDay;
            BudgetDayId = budgetDay.Id;
        }

        private void UpdateValue(decimal value)
        {
            if (value < 0)
            {
                throw new ValidationException("O valor não deve ser menor que zero.");
            }

            Value = value;
        }

        private void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException(nameof(name));
            }

            Name = name;
        }

    }
}