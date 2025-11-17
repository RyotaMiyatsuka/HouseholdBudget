using HouseholdBudget.Core.Domain.Transactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HouseholdBudget.Infrastructure.EFCore;

/// <summary>
/// DbContext. DBの設定等を記述
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>
    /// モデルの作成
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // EntityTypeConfigurationから各テーブルの設定を読み込む
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

/// <summary>
/// DbContextのファクトリ
/// </summary>
public class AppDbContextFactory :
    IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

        var options = new DbContextOptionsBuilder<AppDbContext>();
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        // スネークケースに変更
        options.UseSnakeCaseNamingConvention();

        return new AppDbContext(options.Options);
    }
}
