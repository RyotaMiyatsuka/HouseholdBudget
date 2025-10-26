namespace HouseholdBudget.Core.Domain.Transactions.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    ITransactionRepository TransactionRepository { get; }
}
