using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Results;
using HouseholdBudget.Core.Application.Common.Models;

namespace HouseholdBudget.Core.Application.Auth.Interfaces;

/// <summary>
/// 認証用ユースケースインターフェース
/// </summary>
public interface IAuthUseCase
{
    /// <summary>
    /// Google認証ログイン処理
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    Task<UseCaseResult<GoogleAuthResultData>> LoginWithGoogleAsync(GoogleAuthCommand command);
}
