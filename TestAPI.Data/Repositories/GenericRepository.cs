using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Repositories;

public abstract class GenericRepository<TModel> : IGenericRepository<TModel>
   where TModel : class, IBaseModel
{
    private readonly DatabaseContext _database;
    protected readonly DbSet<TModel> _dbSet;

    protected GenericRepository(DatabaseContext database)
    {
        _database = database;
        _dbSet = database.Set<TModel>();
    }

    public async Task Add(TModel item) =>
        await _dbSet.AddAsync(item);

    public void Update(TModel item)
    {
        item.UpdatedAt = DateTime.Now;
    }

    public async Task Delete(int id)
    {
        var model = await _dbSet.FindAsync(id);
        model.DeletedAt = DateTime.Now;
    }

    public async Task<IReadOnlyList<TModel>> GetAll() =>
        await _dbSet.Where(c => c.DeletedAt == null).ToListAsync();

    public async Task<TModel> Get(int id) =>
        await _dbSet.Where(f => f.DeletedAt == null && f.Id == id).FirstOrDefaultAsync();
}