using HouseholdBudget.Core.Application.Common.Models;

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
    protected IActionResult HandleError<T>(UseCaseResult<T> result)
    {
        return result.ErrorType switch
        {
            UseCaseErrorType.Unauthorized => Unauthorized(result.ErrorMessage),
            UseCaseErrorType.NotFound => NotFound(result.ErrorMessage),
            UseCaseErrorType.Conflict => Conflict(result.ErrorMessage),
            UseCaseErrorType.Validation => BadRequest(result.ErrorMessage),
            _ => BadRequest(result.ErrorMessage)
        };
    }

    /// <summary>
    /// UseCaseResult のエラーを適切な HTTP レスポンスに変換する
    /// </summary>
    protected IActionResult HandleError(UseCaseResult result)
    {
        return result.ErrorType switch
        {
            UseCaseErrorType.Unauthorized => Unauthorized(result.ErrorMessage),
            UseCaseErrorType.NotFound => NotFound(result.ErrorMessage),
            UseCaseErrorType.Conflict => Conflict(result.ErrorMessage),
            UseCaseErrorType.Validation => BadRequest(result.ErrorMessage),
            _ => BadRequest(result.ErrorMessage)
        };
    }
}
