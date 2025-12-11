using HouseholdBudget.Core.Domain.Common;

namespace HouseholdBudget.Core.Domain.Transactions.ValueObjects;

/// <summary>
/// 取引日を表すValueObject
/// </summary>
public class TransactionDate : ValueObject
{
    public DateOnly Value { get; }

    private TransactionDate(DateOnly value)
    {
        Value = value;
    }

    public static TransactionDate Create(DateOnly value)
    {
        return new TransactionDate(value);
    }

    public static TransactionDate Create(DateTime value)
    {
        return new TransactionDate(DateOnly.FromDateTime(value));
    }

    public static TransactionDate Create(string value)
    {
        if (DateOnly.TryParse(value, out var date))
            return new TransactionDate(date);

        throw new ArgumentException("Invalid date format.", nameof(value));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString("yyyy-MM-dd");
}
