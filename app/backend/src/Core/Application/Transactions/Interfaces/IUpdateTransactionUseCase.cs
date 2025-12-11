using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Results;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// 取引更新ユースケースのインタフェース
/// </summary>
public interface IUpdateTransactionUseCase
{
    Task<UseCaseResult<TransactionResultData>> ExecuteAsync(UpdateTransactionCommand command, CancellationToken cancellationToken = default);
}
