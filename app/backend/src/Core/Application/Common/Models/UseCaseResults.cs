using System.Diagnostics.CodeAnalysis;

namespace HouseholdBudget.Core.Application.Common.Models;

/// <summary>
/// ユースケース結果
/// </summary>
/// <typeparam name="T"></typeparam>
public class UseCaseResult<T>
{
    [MemberNotNullWhen(true, nameof(Data))]
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? ErrorMessage { get; set; }
}
