using Couple.Budget.Core.DomainObjects;
using Couple.Budget.Core.Exceptions;
using Couple.Budget.Domain.Budgets.ValueObjects;
using Couple.Budget.Domain.Users.Entities;

namespace Couple.Budget.Domain.Budgets.Entities
{
    public class Budget : AggregateRoot
    {
        public Month Month { get; set; }
        public decimal Value { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        private List<Suggest> _suggests;
        public IReadOnlyCollection<Suggest> Suggests => _suggests.AsReadOnly();

        private List<BudgetDay> _budgetDays;
        public IReadOnlyCollection<BudgetDay> BudgetDays => _budgetDays.AsReadOnly();

        protected Budget() 
        {
            _suggests = new List<Suggest>();
            _budgetDays = new List<BudgetDay>();
        }
        public Budget(User user, int month, decimal value) : this()
        {
            UpdateMonth(month);
            UpdateValue(value);
            UpdateUser(user);
        }

        public void UpdateValue(decimal value)
        {
            if (value <= 0)
            {
                throw new ValidationException("O valor do orçamento não pode ser menor que 1");
            }

            var budgetDaysValues = BudgetDays.Sum(d => d.Value);

            if (value < budgetDaysValues)
            {
                throw new ValidationException("O valor do orçamento não pode ser menor que o valor da soma dos dias já cadastrados");
            }

            Value = value;
        }

        public void UpdateMonth(int month)
        {
            Month = Month.FromNumber(month);
        }

        private void UpdateUser(User user)
        {
            if(user is null)
            {
                throw new ValidationException(nameof(user));
            }

            User = user;
            UserId = user.Id;
        }

        public void AddSuggests(IEnumerable<Suggest> suggests)
        {
            if(suggests is null)
            {
                throw new ValidationException(nameof(suggests));
            }

            foreach (var suggest in suggests)
            {
                _suggests.Add(suggest);
            }
        }

        public void ClearSuggests()
        {
            _suggests.Clear();
        }

        public void AddBudgetDays(IEnumerable<BudgetDay> budgetDays)
        {
            if (budgetDays is null)
            {
                throw new ValidationException(nameof(budgetDays));
            }

            foreach (var budgetDay in budgetDays)
            {
                AddBudgetDay(budgetDay);
            }
        }

        public void AddBudgetDay(BudgetDay budgetDay)
        {
            if(budgetDay is null)
            {
                throw new ValidationException(nameof(budgetDay));
            }

            _budgetDays.Add(budgetDay);
        }

        public void ClearBudgetDays()
        {
            _budgetDays.Clear();
        }
    }
}