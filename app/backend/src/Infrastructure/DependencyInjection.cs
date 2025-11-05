// using HouseholdBudget.Core.Domain.Users.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Infrastructure.EFCore;
using HouseholdBudget.Infrastructure.EFCore.Repositories;

// using HouseholdBudget.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HouseholdBudget.Infrastructure;

public static class InfrastructureDI
{
    /// <summary>
    /// Infrastructure 層のDI
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // DBコンテキストの登録
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                   .UseSnakeCaseNamingConvention()
        );

        // リポジトリの登録
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
