using System.Text.RegularExpressions;

namespace HouseholdBudget.Core.Domain.Users.ValueObjects;

public record UserName
{
    private const int MIN_LENGTH = 1;
    private const int MAX_LENGTH = 20;
    public string Value { get; }

    /// <summary>
    /// バリデーションを行う.
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException">バリデーションエラー</exception>
    public UserName(string value)
    {
        // TODO: エラーメッセージ共通化
        string lengthErrorMessage = "{0} は {1} 文字以上 {2} 文字以下である必要があります。";

        // 文字数バリデーション
        if (string.IsNullOrWhiteSpace(value) || value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
        {
            throw new ArgumentException(string.Format(lengthErrorMessage, nameof(UserName), MIN_LENGTH, MAX_LENGTH));
        }

        // TODO: 文字数種別バリデーション

        // 全ての検証をパスした場合にプロパティに値を代入
        Value = value;
    }
}