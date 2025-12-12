using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using HouseholdBudget.Defines.Enums;

namespace HouseholdBudget.Presentation.DTOs.Transactions;

/// <summary>
/// 取引更新リクエスト
/// </summary>
public record UpdateTransactionRequest
{
    [Required]
    public Guid Id { get; init; }

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
    public Guid CategoryId { get; init; }

    [StringLength(500)]
    public string? Memo { get; init; }

    [StringLength(100)]
    public string? Place { get; init; }
}
