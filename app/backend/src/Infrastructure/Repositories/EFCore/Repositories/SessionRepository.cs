using HouseholdBudget.Core.Domain.Auth.Entities;
using HouseholdBudget.Core.Domain.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore.Repositories;

/// <summary>
/// セッションリポジトリの実装
/// </summary>
public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetByIdAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.Sessions.FindAsync([sessionId], cancellationToken);
    }

    public async Task<Session?> GetActiveByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Sessions
            .Where(s => s.UserId == userId && s.IsActive && s.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddAsync(Session session, CancellationToken cancellationToken = default)
    {
        await _context.Sessions.AddAsync(session, cancellationToken);
    }

    public void Update(Session session)
    {
        _context.Sessions.Update(session);
    }

    public void Delete(Session session)
    {
        _context.Sessions.Remove(session);
    }

    public async Task DeleteExpiredSessionsAsync(CancellationToken cancellationToken = default)
    {
        var expiredSessions = await _context.Sessions
            .Where(s => s.ExpiresAt <= DateTime.UtcNow || !s.IsActive)
            .ToListAsync(cancellationToken);

        _context.Sessions.RemoveRange(expiredSessions);
    }
}
