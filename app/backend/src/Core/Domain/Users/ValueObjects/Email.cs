using System.Text.RegularExpressions;
using HouseholdBudget.Core.Domain.Common;
using HouseholdBudget.Defines.Exceptions;

namespace HouseholdBudget.Core.Domain.Users.ValueObjects;

/// <summary>
/// メールアドレスを表すValueObject
/// </summary>
public partial class Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValidationException("Email is required.");

        if (!EmailRegex().IsMatch(value))
            throw new ValidationException("Invalid email format.");

        return new Email(value.ToLowerInvariant());
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)]
    private static partial Regex EmailRegex();
}
