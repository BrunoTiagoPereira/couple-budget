namespace Couple.Budget.Host.Budget.Queries.Responses
{
    public class GetBudgetsQueryResponse
    {
        public IEnumerable<GetBudgetsBudgetQueryResponse> Budgets { get; set; }
    }

    public class GetBudgetsBudgetQueryResponse
    {
        public Guid Id { get; set; }
        public int Month { get; set; }
        public string MonthName { get; set; }
        public decimal Value { get; set; }
    }

}
