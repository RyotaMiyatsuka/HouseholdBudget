using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Domain.Users.Entities;

/// <summary>
/// ユーザーエンティティ
/// </summary>
/// <param name="id"></param>
/// <param name="loginId"></param>
/// <param name="userName"></param>
/// <param name="mailAddress"></param>
/// <param name="isVerified"></param>
public class User
{
    /// <summary>
    /// Id. 不変のId.
    /// </summary>
    public string Id { get; private set; }
    /// <summary>
    /// ユーザーによって設定、変更可能なId.
    /// </summary>
    public LoginId LoginId { get; private set; }
    /// <summary>
    /// ユーザー名.
    /// </summary>
    public UserName UserName { get; private set; }
    /// <summary>
    /// メールアドレス.
    /// </summary>
    public string MailAddress { get; private set; }
    /// <summary>
    /// 認証済みであるかどうか
    /// </summary>
    public bool IsVerified { get; private set; }

    public User(string loginId, string userName, string mailAddress)
    {
        this.Id = Guid.NewGuid().ToString();
        this.LoginId = new LoginId(loginId);
        this.UserName = new UserName(userName);
        this.MailAddress = mailAddress;
        this.IsVerified = false;
    }

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