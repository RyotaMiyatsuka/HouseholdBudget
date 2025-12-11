namespace HouseholdBudget.Core.Application.Auth.Results;

/// <summary>
/// 認証結果データ
/// </summary>
public record AuthResultData(string SessionId, Guid UserId, string Email);
