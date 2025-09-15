using HouseholdBudget.Core.Domain.Users.Interfaces;
using HouseholdBudget.Infrastructure.InMemory.Users;
using Microsoft.Extensions.DependencyInjection;

namespace HouseholdBudget.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        return services;
    }
}
