
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HouseholdBudget.Infrastructure.EFCore.EFEntities;
/// <summary>
/// transactions
/// </summary>
public class Transactions
{
    public string Id { get; set; }

    /// <summary>
    /// 取引を記録したユーザー
    /// </summary>
    [Required]
    [ForeignKey(nameof(Users))]
    public string UserId { get; set; }

    /// <summary>
    /// csv取り込みした取引と紐づく
    /// </summary>
    [ForeignKey(nameof(CsvImports))]
    public string? CsvImportId { get; set; }

    /// <summary>
    /// 定期取引と紐づく
    /// </summary>
    [ForeignKey(nameof(RecurringTransactions))]
    public string? RecurringTransactionId { get; set; }

    /// <summary>
    /// 取引額
    /// </summary>
    [Required]
    public int Price { get; set; }

    /// <summary>
    /// 取引種別
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Type { get; set; }

    /// <summary>
    /// 取引カテゴリ
    /// </summary>
    [MaxLength(100)]
    public string? Category { get; set; }

    /// <summary>
    /// 取引に付属するメモ
    /// </summary>
    public string? Memo { get; set; }

    /// <summary>
    /// 取引の場所
    /// </summary>
    [MaxLength(200)]
    public string? Place { get; set; }

    /// <summary>
    /// 削除フラグ
    /// </summary>
    [Required]
    public bool IsDeleted { get; set; }

    public virtual Users User { get; set; }
    public virtual CsvImports? CsvImport { get; set; }
    public virtual RecurringTransactions? RecurringTransaction { get; set; }
}
