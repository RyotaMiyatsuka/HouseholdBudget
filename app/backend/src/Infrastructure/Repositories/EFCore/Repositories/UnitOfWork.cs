using HouseholdBudget.Core.Domain.Auth.Interfaces;
using HouseholdBudget.Core.Domain.Common.Interfaces;
using HouseholdBudget.Core.Domain.Transactions.Interfaces;
using HouseholdBudget.Core.Domain.Users.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace HouseholdBudget.Infrastructure.Repositories.EFCore.Repositories;

/// <summary>
/// UnitOfWorkの実装
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public ITransactionRepository Transactions { get; }
    public ICategoryRepository Categories { get; }
    public IUserRepository Users { get; }
    public ISessionRepository Sessions { get; }

    public UnitOfWork(
        AppDbContext context,
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository,
        ISessionRepository sessionRepository)
    {
        _context = context;
        Transactions = transactionRepository;
        Categories = categoryRepository;
        Users = userRepository;
        Sessions = sessionRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
        _disposed = true;
    }
}
