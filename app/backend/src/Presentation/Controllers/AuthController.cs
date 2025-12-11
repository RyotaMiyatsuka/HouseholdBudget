using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Presentation.ApiModels.Users;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;
    private readonly ISessionService _sessionService;

    public AuthController(IAuthUseCase authUseCase, ISessionService sessionService)
    {
        _authUseCase = authUseCase;
        _sessionService = sessionService;
    }

    [Authorize]
    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        try
        {
            // Authorizationヘッダーから取得
            if (!Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                // TODO: メッセージ共通化
                return Unauthorized(new { message = "Authorization header is missing" });
            }

            var authHeaderValue = authHeader.ToString();

            // "Bearer " プレフィックスを除去
            if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                // TODO: メッセージ共通化
                return Unauthorized(new { message = "Invalid authorization header format" });
            }

            var idToken = authHeaderValue["Bearer ".Length..].Trim();

            // UseCase 実行
            GoogleAuthCommand command = new GoogleAuthCommand
            {
                IdToken = idToken
            };
            var result = await _authUseCase.LoginWithGoogleAsync(command);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.ErrorMessage });
            }

            // セッションにLoginIdを保存
            await _sessionService.SetLoginIdAsync(result.Data.LoginId);

            return Ok(new { message = "Login successful" });
        }
        catch (Exception ex)
        {
            // Proper logging should be implemented here
            return StatusCode(500, new { error = "Authentication failed" });
        }
    }
}
