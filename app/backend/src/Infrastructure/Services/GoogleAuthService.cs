using Google.Apis.Auth;
using HouseholdBudget.Core.Domain.Auth.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HouseholdBudget.Infrastructure.Services;

/// <summary>
/// Google認証サービスの実装
/// </summary>
public class GoogleAuthService : IGoogleAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<GoogleAuthService> _logger;

    public GoogleAuthService(IConfiguration configuration, ILogger<GoogleAuthService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string?> ValidateTokenAndGetEmailAsync(string idToken, CancellationToken cancellationToken = default)
    {
        try
        {
            var clientId = _configuration["Authentication:Google:ClientId"];

            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = string.IsNullOrEmpty(clientId) ? null : [clientId]
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            if (payload == null)
            {
                _logger.LogWarning("Google token validation failed: payload is null");
                return null;
            }

            if (!payload.EmailVerified)
            {
                _logger.LogWarning("Google token validation failed: email not verified");
                return null;
            }

            return payload.Email;
        }
        catch (InvalidJwtException ex)
        {
            _logger.LogWarning(ex, "Invalid Google JWT token");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating Google token");
            return null;
        }
    }
}
