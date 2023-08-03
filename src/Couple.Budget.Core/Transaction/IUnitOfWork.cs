namespace Couple.Budget.Core.Transaction
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}