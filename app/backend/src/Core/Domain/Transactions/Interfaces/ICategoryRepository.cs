using HouseholdBudget.Core.Domain.Transactions.Entities;

namespace HouseholdBudget.Core.Domain.Transactions.Interfaces;

/// <summary>
/// カテゴリリポジトリのインタフェース
/// </summary>
public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    Task<Category?> GetByNameAndUserIdAsync(string name, Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Category>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByNameAndUserIdAsync(string name, Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
    void Update(Category category);
    void Delete(Category category);
}
