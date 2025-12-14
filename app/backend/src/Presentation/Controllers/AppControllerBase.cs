using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Presentation.Middlewares;

using Microsoft.AspNetCore.Mvc;

namespace HouseholdBudget.Presentation.Controllers;

/// <summary>
/// APIコントローラーの基底クラス
/// </summary>
[ApiController]
public abstract class AppControllerBase : ControllerBase
{
    /// <summary>
    /// UseCaseResult のエラーを適切な HTTP レスポンスに変換する
    /// </summary>
    protected IActionResult HandleUseCaseError<T>(UseCaseResult<T> result)
    {
        return result.ErrorType switch
        {
            UseCaseErrorType.Unauthorized => CreateErrorResponse(StatusCodes.Status401Unauthorized, "Unauthorized", result.ErrorMessage),
            UseCaseErrorType.NotFound => CreateErrorResponse(StatusCodes.Status404NotFound, "NotFound", result.ErrorMessage),
            UseCaseErrorType.Conflict => CreateErrorResponse(StatusCodes.Status409Conflict, "Conflict", result.ErrorMessage),
            UseCaseErrorType.Validation => CreateErrorResponse(StatusCodes.Status400BadRequest, "ValidationError", result.ErrorMessage),
            _ => CreateErrorResponse(StatusCodes.Status500InternalServerError, "InternalServerError", result.ErrorMessage)
        };
    }

    /// <summary>
    /// UseCaseResult のエラーを適切な HTTP レスポンスに変換する
    /// </summary>
    protected IActionResult HandleUseCaseError(UseCaseResult result)
    {
        return result.ErrorType switch
        {
            UseCaseErrorType.Unauthorized => CreateErrorResponse(StatusCodes.Status401Unauthorized, "Unauthorized", result.ErrorMessage),
            UseCaseErrorType.NotFound => CreateErrorResponse(StatusCodes.Status404NotFound, "NotFound", result.ErrorMessage),
            UseCaseErrorType.Conflict => CreateErrorResponse(StatusCodes.Status409Conflict, "Conflict", result.ErrorMessage),
            UseCaseErrorType.Validation => CreateErrorResponse(StatusCodes.Status400BadRequest, "ValidationError", result.ErrorMessage),
            _ => CreateErrorResponse(StatusCodes.Status500InternalServerError, "InternalServerError", result.ErrorMessage)
        };
    }

    /// <summary>
    /// 401 Unauthorized エラーレスポンスを返す
    /// </summary>
    protected static IActionResult UnauthorizedError(string message = "Unauthorized access.")
    {
        return CreateErrorResponse(StatusCodes.Status401Unauthorized, "Unauthorized", message);
    }

    /// <summary>
    /// 404 NotFound エラーレスポンスを返す
    /// </summary>
    protected static IActionResult NotFoundError(string message = "Resource not found.")
    {
        return CreateErrorResponse(StatusCodes.Status404NotFound, "NotFound", message);
    }

    /// <summary>
    /// 400 BadRequest エラーレスポンスを返す
    /// </summary>
    protected static IActionResult BadRequestError(string message = "Bad request.")
    {
        return CreateErrorResponse(StatusCodes.Status400BadRequest, "BadRequest", message);
    }

    /// <summary>
    /// 409 Conflict エラーレスポンスを返す
    /// </summary>
    protected static IActionResult ConflictError(string message = "Resource conflict.")
    {
        return CreateErrorResponse(StatusCodes.Status409Conflict, "Conflict", message);
    }

    private static JsonResult CreateErrorResponse(int statusCode, string code, string? message)
    {
        return new JsonResult(new ErrorResponse(code, message ?? "An error occurred."))
        {
            StatusCode = statusCode
        };
    }
}
