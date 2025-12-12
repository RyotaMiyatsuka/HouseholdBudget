using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Application.Auth.Results;
using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Domain.Auth.Entities;
using HouseholdBudget.Core.Domain.Auth.Interfaces;
using HouseholdBudget.Core.Domain.Common.Interfaces;
using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Application.Auth.UseCases;

/// <summary>
/// Googleログインユースケースの実装
/// </summary>
public class GoogleLoginUseCase : IGoogleLoginUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGoogleAuthService _googleAuthService;

    public GoogleLoginUseCase(IUnitOfWork unitOfWork, IGoogleAuthService googleAuthService)
    {
        _unitOfWork = unitOfWork;
        _googleAuthService = googleAuthService;
    }

    public async Task<UseCaseResult<AuthResultData>> ExecuteAsync(GoogleLoginCommand command, CancellationToken cancellationToken = default)
    {
        // Google IDトークンを検証
        var email = await _googleAuthService.ValidateTokenAndGetEmailAsync(command.IdToken, cancellationToken);
        if (string.IsNullOrEmpty(email))
        {
            return UseCaseResult<AuthResultData>.Unauthorized("Invalid Google token.");
        }

        // メールアドレスでユーザーを検索
        var emailVo = Email.Create(email);
        var user = await _unitOfWork.Users.GetByEmailAsync(emailVo, cancellationToken);

        // ユーザーが存在しない場合は新規作成
        if (user == null)
        {
            var userName = UserName.Create(emailVo.GetLocalPart());
            user = User.Create(emailVo, userName);
            await _unitOfWork.Users.AddAsync(user, cancellationToken);
        }

        // 既存のアクティブなセッションがあれば無効化
        var existingSession = await _unitOfWork.Sessions.GetActiveByUserIdAsync(user.Id, cancellationToken);
        if (existingSession != null)
        {
            existingSession.Invalidate();
            _unitOfWork.Sessions.Update(existingSession);
        }

        // 新しいセッションを作成
        var session = Session.Create(user.Id);
        await _unitOfWork.Sessions.AddAsync(session, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult<AuthResultData>.Success(
            new AuthResultData(session.Id, user.Id, email));
    }
}
