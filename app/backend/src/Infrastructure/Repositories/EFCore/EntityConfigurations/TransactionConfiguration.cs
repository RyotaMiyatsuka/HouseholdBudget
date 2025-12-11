using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Transactions.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore.EntityConfigurations;

/// <summary>
/// 取引エンティティの設定
/// </summary>
public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.OwnsOne(t => t.Money, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("amount")
                .HasPrecision(18, 2)
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("currency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(t => t.Date)
            .HasColumnName("date")
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => TransactionDate.Create(v));

        builder.Property(t => t.TransactionType)
            .HasColumnName("transaction_type")
            .IsRequired();

        builder.Property(t => t.CategoryId)
            .HasColumnName("category_id")
            .IsRequired();

        builder.Property(t => t.Memo)
            .HasColumnName("memo")
            .HasMaxLength(Transaction.MaxMemoLength);

        builder.Property(t => t.Place)
            .HasColumnName("place")
            .HasMaxLength(Transaction.MaxPlaceLength);

        builder.Property(t => t.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(t => t.UserId);
        builder.HasIndex(t => new { t.UserId, t.Date });
    }
}
