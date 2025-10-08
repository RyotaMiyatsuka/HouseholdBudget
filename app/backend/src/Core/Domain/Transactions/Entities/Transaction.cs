using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Domain.Transactions.Entities;

/// <summary>
/// 取引エンティティ
/// </summary>
/// <param name="id"></param>
/// <param name="loginId"></param>
/// <param name="userName"></param>
/// <param name="mailAddress"></param>
/// <param name="isVerified"></param>
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
    /// ユーザー名を変更する
    /// </summary>
    /// <param name="userName">変更後のユーザー名</param>
    public void ChangeUserName(string userName)
    {
        this.UserName = new UserName(userName);
    }

    /// <summary>
    /// ログインIdを変更する
    /// </summary>
    /// <param name="loginId"></param>
    public void ChangeLoginId(string loginId)
    {
        this.LoginId = new LoginId(loginId);
    }

    /// <summary>
    /// メールアドレスを変更する
    /// </summary>
    /// <param name="loginId"></param>
    public void ChangeMailAddress(string mailAddress)
    {
        this.MailAddress = mailAddress;
        this.IsVerified = false; // メールアドレス変更後は再検証が必要
    }

    /// <summary>
    /// 認証済みユーザーに変更する
    /// </summary>
    public void Verify()
    {
        this.IsVerified = true;
    }
}
