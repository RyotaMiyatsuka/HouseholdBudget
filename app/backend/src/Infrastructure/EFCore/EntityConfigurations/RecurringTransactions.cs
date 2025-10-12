using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Users.Entities;

namespace HouseholdBudget.Infrastructure.EFCore.EntityConfigurations;


public class RecurringTransactionsConfiguration : BaseEntityConfiguration<RecurringTransaction>
{
    /// <summary>
    /// エンティティとテーブルのマッピング設定
    /// </summary>
    /// <param name="builder"></param>
    public override void Configure(EntityTypeBuilder<RecurringTransaction> builder)
    {
        base.Configure(builder);

        builder.ToTable("recurring_transactions");
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .HasMaxLength(36)
            .IsRequired();
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(r => r.Price)
            .IsRequired();
        builder.Property(r => r.Type)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(r => r.Category)
            .HasMaxLength(100);
        builder.Property(r => r.Memo)
            .HasMaxLength(255);
        builder.Property(r => r.Place)
            .HasMaxLength(100);
        builder.Property(r => r.IsDeleted)
            .IsRequired();
    }
}
