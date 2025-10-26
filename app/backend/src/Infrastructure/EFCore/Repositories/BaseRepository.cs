namespace HouseholdBudget.Infrastructure.EFCore.Repositories;

/// <summary>
/// リポジトリの基底クラス
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseRepository<T> where T : class
{
    protected AppDbContext _context;
    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    /// <summary>
    /// INSERT
    /// </summary>
    /// <param name="entity"></param>
    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }
}
