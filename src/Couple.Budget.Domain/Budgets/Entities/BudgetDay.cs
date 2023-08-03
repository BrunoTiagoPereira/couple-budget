using Couple.Budget.Core.DomainObjects;
using Couple.Budget.Core.Exceptions;

namespace Couple.Budget.Domain.Budgets.Entities
{
    public class BudgetDay : Entity
    {
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
        public Guid BudgetId { get; private set; }
        public Budget Budget { get; private set; }

        private List<BudgetDayExpense> _expenses;
        public IReadOnlyCollection<BudgetDayExpense> Expenses => _expenses.AsReadOnly();

        protected BudgetDay() 
        {
            _expenses = new List<BudgetDayExpense>();
        }
        public BudgetDay(Budget budget, DateTime date, string name, decimal value) : this()
        {
            UpdateName(name);
            UpdateValue(value);
            UpdateDate(date);
            UpdateBudget(budget);
        }

        public void AddExpenses(IEnumerable<BudgetDayExpense> expenses)
        {
            if (expenses is null)
            {
                throw new ValidationException(nameof(expenses));
            }

            foreach (var expense in expenses)
            {
                AddExpense(expense);
            }
        }

        public void AddExpense(BudgetDayExpense expense)
        {
            if(expense is null)
            {
                throw new ValidationException(nameof(expense));
            }

            if (ExpenseExceedsDayValue(expense))
            {
                throw new ValidationException("O valor das despesas é maior que o valor do dia do orçamento.");
            }

            _expenses.Add(expense);
        }

        private bool ExpenseExceedsDayValue(BudgetDayExpense expense)
        {
            return (_expenses.Sum(x => x.Value) + expense.Value) > Value;
        }


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

        private void UpdateDate(DateTime date)
        {
            if (date == default)
            {
                throw new ValidationException("Data inválida.");
            }

            Date = date;
        }
    }
}