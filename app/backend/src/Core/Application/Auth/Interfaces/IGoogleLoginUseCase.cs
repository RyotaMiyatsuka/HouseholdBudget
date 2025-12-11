using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Auth.Results;
using HouseholdBudget.Core.Application.Common.Models;

namespace HouseholdBudget.Core.Application.Auth.Interfaces;

/// <summary>
/// Googleログインユースケースのインタフェース
/// </summary>
public interface IGoogleLoginUseCase
{
    Task<UseCaseResult<AuthResultData>> ExecuteAsync(GoogleLoginCommand command, CancellationToken cancellationToken = default);
}
