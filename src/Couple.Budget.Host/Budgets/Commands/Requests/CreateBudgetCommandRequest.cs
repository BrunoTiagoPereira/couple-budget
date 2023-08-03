namespace Couple.Budget.Host.Budget.Commands.Requests
{
    public class CreateBudgetCommandRequest
    {
        public int Month { get; set; }
        public decimal Value { get; set; }
    }
}
