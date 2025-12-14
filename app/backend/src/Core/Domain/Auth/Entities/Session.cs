using HouseholdBudget.Core.Domain.Common;

namespace HouseholdBudget.Core.Domain.Auth.Entities;

/// <summary>
/// セッションエンティティ
/// </summary>
public class Session : Entity<string>
{
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsActive { get; private set; }

    private Session() : base()
    {
    }

    private Session(string sessionId, Guid userId, DateTime expiresAt) : base(sessionId)
    {
        UserId = userId;
        ExpiresAt = expiresAt;
        IsActive = true;
    }

    public static Session Create(Guid userId, TimeSpan? duration = null)
    {
        var sessionId = GenerateSessionId();
        var expiresAt = DateTime.UtcNow.Add(duration ?? TimeSpan.FromHours(24));
        return new Session(sessionId, userId, expiresAt);
    }

    public bool IsValid()
    {
        return IsActive && ExpiresAt > DateTime.UtcNow;
    }

    public void Invalidate()
    {
        IsActive = false;
        SetUpdatedAt();
    }

    public void Extend(TimeSpan duration)
    {
        ExpiresAt = DateTime.UtcNow.Add(duration);
        SetUpdatedAt();
    }

    private static string GenerateSessionId()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("/", "_")
            .Replace("+", "-")
            .TrimEnd('=');
    }
}
