using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Domain.Auth.Interfaces;
using Microsoft.AspNetCore.Http;

namespace HouseholdBudget.Infrastructure.Services;

/// <summary>
/// 現在のユーザー情報を取得するサービスの実装
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISessionRepository _sessionRepository;
    private Guid? _cachedUserId;
    private bool _isUserIdCached;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, ISessionRepository sessionRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _sessionRepository = sessionRepository;
    }

    public Guid? UserId
    {
        get
        {
            if (_isUserIdCached)
                return _cachedUserId;

            _cachedUserId = GetUserIdFromSession();
            _isUserIdCached = true;
            return _cachedUserId;
        }
    }

    public bool IsAuthenticated => UserId.HasValue;

    private Guid? GetUserIdFromSession()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return null;

        // セッションからユーザーIDを取得
        var userIdString = httpContext.Session.GetString("UserId");
        if (!string.IsNullOrEmpty(userIdString) && Guid.TryParse(userIdString, out var userId))
        {
            return userId;
        }

        return null;
    }
}
