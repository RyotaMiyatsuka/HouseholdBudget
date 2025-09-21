using System.ComponentModel.DataAnnotations;

/// <summary>
/// ユーザー情報
/// </summary>
public class Users
{
    public string Id { get; set; }

    /// <summary>
    /// ユーザーが定義する一意のID。変更可能。
    /// </summary>
    /// [Required] // 必須制約
    [MaxLength(100)] // 最大100文字制限
    public string LoginId { get; set; }

    /// <summary>
    /// ユーザーが設定する名前。変更可能。
    /// </summary>
    [MaxLength(100)]
    public string UserName { get; set; }

    /// <summary>
    /// ユーザーのメールアドレス。
    /// </summary>
    public string MailAddress { get; set; }

    /// <summary>
    /// 検証済みであるか。
    /// </summary>
    public string IsVerifield { get; set; }
}
