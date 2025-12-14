using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Users.Interfaces;
using HouseholdBudget.Core.Domain.Auth.Interfaces;

namespace HouseholdBudget.Core.Domain.Common.Interfaces;

/// <summary>
/// UnitOfWorkパターンのインタフェース
/// </summary>
public interface IUnitOfWork : IDisposable
{
    ITransactionRepository Transactions { get; }
    ICategoryRepository Categories { get; }
    IUserRepository Users { get; }
    ISessionRepository Sessions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
