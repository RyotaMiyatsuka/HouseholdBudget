namespace HouseholdBudget.Core.Domain.Auth.Interfaces;

/// <summary>
/// Google認証サービスのインタフェース
/// </summary>
public interface IGoogleAuthService
{
    /// <summary>
    /// GoogleのIDトークンを検証し、メールアドレスを取得する
    /// </summary>
    /// <param name="idToken">Google ID Token</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>メールアドレス、検証失敗時はnull</returns>
    Task<string?> ValidateTokenAndGetEmailAsync(string idToken, CancellationToken cancellationToken = default);
}
