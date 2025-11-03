
namespace HouseholdBudget.Core.Application.Transactions.Dto;

/// <summary>
/// 単一の取引登録DTO
/// </summary>
public record RegisterTransactionDto
{
    /// <summary>
    /// 取引を記録したユーザーのログインId.
    /// </summary>
    public required string UserId { get; set; }
    /// <summary>
    /// 取引額
    /// </summary>
    public required int Price { get; set; }
    /// <summary>
    /// 取引種別
    /// </summary>
    public required string Type { get; set; }
    /// <summary>
    /// 取引カテゴリ
    /// </summary>
    public string? Category { get; set; }
    /// <summary>
    /// 取引に付属するメモ
    /// </summary>
    public string? Memo { get; set; }
    /// <summary>
    /// 取引の場所
    /// </summary>
    public string? Place { get; set; }
}
