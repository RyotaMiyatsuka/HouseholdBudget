using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Core.Application.Transactions.Commands;

/// <summary>
/// 取引登録コマンド
/// </summary>
public record CreateTransactionCommand(
    decimal Amount,
    string Currency,
    DateOnly Date,
    TransactionType TransactionType,
    Guid CategoryId,
    string? Memo,
    string? Place);
