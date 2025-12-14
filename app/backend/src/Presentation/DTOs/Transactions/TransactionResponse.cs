using System.Text.Json.Serialization;

using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Presentation.DTOs.Transactions;

/// <summary>
/// 取引レスポンス
/// </summary>
public record TransactionResponse(
    Guid Id,
    decimal Amount,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    Currency Currency,
    string Date,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    TransactionType TransactionType,
    string CategoryName,
    string? Memo,
    string? Place);
