namespace HouseholdBudget.Defines.Exceptions;

/// <summary>
/// リソースが見つからない場合の例外
/// </summary>
public class NotFoundException : DomainException
{
    public NotFoundException(string resourceName, object key)
        : base($"{resourceName} with key '{key}' was not found.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}
