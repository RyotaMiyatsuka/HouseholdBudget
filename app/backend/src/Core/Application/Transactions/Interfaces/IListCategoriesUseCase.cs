using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Results;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// カテゴリ一覧取得ユースケースのインタフェース
/// </summary>
public interface IListCategoriesUseCase
{
    Task<UseCaseResult<IEnumerable<CategoryResultData>>> ExecuteAsync(CancellationToken cancellationToken = default);
}
