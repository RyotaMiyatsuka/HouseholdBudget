using System.ComponentModel.DataAnnotations;

namespace HouseholdBudget.Presentation.DTOs.Users;

/// <summary>
/// ユーザー登録リクエスト
/// </summary>
public record CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required]
    [StringLength(100)]
    public string UserName { get; init; } = null!;
}
