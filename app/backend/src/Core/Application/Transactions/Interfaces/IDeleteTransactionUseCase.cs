using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Application.Transactions.Commands;

namespace HouseholdBudget.Core.Application.Transactions.Interfaces;

/// <summary>
/// 取引削除ユースケースのインタフェース
/// </summary>
public interface IDeleteTransactionUseCase
{
    Task<UseCaseResult> ExecuteAsync(DeleteTransactionCommand command, CancellationToken cancellationToken = default);
}
