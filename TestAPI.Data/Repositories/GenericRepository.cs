using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;

namespace TestAPI.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T>
    where T : class, IBaseModel
{
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DatabaseContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task Add(T item) =>
        await _dbSet.AddAsync(item);

    public virtual async Task<IReadOnlyList<T>> GetAll(int pageNumber, int pageSize)
    {
        var skipCount = (pageNumber - 1) * pageSize;
        return await _dbSet.OrderBy(t => t.Id).Where(t => t.DeletedAt == null)
            .Skip(skipCount)
            .Take(pageSize)
            .ToListAsync();
    }

    public virtual async Task<T?> Get(int id) =>
        await _dbSet.Where(t => t.DeletedAt == null && t.Id == id).FirstOrDefaultAsync();

    public void Remove(T item)
    {
        _dbSet.Remove(item);
    }
}