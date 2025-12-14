using HouseholdBudget.Core.Domain.Auth.Entities;

namespace HouseholdBudget.Core.Domain.Auth.Interfaces;

/// <summary>
/// セッションリポジトリのインタフェース
/// </summary>
public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(string sessionId, CancellationToken cancellationToken = default);
    Task<Session?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Session session, CancellationToken cancellationToken = default);
    void Update(Session session);
    void Delete(Session session);
    Task DeleteExpiredSessionsAsync(CancellationToken cancellationToken = default);
}
