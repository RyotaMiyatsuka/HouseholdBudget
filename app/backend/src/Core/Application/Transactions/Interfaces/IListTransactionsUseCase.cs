using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Results;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// 取引一覧取得ユースケースのインタフェース
/// </summary>
public interface IListTransactionsUseCase
{
    Task<UseCaseResult<IEnumerable<TransactionResultData>>> ExecuteAsync(CancellationToken cancellationToken = default);
}
