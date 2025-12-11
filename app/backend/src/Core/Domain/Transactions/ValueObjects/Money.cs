using HouseholdBudget.Core.Domain.Common;
using HouseholdBudget.Defines.Exceptions;

namespace HouseholdBudget.Core.Domain.Transactions.ValueObjects;

/// <summary>
/// 金額を表すValueObject
/// </summary>
public class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ValidationException("Amount cannot be negative.");

        if (string.IsNullOrWhiteSpace(currency))
            throw new ValidationException("Currency is required.");

        if (currency.Length != 3)
            throw new ValidationException("Currency must be a 3-letter ISO code.");

        return new Money(amount, currency.ToUpperInvariant());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount} {Currency}";
}
