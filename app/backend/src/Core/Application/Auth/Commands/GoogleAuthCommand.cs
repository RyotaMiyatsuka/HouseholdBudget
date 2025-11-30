namespace HouseholdBudget.Core.Application.Auth.Commands;

/// <summary>
/// Google認証コマンド
/// </summary>
public class GoogleAuthCommand
{
    /// <summary>
    /// Google OAuth 2.0 ID トークン
    /// </summary>
    public required string IdToken { get; set; }
}
