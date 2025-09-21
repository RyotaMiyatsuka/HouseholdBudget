using Microsoft.EntityFrameworkCore;
using HouseholdBudget.Infrastructure.EFCore.EFEntities;
using Microsoft.EntityFrameworkCore.Design;
using MySqlConnector;

namespace HouseholdBudget.Infrastructure.EFCore;

/// <summary>
/// DbContext。DBの設定等を記述
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<CsvImports> CsvImports { get; set; }
    public DbSet<RecurringTransactions> RecurringTransactions { get; set; }
    public DbSet<Transactions> Transactions { get; set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<CsvImports>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<CsvImports>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecurringTransactions>()
            .HasKey(r => r.Id);
        modelBuilder.Entity<RecurringTransactions>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Transactions>()
            .HasKey(t => t.Id);
        modelBuilder.Entity<Transactions>()
            .HasOne<Users>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Transactions>()
            .HasOne<CsvImports>()
            .WithMany()
            .HasForeignKey(t => t.CsvImportId)
            .OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Transactions>()
            .HasOne<RecurringTransactions>()
            .WithMany()
            .HasForeignKey(t => t.RecurringTransactionId)
            .OnDelete(DeleteBehavior.SetNull);
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
