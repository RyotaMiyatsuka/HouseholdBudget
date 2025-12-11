using HouseholdBudget.Core.Domain.Auth.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore.EntityConfigurations;

/// <summary>
/// セッションエンティティの設定
/// </summary>
public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("sessions");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasMaxLength(64)
            .ValueGeneratedNever();

        builder.Property(s => s.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.Property(s => s.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(s => s.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(s => s.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(s => s.UserId);
        builder.HasIndex(s => new { s.UserId, s.IsActive });
    }
}
