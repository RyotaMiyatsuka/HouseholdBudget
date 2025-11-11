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

            // 存在確認
            if (string.IsNullOrWhiteSpace(idToken))
            {
                // _logger.LogWarning("Missing ID token in login attempt");
                return Unauthorized(new { error = "Authentication required" });
            }

            // ② Google トークン検証（厳格な設定）
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

            // ④ ユーザーの取得または作成 (UseCase呼び出し)
            // var user = await _userRepository.GetUserByEmailAsync(payload.Email);
            // if (user == null)
            // {
            //     user = new User
            //     {
            //         Email = payload.Email,
            //         Name = payload.Name ?? "Unknown",
            //         GoogleSubject = payload.Subject,
            //         CreatedAt = DateTime.UtcNow
            //     };
            //     await _userRepository.CreateUserAsync(user);
            //     _logger.LogInformation("New user registered: {Email}", payload.Email);
            // }
            // else
            // {
            //     user.LastLoginAt = DateTime.UtcNow;
            //     await _userRepository.UpdateUserAsync(user);
            // }

            // 自アプリ用の SessionId 発行
            // var token = GenerateJwtToken(user);

            // _logger.LogInformation("Successful login: {UserId}", user.Id);

            // ⑥ レスポンス（セッションではなくトークンベース）
            // return Ok(new
            // {
            //     token = token,
            //     user = new
            //     {
            //         id = user.Id,
            //         email = user.Email,
            //         name = user.Name
            //     }
            // });
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
