
using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Application.Auth.Results;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Application.Auth.UseCases;

/// <summary>
/// 認証用ユースケースインターフェース
/// </summary>
public class AuthUseCase(IGoogleTokenValidator googleTokenValidator) : IAuthUseCase
{
    public async Task<UseCaseResult<GoogleAuthResultData>> LoginWithGoogleAsync(GoogleAuthCommand command)
    {
        // Google ID トークンを検証
        var payload = await googleTokenValidator.ValidateTokenAsync(command.IdToken);

        if (payload == null)
        {
            return new UseCaseResult<GoogleAuthResultData>
            {
                IsSuccess = false,
                ErrorMessage = "Invalid Google ID token" // TODO: メッセージ共通化
            };
        }

        // メールアドレスが検証されていない場合はエラー
        if (!payload.EmailVerified)
        {
            return new UseCaseResult<GoogleAuthResultData>
            {
                IsSuccess = false,
                ErrorMessage = "Email not verified" // TODO: メッセージ共通化
            };
        }

        // TODO: ユーザーの作成・取得
        // Google Subject ID を使用してユーザーを検索または作成

        // 仮実装: Subject を LoginId として返却
        string loginId = payload.Subject;

        return new UseCaseResult<GoogleAuthResultData>
        {
            IsSuccess = true,
            Data = new GoogleAuthResultData { LoginId = loginId },
        };
    }
}
