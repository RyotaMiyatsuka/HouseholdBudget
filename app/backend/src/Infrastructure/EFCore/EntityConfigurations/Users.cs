using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Infrastructure.EFCore.EntityConfigurations;

/// <summary>
/// usersテーブル
/// </summary>
public class UsersConfiguration : BaseEntityConfiguration<User>
{
    /// <summary>
    /// エンティティとテーブルのマッピング設定
    /// </summary>
    /// <param name="builder"></param>
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.LoginId).IsUnique();

        builder.Property(u => u.Id)
            .HasMaxLength(36)
            .IsRequired();
        builder.Property(u => u.LoginId)
            .HasMaxLength(100)
            .IsRequired()
            .HasConversion(v => v.Value, v => new LoginId(v));
        builder.Property(u => u.UserName)
            .HasMaxLength(100)
            .IsRequired()
            .HasConversion(v => v.Value, v => new UserName(v));
        builder.Property(u => u.MailAddress)
            .IsRequired();
        builder.Property(u => u.IsVerified)
            .IsRequired();
    }
}
