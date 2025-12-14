namespace HouseholdBudget.Core.Application.Users.Commands;

/// <summary>
/// ユーザー登録コマンド
/// </summary>
public record CreateUserCommand(string Email, string UserName, string? IdToken = null);
