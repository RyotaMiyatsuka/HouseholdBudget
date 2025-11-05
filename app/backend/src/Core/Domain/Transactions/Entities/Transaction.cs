using HouseholdBudget.Core.Domain.Transactions.ValueObjects;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Domain.Transactions.Entities;

/// <summary>
/// 取引エンティティ
/// </summary>
public class Transaction
{
    /// <summary>
    /// Id.
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// 取引を記録したユーザーのログインId.
    /// </summary>
    public LoginId UserId { get; private set; }
    /// <summary>
    /// csv取り込みした取引と紐づく
    /// </summary>
    public string? CsvImportId { get; private set; }
    /// <summary>
    /// 定期取引と紐づく
    /// </summary>
    public string? RecurringTransactionId { get; private set; }
    /// <summary>
    /// 取引額
    /// </summary>
    public Price Price { get; set; }
    /// <summary>
    /// 取引種別
    /// </summary>
    public string TransactionType { get; set; }
    /// <summary>
    /// 取引カテゴリ
    /// </summary>
    public string CategoryId { get; set; }
    /// <summary>
    /// 取引に付属するメモ
    /// </summary>
    public string Memo { get; set; }
    /// <summary>
    /// 取引の場所
    /// </summary>
    public string Place { get; set; }
    /// <summary>
    /// 削除フラグ
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// コンストラクタ (ユニークキー自動生成)
    /// </summary>
    public Transaction(
        LoginId userId,
        Price price,
        string categoryId,
        string memo,
        string place,
        string transactionType,
        string? csvImportId = null,
        string? recurringTransactionId = null,
        bool isDeleted = false
    )
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = userId;
        this.Price = price;
        this.TransactionType = transactionType;
        this.CsvImportId = csvImportId;
        this.RecurringTransactionId = recurringTransactionId;
        this.CategoryId = categoryId;
        this.Memo = memo;
        this.Place = place;
        this.IsDeleted = isDeleted;
    }

    /// <summary>
    /// コンストラクタ (ユニークキー指定時)
    /// </summary>
    public Transaction(
        string id,
        LoginId userId,
        Price price,
        string categoryId,
        string memo,
        string place,
        string transactionType,
        string? csvImportId = null,
        string? recurringTransactionId = null,
        bool isDeleted = false
    )
    {
        this.Id = id;
        this.UserId = userId;
        this.Price = price;
        this.TransactionType = transactionType;
        this.CsvImportId = csvImportId;
        this.RecurringTransactionId = recurringTransactionId;
        this.CategoryId = categoryId;
        this.Memo = memo;
        this.Place = place;
        this.IsDeleted = isDeleted;
    }
}
