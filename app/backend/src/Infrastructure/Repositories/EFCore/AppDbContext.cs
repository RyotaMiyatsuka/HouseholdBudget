using HouseholdBudget.Core.Domain.Auth.Entities;
using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Infrastructure.Repositories.EFCore.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore;

/// <summary>
/// アプリケーションのDBコンテキスト
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    public DbSet<Category> Categories => Set<Category>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SessionConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
    }
}
