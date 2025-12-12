using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Core.Application.Transactions.Commands;

/// <summary>
/// 取引登録コマンド
/// </summary>
public record CreateTransactionCommand(
    decimal Amount,
    Currency Currency,
    DateOnly Date,
    TransactionType TransactionType,
    string CategoryName,
    string? Memo,
    string? Place);
