// using HouseholdBudget.Core.Domain.Users.Interfaces;
using HouseholdBudget.Infrastructure.EFCore;
// using HouseholdBudget.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HouseholdBudget.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                       .UseSnakeCaseNamingConvention()
            );

            // services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
