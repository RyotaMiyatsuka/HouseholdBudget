namespace HouseholdBudget.Defines;

public class UserExistsException : Exception
{
    public UserExistsException()
    {
    }

    public UserExistsException(string message)
        : base(message)
    {
    }

    public UserExistsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string message)
        : base(message)
    {
    }

    public UserNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}