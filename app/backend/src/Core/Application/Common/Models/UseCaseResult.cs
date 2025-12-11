namespace HouseholdBudget.Core.Application.Common.Models;

/// <summary>
/// ユースケースの結果を表すクラス
/// </summary>
public class UseCaseResult
{
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public UseCaseErrorType? ErrorType { get; }

    protected UseCaseResult(bool isSuccess, string? errorMessage, UseCaseErrorType? errorType)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }

    public static UseCaseResult Success() => new(true, null, null);

    public static UseCaseResult Failure(string errorMessage, UseCaseErrorType errorType = UseCaseErrorType.Unknown)
        => new(false, errorMessage, errorType);

    public static UseCaseResult NotFound(string message = "Resource not found.")
        => new(false, message, UseCaseErrorType.NotFound);

    public static UseCaseResult Unauthorized(string message = "Unauthorized.")
        => new(false, message, UseCaseErrorType.Unauthorized);

    public static UseCaseResult Conflict(string message)
        => new(false, message, UseCaseErrorType.Conflict);

    public static UseCaseResult ValidationError(string message)
        => new(false, message, UseCaseErrorType.Validation);
}

/// <summary>
/// データを含むユースケースの結果を表すクラス
/// </summary>
public class UseCaseResult<T> : UseCaseResult
{
    public T? Data { get; }

    private UseCaseResult(bool isSuccess, T? data, string? errorMessage, UseCaseErrorType? errorType)
        : base(isSuccess, errorMessage, errorType)
    {
        Data = data;
    }

    public static UseCaseResult<T> Success(T data) => new(true, data, null, null);

    public new static UseCaseResult<T> Failure(string errorMessage, UseCaseErrorType errorType = UseCaseErrorType.Unknown)
        => new(false, default, errorMessage, errorType);

    public new static UseCaseResult<T> NotFound(string message = "Resource not found.")
        => new(false, default, message, UseCaseErrorType.NotFound);

    public new static UseCaseResult<T> Unauthorized(string message = "Unauthorized.")
        => new(false, default, message, UseCaseErrorType.Unauthorized);

    public new static UseCaseResult<T> Conflict(string message)
        => new(false, default, message, UseCaseErrorType.Conflict);

    public new static UseCaseResult<T> ValidationError(string message)
        => new(false, default, message, UseCaseErrorType.Validation);
}

/// <summary>
/// ユースケースのエラー種別
/// </summary>
public enum UseCaseErrorType
{
    Unknown,
    NotFound,
    Unauthorized,
    Conflict,
    Validation
}
