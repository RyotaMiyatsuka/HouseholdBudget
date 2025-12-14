using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore.EntityConfigurations;

/// <summary>
/// ユーザーエンティティの設定
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(256)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => Email.Create(v));

        builder.Property(u => u.UserName)
            .HasColumnName("user_name")
            .HasMaxLength(UserName.MaxLength)
            .IsRequired()
            .HasConversion(
                v => v.Value,
                v => UserName.Create(v));

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
