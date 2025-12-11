using HouseholdBudget.Core.Domain.Common;
using HouseholdBudget.Defines.Exceptions;

namespace HouseholdBudget.Core.Domain.Users.ValueObjects;

/// <summary>
/// ユーザー名を表すValueObject
/// </summary>
public class UserName : ValueObject
{
    public const int MaxLength = 100;

    public string Value { get; }

    private UserName(string value)
    {
        Value = value;
    }

    public static UserName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException("User name is required.");

        if (value.Length > MaxLength)
            throw new ValidationException($"User name must not exceed {MaxLength} characters.");

        return new UserName(value.Trim());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
