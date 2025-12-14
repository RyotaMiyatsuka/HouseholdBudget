using HouseholdBudget.Presentation.Middlewares;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HouseholdBudget.Presentation.Filters;

/// <summary>
/// セッションベースの認証を要求する属性
/// HttpContext.Session に UserId が存在しない場合は 401 を返す
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new JsonResult(new ErrorResponse("Unauthorized", "Unauthorized access."))
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}
