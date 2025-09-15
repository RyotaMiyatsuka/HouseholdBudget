using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Defines;

namespace HouseholdBudget.Core.Domain.Users.Interfaces;

public interface IAuthenticationService
{
    Task<User> AuthenticateAsync(string token, AuthenticationType provider);
}