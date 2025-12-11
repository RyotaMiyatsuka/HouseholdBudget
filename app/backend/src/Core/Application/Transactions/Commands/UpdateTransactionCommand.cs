using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Core.Application.Transactions.Commands;

/// <summary>
/// 取引更新コマンド
/// </summary>
public record UpdateTransactionCommand(
    Guid Id,
    decimal Amount,
    string Currency,
    DateOnly Date,
    TransactionType TransactionType,
    Guid CategoryId,
    string? Memo,
    string? Place);
