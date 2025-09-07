using System.Text.RegularExpressions;

namespace HouseholdBudget.Core.Domain.Users.ValueObjects;

public record LoginId
{
    private const int MIN_LENGTH = 1;
    private const int MAX_LENGTH = 20;
    private static readonly Regex ValidCharactersRegex = new Regex("^[a-zA-Z0-9_-]+$", RegexOptions.Compiled);
    public string Value { get; }

    /// <summary>
    /// バリデーションを行う.
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException">バリデーションエラー</exception>
    public LoginId(string value)
    {
        // TODO: エラーメッセージ共通化
        string lengthErrorMessage = "{0} は {1} 文字以上 {2} 文字以下である必要があります。";
        string invalidCharsErrorMessage = "無効な文字が含まれています。";

        // 文字数バリデーション
        if (string.IsNullOrEmpty(value) || value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
        {
            throw new ArgumentException(string.Format(lengthErrorMessage, nameof(LoginId), MIN_LENGTH, MAX_LENGTH));
        }
        // 文字種別バリデーション
        if (!ValidCharactersRegex.IsMatch(value))
        {
            throw new ArgumentException(invalidCharsErrorMessage);
        }

        // 全ての検証をパスした場合にプロパティに値を代入
        Value = value;
    }
}