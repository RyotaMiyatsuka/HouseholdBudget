using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Transactions.Interfaces;

namespace HouseholdBudget.Infrastructure.EFCore.Repositories.Transactions;

/// <summary>
/// Transactionリポジトリの実装
/// </summary>
public sealed class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddAsync(Transaction transaction)
    {
        await this._context.Transactions.AddAsync(transaction);
    }

    public Task UpdateAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }

    public Task<Transaction?> FindByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Transaction transaction)
    {
        throw new NotImplementedException();
    }
}
