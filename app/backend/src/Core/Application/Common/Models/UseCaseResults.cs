namespace HouseholdBudget.Core.Application.Common.Models;

/// <summary>
/// ユースケース結果
/// </summary>
/// <typeparam name="T"></typeparam>
public class UseCaseResult<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
}
