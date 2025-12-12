using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Core.Application.Transactions.Results;

/// <summary>
/// 取引結果データ
/// </summary>
public record TransactionResultData(
    Guid Id,
    decimal Amount,
    Currency Currency,
    DateOnly Date,
    TransactionType TransactionType,
    string CategoryName,
    string? Memo,
    string? Place);
