using System.Text.Json.Serialization;

namespace HouseholdBudget.Presentation.DTOs.Transactions;

/// <summary>
/// 取引レスポンス
/// </summary>
public record TransactionResponse(
    Guid Id,
    decimal Amount,
    string Currency,
    string Date,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    TransactionTypeDto TransactionType,
    Guid CategoryId,
    string? Memo,
    string? Place);

/// <summary>
/// 取引種別DTO
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionTypeDto
{
    [JsonPropertyName("income")]
    Income,
    [JsonPropertyName("expense")]
    Expense
}
