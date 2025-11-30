namespace HouseholdBudget.Core.Application.Auth.Interfaces;

/// <summary>
/// Google トークン検証結果
/// </summary>
public class GoogleTokenPayload
{
    /// <summary>
    /// ユーザーID (Google Subject Identifier)
    /// </summary>
    public required string Subject { get; set; }

    /// <summary>
    /// メールアドレス
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// メールアドレスが検証済みかどうか
    /// </summary>
    public bool EmailVerified { get; set; }

    /// <summary>
    /// ユーザー名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// プロフィール画像URL
    /// </summary>
    public string? Picture { get; set; }
}

/// <summary>
/// Google トークン検証サービスインターフェース
/// </summary>
public interface IGoogleTokenValidator
{
    /// <summary>
    /// Google ID トークンを検証する
    /// </summary>
    /// <param name="idToken">Google OAuth 2.0 ID トークン</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>検証結果のペイロード。検証失敗時は null</returns>
    Task<GoogleTokenPayload?> ValidateTokenAsync(string idToken, CancellationToken cancellationToken = default);
}