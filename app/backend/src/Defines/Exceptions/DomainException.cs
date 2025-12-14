namespace HouseholdBudget.Defines.Exceptions;

/// <summary>
/// ドメイン層で発生する例外の基底クラス
/// </summary>
public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
