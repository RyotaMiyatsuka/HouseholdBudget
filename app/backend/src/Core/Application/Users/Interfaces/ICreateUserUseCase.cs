using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Users.Commands;
using HouseholdBudget.Core.Application.Users.Results;

namespace HouseholdBudget.Core.Application.Users.Interfaces;

/// <summary>
/// ユーザー登録ユースケースのインタフェース
/// </summary>
public interface ICreateUserUseCase
{
    Task<UseCaseResult<UserResultData>> ExecuteAsync(CreateUserCommand command, CancellationToken cancellationToken = default);
}
