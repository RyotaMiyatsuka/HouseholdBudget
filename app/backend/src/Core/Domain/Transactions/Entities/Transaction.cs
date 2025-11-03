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
    public int Price { get; set; }
    /// <summary>
    /// 取引種別
    /// </summary>
    public string Type { get; set; }
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
    /// <summary>
    /// 削除フラグ
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// コンストラクタ (ユニークキー自動生成)
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="price"></param>
    /// <param name="type"></param>
    /// <param name="csvImportId"></param>
    /// <param name="recurringTransactionId"></param>
    /// <param name="category"></param>
    /// <param name="memo"></param>
    /// <param name="place"></param>
    /// <param name="isDeleted"></param>
    public Transaction(
        LoginId userId,
        int price,
        string type,
        string? csvImportId = null,
        string? recurringTransactionId = null,
        string? category = null,
        string? memo = null,
        string? place = null,
        bool isDeleted = false
    )
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = userId;
        this.Price = price;
        this.Type = type;
        this.CsvImportId = csvImportId;
        this.RecurringTransactionId = recurringTransactionId;
        this.Category = category;
        this.Memo = memo;
        this.Place = place;
        this.IsDeleted = isDeleted;
    }

    /// <summary>
    /// コンストラクタ (ユニークキー指定時)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    /// <param name="price"></param>
    /// <param name="type"></param>
    /// <param name="csvImportId"></param>
    /// <param name="recurringTransactionId"></param>
    /// <param name="category"></param>
    /// <param name="memo"></param>
    /// <param name="place"></param>
    /// <param name="isDeleted"></param>
    public Transaction(
        string id,
        LoginId userId,
        int price,
        string type,
        string? csvImportId = null,
        string? recurringTransactionId = null,
        string? category = null,
        string? memo = null,
        string? place = null,
        bool isDeleted = false
    )
    {
        this.Id = id;
        this.UserId = userId;
        this.Price = price;
        this.Type = type;
        this.CsvImportId = csvImportId;
        this.RecurringTransactionId = recurringTransactionId;
        this.Category = category;
        this.Memo = memo;
        this.Place = place;
        this.IsDeleted = isDeleted;
    }
}
