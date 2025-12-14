using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Core.Application.Auth.UseCases;
using HouseholdBudget.Core.Application.Transactions.Interfaces;
using HouseholdBudget.Core.Application.Transactions.UseCases;
using HouseholdBudget.Core.Application.Users.Interfaces;
using HouseholdBudget.Core.Application.Users.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace HouseholdBudget.Core.Application;

/// <summary>
/// Application層の依存性注入設定
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Auth UseCases
        services.AddScoped<IGoogleLoginUseCase, GoogleLoginUseCase>();
        services.AddScoped<ILogoutUseCase, LogoutUseCase>();

        // User UseCases
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();

        // Transaction UseCases
        services.AddScoped<IListTransactionsUseCase, ListTransactionsUseCase>();
        services.AddScoped<IGetTransactionsByMonthUseCase, GetTransactionsByMonthUseCase>();
        services.AddScoped<ICreateTransactionUseCase, CreateTransactionUseCase>();
        services.AddScoped<IUpdateTransactionUseCase, UpdateTransactionUseCase>();
        services.AddScoped<IDeleteTransactionUseCase, DeleteTransactionUseCase>();

        // Category UseCases
        services.AddScoped<IListCategoriesUseCase, ListCategoriesUseCase>();
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
        services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();

        return services;
    }
}
