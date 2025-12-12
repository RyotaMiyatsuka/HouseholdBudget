using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Presentation.DTOs.Transactions;

/// <summary>
/// 取引登録リクエスト
/// </summary>
public record CreateTransactionRequest
{
    [Required]
    public decimal Amount { get; init; }

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Currency Currency { get; init; } = Currency.JPY;

    [Required]
    public string Date { get; init; } = null!;

    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TransactionType TransactionType { get; init; }

    [Required]
    [StringLength(100)]
    public string CategoryName { get; init; } = null!;

    [StringLength(500)]
    public string? Memo { get; init; }

    [StringLength(100)]
    public string? Place { get; init; }
}
