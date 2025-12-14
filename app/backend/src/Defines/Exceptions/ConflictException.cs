namespace HouseholdBudget.Defines.Exceptions;

/// <summary>
/// リソースの競合が発生した場合の例外
/// </summary>
public class ConflictException : DomainException
{
    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string resourceName, object key)
        : base($"{resourceName} with key '{key}' already exists.")
    {
    }
}
