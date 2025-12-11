using System.ComponentModel.DataAnnotations;

namespace HouseholdBudget.Presentation.DTOs.Transactions;

/// <summary>
/// カテゴリ登録リクエスト
/// </summary>
public record CreateCategoryRequest
{
    [Required]
    [StringLength(50)]
    public string CategoryName { get; init; } = null!;
}
