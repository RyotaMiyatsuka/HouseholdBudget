using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Users.Commands;
using HouseholdBudget.Core.Application.Users.Interfaces;
using HouseholdBudget.Core.Application.Users.Results;
using HouseholdBudget.Core.Domain.Auth.Entities;
using HouseholdBudget.Core.Domain.Auth.Interfaces;
using HouseholdBudget.Core.Domain.Common.Interfaces;
using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Application.Users.UseCases;

/// <summary>
/// ユーザー登録ユースケースの実装
/// </summary>
public class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGoogleAuthService _googleAuthService;

    public CreateUserUseCase(IUnitOfWork unitOfWork, IGoogleAuthService googleAuthService)
    {
        _unitOfWork = unitOfWork;
        _googleAuthService = googleAuthService;
    }

    public async Task<UseCaseResult<UserResultData>> ExecuteAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        string email = command.Email;

        // Google IDトークンが提供された場合は検証してメールを取得
        if (!string.IsNullOrEmpty(command.IdToken))
        {
            var googleEmail = await _googleAuthService.ValidateTokenAndGetEmailAsync(command.IdToken, cancellationToken);
            if (string.IsNullOrEmpty(googleEmail))
            {
                return UseCaseResult<UserResultData>.Unauthorized("Invalid Google token.");
            }
            email = googleEmail;
        }

        // メールアドレスのValueObjectを作成
        Email emailVo;
        try
        {
            emailVo = Email.Create(email);
        }
        catch (Exception ex)
        {
            return UseCaseResult<UserResultData>.ValidationError(ex.Message);
        }

        // 既存ユーザーの確認
        if (await _unitOfWork.Users.ExistsByEmailAsync(emailVo, cancellationToken))
        {
            return UseCaseResult<UserResultData>.Conflict("User with this email already exists.");
        }

        // ユーザー名のValueObjectを作成
        UserName userNameVo;
        try
        {
            userNameVo = UserName.Create(command.UserName);
        }
        catch (Exception ex)
        {
            return UseCaseResult<UserResultData>.ValidationError(ex.Message);
        }

        // ユーザーを作成
        var user = User.Create(emailVo, userNameVo);
        await _unitOfWork.Users.AddAsync(user, cancellationToken);

        // セッションを作成
        var session = Session.Create(user.Id);
        await _unitOfWork.Sessions.AddAsync(session, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return UseCaseResult<UserResultData>.Success(
            new UserResultData(user.Id, emailVo.Value, userNameVo.Value, session.Id));
    }
}
