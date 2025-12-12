using System.ComponentModel.DataAnnotations;

namespace HouseholdBudget.Presentation.DTOs.Transactions;

/// <summary>
/// カテゴリ更新リクエスト
/// </summary>
public record UpdateCategoryRequest
{
    [Required]
    [StringLength(50)]
    public string OldCategoryName { get; init; } = null!;

    [Required]
    [StringLength(50)]
    public string NewCategoryName { get; init; } = null!;
}
