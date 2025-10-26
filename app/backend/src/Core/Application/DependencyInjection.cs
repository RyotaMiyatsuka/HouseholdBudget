using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HouseholdBudget.Core.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Application 層のDI
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // ユースケースの登録
        services.AddScoped<ITransactionUseCase, TransactionUseCase>();

        return services;
    }
}
