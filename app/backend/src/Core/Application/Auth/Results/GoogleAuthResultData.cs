using HouseholdBudget.Core.Application.Common.Models;
using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Application.Auth.Results;

/// <summary>
/// Google認証結果 (TODO)
/// </summary>
public class GoogleAuthResultData
{
    public required string LoginId { get; set; }
}
