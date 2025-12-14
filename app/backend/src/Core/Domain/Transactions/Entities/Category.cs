using HouseholdBudget.Core.Domain.Common;
using HouseholdBudget.Defines.Exceptions;

namespace HouseholdBudget.Core.Domain.Transactions.Entities;

/// <summary>
/// カテゴリエンティティ
/// </summary>
public class Category : Entity<Guid>
{
    public const int MaxNameLength = 50;

    public string Name { get; private set; } = null!;
    public Guid UserId { get; private set; }

    private Category() : base()
    {
    }

    private Category(Guid id, string name, Guid userId) : base(id)
    {
        Name = name;
        UserId = userId;
    }

    public static Category Create(string name, Guid userId)
    {
        ValidateName(name);
        return new Category(Guid.NewGuid(), name.Trim(), userId);
    }

    public void UpdateName(string name)
    {
        ValidateName(name);
        Name = name.Trim();
        SetUpdatedAt();
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Category name is required.");

        if (name.Length > MaxNameLength)
            throw new ValidationException($"Category name must not exceed {MaxNameLength} characters.");
    }
}
