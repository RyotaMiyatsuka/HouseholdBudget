using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Results;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// 月別取引取得ユースケースのインタフェース
/// </summary>
public interface IGetTransactionsByMonthUseCase
{
    Task<UseCaseResult<IEnumerable<TransactionResultData>>> ExecuteAsync(GetTransactionsByMonthCommand command, CancellationToken cancellationToken = default);
}
