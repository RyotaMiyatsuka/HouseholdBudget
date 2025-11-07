
namespace HouseholdBudget.Core.Application.Transactions.Dto;

/// <summary>
/// 単一の取引登録DTO
/// </summary>
public record RegisterTransactionDto
{
    /// <summary>
    /// 取引を記録したユーザーのId.
    /// </summary>
    public required string UserId { get; set; }
    /// <summary>
    /// 取引を記録したユーザーのログインId.
    /// </summary>
    public required string LoginId { get; set; }
    /// <summary>
    /// 取引額
    /// </summary>
    public required decimal Amount { get; set; }
    /// <summary>
    /// 通貨
    /// </summary>
    public required string Currency { get; set; }
    /// <summary>
    /// 取引日付
    /// </summary>
    public required string Date { get; set; }
    /// <summary>
    /// 取引種別
    /// </summary>
    public required string TransactionType { get; set; }
    /// <summary>
    /// 取引カテゴリID
    /// </summary>
    public required string CategoryId { get; set; }
    /// <summary>
    /// 取引に付属するメモ
    /// </summary>
    public required string Memo { get; set; }
    /// <summary>
    /// 取引の場所
    /// </summary>
    public required string Place { get; set; }
}
