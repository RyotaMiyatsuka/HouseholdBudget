using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Results;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// カテゴリ更新ユースケースのインタフェース
/// </summary>
public interface IUpdateCategoryUseCase
{
    Task<UseCaseResult<CategoryResultData>> ExecuteAsync(UpdateCategoryCommand command, CancellationToken cancellationToken = default);
}
