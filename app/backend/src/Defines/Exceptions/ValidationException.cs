namespace HouseholdBudget.Defines.Exceptions;

/// <summary>
/// バリデーションエラーの例外
/// </summary>
public class ValidationException : DomainException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() : base("One or more validation errors occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IDictionary<string, string[]> errors) : base("One or more validation errors occurred.")
    {
        Errors = errors;
    }
}
