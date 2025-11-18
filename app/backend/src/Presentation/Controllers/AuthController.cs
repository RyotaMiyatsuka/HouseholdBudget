using System.Security.Claims;

using Google.Apis.Auth;

using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Presentation.ApiModels.Users;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;
    private readonly IConfiguration _configuration;
    public AuthController(IAuthUseCase authUseCase, IConfiguration configuration)
    {
        _authUseCase = authUseCase;
        _configuration = configuration;
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        try
        {
            // UseCase 実行
            GoogleAuthCommand command = new GoogleAuthCommand
            {
            };
            var result = await _authUseCase.LoginWithGoogleAsync(command);

            // if (!result.IsSuccess || result.User == null)
            // {
            //     return BadRequest(new { message = result.ErrorMessage });
            // }

            // 認証用 Claims の作成
            var claims = new List<Claim>
            {
                // new Claim(ClaimTypes.NameIdentifier, result.User.UserId),
                // new Claim(ClaimTypes.Email, result.User.Email),
                // new Claim(ClaimTypes.Name, result.User.Name),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };

            // Cookie の発行
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok();
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
