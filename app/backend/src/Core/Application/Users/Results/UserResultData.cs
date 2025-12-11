namespace HouseholdBudget.Core.Application.Users.Results;

/// <summary>
/// ユーザー結果データ
/// </summary>
public record UserResultData(Guid UserId, string Email, string UserName, string? SessionId = null);
