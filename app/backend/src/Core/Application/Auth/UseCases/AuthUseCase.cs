
using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Application.Auth.Results;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Application.Auth.UseCases;

/// <summary>
/// 認証用ユースケースインターフェース
/// </summary>
public class AuthUseCase : IAuthUseCase
{
    public Task<UseCaseResult<GoogleAuthResultData>> LoginWithGoogleAsync(GoogleAuthCommand command)
    {
        // TODO: 実装
        // トークン検証
        // ユーザーの作成・取得
        // 結果の返却
        string loginId = "sample-login-id";
        return Task.FromResult(new UseCaseResult<GoogleAuthResultData>
        {
            IsSuccess = true,
            Data = new GoogleAuthResultData { LoginId = loginId },
        });
    }
}
