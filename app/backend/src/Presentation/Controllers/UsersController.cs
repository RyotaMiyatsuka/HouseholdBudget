using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Users.Commands;
using HouseholdBudget.Core.Application.Users.Interfaces;
using HouseholdBudget.Presentation.DTOs.Users;
using HouseholdBudget.Presentation.Filters;

using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudget.Presentation.Controllers;

/// <summary>
/// ユーザーコントローラー
/// </summary>
[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly ICreateUserUseCase _createUserUseCase;

    public UsersController(ICreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    /// <summary>
    /// ユーザー登録
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var authorization = Request.Headers.Authorization.ToString();
        string? idToken = null;
        if (!string.IsNullOrWhiteSpace(authorization))
        {
            idToken = authorization.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
        }

        var command = new CreateUserCommand(request.Email, request.UserName, idToken);
        var result = await _createUserUseCase.ExecuteAsync(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.ErrorType switch
            {
                UseCaseErrorType.Unauthorized => Unauthorized(result.ErrorMessage),
                UseCaseErrorType.Conflict => Conflict(result.ErrorMessage),
                UseCaseErrorType.Validation => UnprocessableEntity(result.ErrorMessage),
                _ => BadRequest(result.ErrorMessage)
            };
        }

        // セッションにユーザー情報を保存
        HttpContext.Session.SetString("UserId", result.Data!.UserId.ToString());
        if (!string.IsNullOrEmpty(result.Data.SessionId))
        {
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
        }

        return StatusCode(StatusCodes.Status201Created);
    }

    /// <summary>
    /// ユーザー情報取得 (未実装)
    /// </summary>
    [HttpGet("me")]
    [SessionAuthorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetUser()
    {
        // TODO: 実装予定
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// ユーザー削除 (未実装)
    /// </summary>
    [HttpDelete]
    [SessionAuthorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult DeleteUser()
    {
        // TODO: 実装予定
        return StatusCode(StatusCodes.Status501NotImplemented);
    }

    /// <summary>
    /// ユーザー情報更新 (未実装)
    /// </summary>
    [HttpPatch]
    [SessionAuthorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult UpdateUser()
    {
        // TODO: 実装予定
        return StatusCode(StatusCodes.Status501NotImplemented);
    }
}
