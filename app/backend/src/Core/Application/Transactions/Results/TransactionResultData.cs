using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Core.Application.Transactions.Results;

/// <summary>
/// 取引結果データ
/// </summary>
public record TransactionResultData(
    Guid Id,
    decimal Amount,
    string Currency,
    DateOnly Date,
    TransactionType TransactionType,
    Guid CategoryId,
    string? Memo,
    string? Place);
