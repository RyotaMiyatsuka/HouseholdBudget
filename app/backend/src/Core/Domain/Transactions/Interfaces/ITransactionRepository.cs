using HouseholdBudget.Core.Domain.Transactions.Entities;

namespace HouseholdBudget.Core.Domain.Transactions.Interfaces;

/// <summary>
/// 取引リポジトリのインタフェース
/// </summary>
public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Transaction?> GetByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Transaction>> GetByUserIdAndMonthAsync(Guid userId, int year, int month, CancellationToken cancellationToken = default);
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
    void Update(Transaction transaction);
    void Delete(Transaction transaction);
}
