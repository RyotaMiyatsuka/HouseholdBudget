
using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Application.Auth.Results;

namespace HouseholdBudget.Core.Application.Auth.UseCases;

/// <summary>
/// 認証用ユースケースインターフェース
/// </summary>
public class AuthUseCase : IAuthUseCase
{
    public Task<GoogleAuthResultData> LoginWithGoogleAsync(GoogleAuthCommand command)
    {
        throw new NotImplementedException();
    }
}
