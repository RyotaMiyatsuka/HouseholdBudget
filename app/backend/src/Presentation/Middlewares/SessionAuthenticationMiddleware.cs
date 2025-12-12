using HouseholdBudget.Core.Domain.Auth.Interfaces;

namespace HouseholdBudget.Presentation.Middlewares;

/// <summary>
/// セッションベースの認証ミドルウェア
/// Cookie の sessionId を検証し、HttpContext.Session にユーザー情報を設定する
/// </summary>
public class SessionAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public SessionAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ISessionRepository sessionRepository)
    {
        // 既にセッションにUserIdがある場合はスキップ
        var existingUserId = context.Session.GetString("UserId");
        if (!string.IsNullOrEmpty(existingUserId))
        {
            await _next(context);
            return;
        }

        // Cookie から sessionId を取得
        var sessionId = context.Request.Cookies["sessionId"];
        if (string.IsNullOrEmpty(sessionId))
        {
            await _next(context);
            return;
        }

        // DB からセッションを取得して検証
        var session = await sessionRepository.GetByIdAsync(sessionId);
        if (session != null && session.IsValid())
        {
            // HttpContext.Session にユーザー情報を設定
            context.Session.SetString("UserId", session.UserId.ToString());
            context.Session.SetString("SessionId", session.Id);
        }

        await _next(context);
    }
}

/// <summary>
/// ミドルウェア登録用の拡張メソッド
/// </summary>
public static class SessionAuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseSessionAuthentication(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SessionAuthenticationMiddleware>();
    }
}
