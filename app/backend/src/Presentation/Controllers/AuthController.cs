using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Presentation.ApiModels.Users;

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

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        try
        {
            // UseCase 実行
            GoogleAuthCommand command = new GoogleAuthCommand
            {
                // TODO: マッピング処理の実装
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
