namespace HouseholdBudget.Core.Application.Auth.Interfaces;

public interface ISessionService
{
    Task SetLoginIdAsync(string loginId, CancellationToken cancellationToken = default);
    Task<string?> GetLoginIdAsync(CancellationToken cancellationToken = default);
    Task ClearSessionAsync(CancellationToken cancellationToken = default);
}
