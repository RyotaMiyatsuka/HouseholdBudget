using HouseholdBudget.Core.Domain.Transactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore.EntityConfigurations;

/// <summary>
/// カテゴリエンティティの設定
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(Category.MaxNameLength)
            .IsRequired();

        builder.Property(c => c.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(c => c.UserId);
        builder.HasIndex(c => new { c.UserId, c.Name }).IsUnique();
    }
}
