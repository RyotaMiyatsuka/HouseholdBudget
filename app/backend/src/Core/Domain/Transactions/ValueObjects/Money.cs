using HouseholdBudget.Core.Domain.Common;
using HouseholdBudget.Defines.Enums;
using HouseholdBudget.Defines.Exceptions;

namespace HouseholdBudget.Core.Domain.Transactions.ValueObjects;

/// <summary>
/// 金額を表すValueObject
/// </summary>
public class Money : ValueObject
{
    public decimal Amount { get; }
    public Currency Currency { get; }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount < 0)
            throw new ValidationException("Amount cannot be negative.");

        return new Money(amount, currency);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount} {Currency}";
}
