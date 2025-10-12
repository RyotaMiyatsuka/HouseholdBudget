using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Users.Entities;

namespace HouseholdBudget.Infrastructure.EFCore.EntityConfigurations;

/// <summary>
/// transactionsテーブル
/// </summary>
public class TransactionsConfiguration : BaseEntityConfiguration<Transaction>
{
    /// <summary>
    /// エンティティとテーブルのマッピング設定
    /// </summary>
    /// <param name="builder"></param>
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);

        builder.ToTable("transactions");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasMaxLength(36)
            .IsRequired();
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<CsvImport>()
            .WithMany()
            .HasForeignKey(t => t.CsvImportId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasOne<RecurringTransaction>()
            .WithMany()
            .HasForeignKey(t => t.RecurringTransactionId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Property(t => t.Price)
            .IsRequired();
        builder.Property(t => t.Type)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(t => t.Category)
            .HasMaxLength(100);
        builder.Property(t => t.Memo)
            .HasMaxLength(255);
        builder.Property(t => t.Place)
            .HasMaxLength(100);
        builder.Property(t => t.IsDeleted)
            .IsRequired();
    }
}
