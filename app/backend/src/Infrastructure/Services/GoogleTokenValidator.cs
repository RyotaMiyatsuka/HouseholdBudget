using Google.Apis.Auth;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HouseholdBudget.Infrastructure.Services;

/// <summary>
/// Google トークン検証サービス実装
/// </summary>
public class GoogleTokenValidator : IGoogleTokenValidator
{
    private readonly string? _googleClientId;

    public GoogleTokenValidator(IConfiguration configuration)
    {
        _googleClientId = configuration["Authentication:Google:ClientId"];
    }

    /// <inheritdoc />
    public async Task<GoogleTokenPayload?> ValidateTokenAsync(string idToken, CancellationToken cancellationToken = default)
    {
        try
        {
            // Google の公開鍵を使用してトークンを検証
            var validationSettings = new GoogleJsonWebSignature.ValidationSettings
            {
                // クライアントIDが設定されている場合のみ検証
                Audience = !string.IsNullOrEmpty(_googleClientId)
                    ? new[] { _googleClientId }
                    : null
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, validationSettings);

            if (payload == null)
            {
                return null;
            }

            // ペイロードを変換して返却
            return new GoogleTokenPayload
            {
                Subject = payload.Subject,
                Email = payload.Email,
                EmailVerified = payload.EmailVerified,
                Name = payload.Name,
                Picture = payload.Picture
            };
        }
        catch (InvalidJwtException)
        {
            // トークンが無効な場合
            return null;
        }
        catch (Exception)
        {
            // その他のエラー(ネットワークエラー等)
            throw;
        }
    }
}