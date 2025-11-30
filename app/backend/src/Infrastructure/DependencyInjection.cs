using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Infrastructure.EFCore;
using HouseholdBudget.Infrastructure.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HouseholdBudget.Core.Application.Auth.Interfaces;
using HouseholdBudget.Infrastructure.Services;

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

        // セッションサービスの登録
        services.AddHttpContextAccessor();
        services.AddScoped<ISessionService, SessionService>();

        // Google トークン検証サービスの登録
        services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();

        // TODO: Identity 用のユーザークラスの実装
        services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
        // 認証
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = true; // 本番では必須
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]
                        ?? throw new InvalidOperationException("Jwt:SecretKey is not configured"))),
                ClockSkew = TimeSpan.Zero // トークン有効期限を厳密に
            };
        })
        .AddGoogle(googleOptions =>
        {
            googleOptions.ClientId = configuration["Authentication:Google:ClientId"]
                ?? throw new InvalidOperationException("Google ClientId is not configured");
            googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"]
                ?? throw new InvalidOperationException("Google ClientSecret is not configured");
        })
        .AddCookie(options =>
        {
            // TODO: Cookie設定のカスタマイズ
        })
        ;

        return services;
    }
}
