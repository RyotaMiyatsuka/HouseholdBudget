using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Core.Application.Transactions.Commands;

/// <summary>
/// 取引更新コマンド
/// </summary>
public record UpdateTransactionCommand(
    Guid Id,
    decimal Amount,
    Currency Currency,
    DateOnly Date,
    TransactionType TransactionType,
    string CategoryName,
    string? Memo,
    string? Place);
