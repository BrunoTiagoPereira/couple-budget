namespace Couple.Budget.Host.Budget.Queries.Responses
{
    public class GetBudgetQueryResponse
    {
        public GetBudgetBudgetQueryResponse Budget { get; set; }
    }

    public class GetBudgetBudgetQueryResponse
    {
        public Guid Id { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }

        public decimal Value { get; set; }

        public IEnumerable<GetBudgetSuggestQueryResponse> Suggests { get; set; }

        public IEnumerable<GetBudgetBudgetDayQueryResponse> BudgetDays { get; set; }
    }

    public class GetBudgetSuggestQueryResponse
    {
        public string Name { get; set; }

        public decimal Value { get; set; }
    }

    public class GetBudgetBudgetDayQueryResponse
    {
        public string Name { get; set; }

        public decimal Value { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<GetBudgetBudgetDayExpenseQueryResponse> Expenses { get; set; }

    }

    public class GetBudgetBudgetDayExpenseQueryResponse
    {
        public string Name { get; set; }

        public decimal Value { get; set; }
    }

}
