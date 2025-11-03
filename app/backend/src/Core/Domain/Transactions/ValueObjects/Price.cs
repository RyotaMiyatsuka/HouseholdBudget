using HouseholdBudget.Defines;

namespace HouseholdBudget.Core.Domain.Transactions.ValueObjects;

/// <summary>
/// 金額
/// </summary>
public record Price
{
    private const int _minAmount = 0;
    public decimal Amount { get; }
    public Currency Currency { get; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="currency"></param>
    /// <exception cref="ArgumentException"></exception>
    public Price(decimal amount, Currency currency)
    {
        // TODO: エラーメッセージ共通化
        string invalidAmountErrorMessage = "{0} は {1} 以上である必要があります。";

        // バリデーション
        if (amount < _minAmount)
        {
            throw new ArgumentException(string.Format(invalidAmountErrorMessage, nameof(Amount), _minAmount.ToString()));
        }

        Amount = amount;
        Currency = currency;
    }

    /// <summary>
    /// 文字列形式に変換
    /// </summary>
    /// <returns>例: "1,000 JPY"</returns>
    public override string ToString()
    {
        return $"{Amount:N0} {Currency}";
    }
}
