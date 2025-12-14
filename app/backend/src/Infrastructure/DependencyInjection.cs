using HouseholdBudget.Core.Application.Common.Interfaces;
using HouseholdBudget.Core.Domain.Auth.Interfaces;
using HouseholdBudget.Core.Domain.Common.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Users.Interfaces;
using HouseholdBudget.Infrastructure.Repositories.EFCore;
using HouseholdBudget.Infrastructure.Repositories.EFCore.Repositories;
using HouseholdBudget.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HouseholdBudget.Infrastructure;

/// <summary>
/// Infrastructure層の依存性注入設定
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<AppDbContext>(options =>
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                // 開発環境でのインメモリDB使用
                options.UseInMemoryDatabase("HouseholdBudgetDb");
            }
            else
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .UseSnakeCaseNamingConvention();
            }
        });

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // HttpContextAccessor
        services.AddHttpContextAccessor();

        return services;
    }
}
