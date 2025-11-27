using HouseholdBudget.Core.Application.Auth.Interfaces;

using Microsoft.AspNetCore.Http;

namespace HouseholdBudget.Infrastructure.Services;

public class SessionService : ISessionService
{
    /// <summary>
    /// HttpContextAccessor のインスタンス
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;
    /// <summary>
    /// セッションキー: LoginId
    /// </summary>
    private const string LoginIdKey = "LoginId";

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <summary>
    /// HttpContext からセッションを取得
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private ISession GetSession()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }
        return httpContext.Session;
    }

    public Task SetLoginIdAsync(string loginId, CancellationToken cancellationToken = default)
    {
        GetSession().SetString(LoginIdKey, loginId);
        return Task.CompletedTask;
    }

    public Task<string?> GetLoginIdAsync(CancellationToken cancellationToken = default)
    {
        var loginId = GetSession().GetString(LoginIdKey);
        return Task.FromResult(loginId);
    }

    public Task ClearSessionAsync(CancellationToken cancellationToken = default)
    {
        GetSession().Clear();
        return Task.CompletedTask;
    }
}
