namespace HouseholdBudget.Defines.Exceptions;

/// <summary>
/// 認証エラーの例外
/// </summary>
public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string message = "Unauthorized access.") : base(message)
    {
    }
}
