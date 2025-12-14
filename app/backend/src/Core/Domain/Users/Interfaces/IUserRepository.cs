using HouseholdBudget.Core.Domain.Users.Entities;
using HouseholdBudget.Core.Domain.Users.ValueObjects;

namespace HouseholdBudget.Core.Domain.Users.Interfaces;

/// <summary>
/// ユーザーリポジトリのインタフェース
/// </summary>
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    void Update(User user);
    void Delete(User user);
}
