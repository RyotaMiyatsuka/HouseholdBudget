using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HouseholdBudget.Infrastructure.EFCore;

/// <summary>
/// 全エンティティ共通の設定
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
    /// <summary>
    /// 全エンティティ共通の設定
    /// 個別の設定クラスでオーバーライドして使用する
    /// </summary>
    /// <param name="builder"></param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property<DateTime>("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property<string>("CreatedBy")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property<DateTime>("UpdatedAt")
            .HasColumnType("datetime")
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property<string>("UpdatedBy")
            .IsRequired()
            .HasMaxLength(100);
    }
}
