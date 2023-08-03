using Couple.Budget.Core.DomainObjects;
using Couple.Budget.Core.Exceptions;

namespace Couple.Budget.Domain.Budgets.Entities
{
    public class Suggest : Entity
    {
        protected Suggest() { }

        public Suggest(Budget budget, string name, decimal value)
        {
            UpdateName(name);
            UpdateValue(value);
            UpdateBudget(budget);
        }

        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public Guid BudgetId { get; private set; }
        public Budget Budget { get; private set; }

        private void UpdateBudget(Budget budget)
        {
            if (budget is null)
            {
                throw new ValidationException(nameof(budget));
            }

            Budget = budget;
            BudgetId = budget.Id;
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