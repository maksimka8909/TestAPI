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

    public async Task<IReadOnlyList<T>> GetAll()=>
        await _dbSet.Where(c => c.DeletedAt == null).ToListAsync();

    public async Task<T> Get(int id) =>
        await _dbSet.Where(f => f.DeletedAt == null && f.Id == id).FirstOrDefaultAsync();
}