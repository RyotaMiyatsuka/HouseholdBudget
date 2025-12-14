using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;
using HouseholdBudget.Core.Application.Transactions.Results;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// 取引登録ユースケースのインタフェース
/// </summary>
public interface ICreateTransactionUseCase
{
    Task<UseCaseResult<TransactionResultData>> ExecuteAsync(CreateTransactionCommand command, CancellationToken cancellationToken = default);
}
