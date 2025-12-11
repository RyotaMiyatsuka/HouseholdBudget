using HouseholdBudget.Core.Domain.Transactions.Entities;
using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore.Repositories;

/// <summary>
/// 取引リポジトリの実装
/// </summary>
public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions.FindAsync([id], cancellationToken);
    }

    public async Task<Transaction?> GetByIdAndUserIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAndMonthAsync(Guid userId, int year, int month, CancellationToken cancellationToken = default)
    {
        var startDate = new DateOnly(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await _context.Transactions
            .Where(t => t.UserId == userId &&
                        t.Date.Value >= startDate &&
                        t.Date.Value <= endDate)
            .OrderByDescending(t => t.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
    }

    public void Update(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
    }

    public void Delete(Transaction transaction)
    {
        _context.Transactions.Remove(transaction);
    }
}
