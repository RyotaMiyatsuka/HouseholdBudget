using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Presentation.Filters;

using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudget.Presentation.Controllers;

/// <summary>
/// 認証コントローラー
/// </summary>
[Route("auth")]
public class AuthController : AppControllerBase
{
    private readonly IGoogleLoginUseCase _googleLoginUseCase;
    private readonly ILogoutUseCase _logoutUseCase;

    public AuthController(IGoogleLoginUseCase googleLoginUseCase, ILogoutUseCase logoutUseCase)
    {
        _googleLoginUseCase = googleLoginUseCase;
        _logoutUseCase = logoutUseCase;
    }

    /// <summary>
    /// Googleログイン
    /// </summary>
    [HttpPost("google-login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GoogleLogin(CancellationToken cancellationToken)
    {
        var authorization = Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authorization))
        {
            return Unauthorized();
        }

        var token = authorization.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }

        var result = await _googleLoginUseCase.ExecuteAsync(new GoogleLoginCommand(token), cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        // セッションにユーザー情報を保存
        HttpContext.Session.SetString("UserId", result.Data!.UserId.ToString());
        HttpContext.Session.SetString("SessionId", result.Data.SessionId);

        // セッションIDをCookieに設定
        Response.Cookies.Append("sessionId", result.Data.SessionId, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddHours(24)
        });

        return Ok();
    }

    /// <summary>
    /// ログアウト
    /// </summary>
    [HttpPost("logout")]
    [SessionAuthorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        var sessionId = HttpContext.Session.GetString("SessionId");
        if (string.IsNullOrEmpty(sessionId))
        {
            // Cookieからも試行
            sessionId = Request.Cookies["sessionId"];
        }

        if (string.IsNullOrEmpty(sessionId))
        {
            return Unauthorized();
        }

        var result = await _logoutUseCase.ExecuteAsync(new LogoutCommand(sessionId), cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        // セッションをクリア
        HttpContext.Session.Clear();

        // Cookieを削除
        Response.Cookies.Delete("sessionId");

        return NoContent();
    }
}
