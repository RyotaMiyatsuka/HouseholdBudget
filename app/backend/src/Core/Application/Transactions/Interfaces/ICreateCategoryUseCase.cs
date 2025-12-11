using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Results;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// カテゴリ登録ユースケースのインタフェース
/// </summary>
public interface ICreateCategoryUseCase
{
    Task<UseCaseResult<CategoryResultData>> ExecuteAsync(CreateCategoryCommand command, CancellationToken cancellationToken = default);
}
