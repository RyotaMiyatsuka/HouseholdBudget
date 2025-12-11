using System.ComponentModel.DataAnnotations;

namespace HouseholdBudget.Presentation.DTOs.Transactions;

/// <summary>
/// カテゴリ更新リクエスト
/// </summary>
public record UpdateCategoryRequest
{
    [Required]
    public Guid CategoryId { get; init; }

    [Required]
    [StringLength(50)]
    public string CategoryName { get; init; } = null!;
}
