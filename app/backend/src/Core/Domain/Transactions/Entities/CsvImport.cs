using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Domain.Transactions.Entities;

/// <summary>
/// CSV取り込みエンティティ
/// </summary>
public class CsvImport
{
    /// <summary>
    /// Id.
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// 取り込みを行ったユーザーのログインId.
    /// </summary>
    public LoginId UserId { get; private set; }

    /// <summary>
    /// コンストラクタ (ユニークキー自動生成)
    /// </summary>
    /// <param name="userId"></param>
    public CsvImport(string userId)
    {
        this.Id = Guid.NewGuid().ToString();
        this.UserId = new LoginId(userId);
    }

    /// <summary>
    /// コンストラクタ (ユニークキー指定時)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userId"></param>
    public CsvImport(string id, string userId)
    {
        this.Id = id;
        this.UserId = new LoginId(userId);
    }
}
