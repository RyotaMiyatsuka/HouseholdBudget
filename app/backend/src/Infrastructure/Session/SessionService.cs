using System;
using System.Threading;
using System.Threading.Tasks;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HouseholdBudget.Infrastructure.Session;

public class SessionService : ISessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string LoginIdKey = "LoginId";

    public SessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

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
