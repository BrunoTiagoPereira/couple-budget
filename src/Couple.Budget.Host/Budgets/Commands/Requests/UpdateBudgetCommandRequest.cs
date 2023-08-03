namespace Couple.Budget.Host.Budget.Commands.Requests
{
    public class UpdateBudgetCommandRequest
    {
        public UpdateBudgetBudgetCommandRequest Budget { get; set; }
    }

    public class UpdateBudgetBudgetCommandRequest
    {
        public Guid Id { get; set; }
        public int Month { get; set; }
        public decimal Value { get; set; }

        public IEnumerable<UpdateBudgetSuggestCommandRequest> Suggests { get; set; }

        public IEnumerable<UpdateBudgetBudgetDayCommandRequest> BudgetDays { get; set; }
    }

    public class UpdateBudgetSuggestCommandRequest
    {
        public string Name { get; set; }

        public decimal Value { get; set; }
    }

    public class UpdateBudgetBudgetDayCommandRequest
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<UpdateBudgetBudgetDayExpenseCommandRequest> Expenses { get; set; }

    }

    public class UpdateBudgetBudgetDayExpenseCommandRequest
    {
        public string Name { get; set; }

        public decimal Value { get; set; }
    }
}
