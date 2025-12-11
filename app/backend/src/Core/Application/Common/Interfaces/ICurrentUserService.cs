namespace HouseholdBudget.Core.Application.Common.Interfaces;

/// <summary>
/// 現在のユーザー情報を取得するサービスのインタフェース
/// </summary>
public interface ICurrentUserService
{
    Guid? UserId { get; }
    bool IsAuthenticated { get; }
}
