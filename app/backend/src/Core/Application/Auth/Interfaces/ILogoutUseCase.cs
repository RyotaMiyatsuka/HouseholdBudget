using HouseholdBudget.Core.Application.Auth.Commands;
using HouseholdBudget.Core.Application.Common.Models;

namespace HouseholdBudget.Core.Application.Auth.Interfaces;

/// <summary>
/// ログアウトユースケースのインタフェース
/// </summary>
public interface ILogoutUseCase
{
    Task<UseCaseResult> ExecuteAsync(LogoutCommand command, CancellationToken cancellationToken = default);
}
