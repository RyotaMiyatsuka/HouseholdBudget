using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Infrastructure.EFCore.Repositories.Transactions;

namespace HouseholdBudget.Infrastructure.EFCore.Repositories;

/// <summary>
/// UnitOfWorkの実装
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    private AppDbContext _context;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="context"></param>
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 変更を保存する
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Transactionリポジトリ
    /// </summary>
    private ITransactionRepository? _transactionRepository;
    public ITransactionRepository TransactionRepository
    {
        get
        {
            if (_transactionRepository == null)
            {
                _transactionRepository = new TransactionRepository(_context);
            }
            return _transactionRepository;
        }
    }
}
