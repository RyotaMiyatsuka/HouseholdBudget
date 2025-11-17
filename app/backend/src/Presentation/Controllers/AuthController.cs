using Google.Apis.Auth;

using HouseholdBudget.Core.Presentation.ApiModels.Users;

using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        try
        {
            // ヘッダーからトークンを取得
            var idToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

            // 空文字確認
            if (string.IsNullOrWhiteSpace(idToken))
            {
                // TODO: ログ出力
                // _logger.LogWarning("Missing ID token in login attempt");
                return Unauthorized(new { error = "Authentication required" });
            }

            // Google トークン検証
            var googleClientId = _configuration["Authentication:Google:ClientId"];
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken,
                new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { googleClientId },
                    IssuedAtClockTolerance = TimeSpan.FromMinutes(5),
                    ExpirationTimeClockTolerance = TimeSpan.FromMinutes(5)
                });

            // ③ メールアドレスの検証
            if (string.IsNullOrEmpty(payload.Email) || !payload.EmailVerified)
            {
                // _logger.LogWarning("Unverified email attempt: {Email}", payload.Email);
                return Unauthorized(new { error = "Email verification required" });
            }
            return Ok(); // 仮実装
        }
        catch (InvalidJwtException ex)
        {
            // _logger.LogWarning(ex, "Invalid JWT token in login attempt");
            return Unauthorized(new { error = "Invalid credentials" });
        }
        catch (Exception ex)
        {
            // _logger.LogError(ex, "Error during Google login");
            return StatusCode(500, new { error = "Authentication failed" });
        }
    }
}
