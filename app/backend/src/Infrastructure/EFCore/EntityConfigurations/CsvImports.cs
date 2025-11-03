using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Infrastructure.EFCore.EFEntities;

/// <summary>
/// csv_importsテーブル
/// </summary>
public class CsvImportsConfiguration : BaseEntityConfiguration<CsvImport>
{
    /// <summary>
    /// エンティティとテーブルのマッピング設定
    /// </summary>
    /// <param name="builder"></param>
    public override void Configure(EntityTypeBuilder<CsvImport> builder)
    {
        base.Configure(builder);

        builder.ToTable("csv_imports");
        builder.HasKey(c => c.Id);
        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .HasPrincipalKey(c => c.LoginId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.Id)
            .HasMaxLength(36)
            .IsRequired();
        builder.Property(c => c.UserId)
            .HasMaxLength(100)
            .IsRequired()
            .HasConversion(v => v.Value, v => new LoginId(v));
    }
}
