using HouseholdBudget.Core.Domain.Common;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Domain.Users.Entities;

/// <summary>
/// ユーザーエンティティ
/// </summary>
public class User : Entity<Guid>
{
    public Email Email { get; private set; } = null!;
    public UserName UserName { get; private set; } = null!;

    private User() : base()
    {
    }

    private User(Guid id, Email email, UserName userName) : base(id)
    {
        Email = email;
        UserName = userName;
    }

    public static User Create(Email email, UserName userName)
    {
        return new User(Guid.NewGuid(), email, userName);
    }

    public void UpdateUserName(UserName userName)
    {
        UserName = userName;
        SetUpdatedAt();
    }
}
