using HouseholdBudget.Core.Domain.Transactions.Entities;

namespace HouseholdBudget.Core.Domain.Transactions.Interfaces;

public interface ITransactionRepository
{
    /// <summary>
    /// 保存
    /// </summary>
    Task AddAsync(Transaction transaction);

    /// <summary>
    /// 更新
    /// </summary>
    Task UpdateAsync(Transaction transaction);

    /// <summary>
    /// Id検索
    /// </summary>
    /// <returns></returns>
    Task<Transaction?> FindByIdAsync(string id);

    /// <summary>
    /// 削除
    /// </summary>
    Task DeleteAsync(Transaction transaction);
}
