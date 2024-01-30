using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;

namespace TestAPI.Data.Repositories;

public class SaveRepository : ISaveRepository
{
    private readonly DatabaseContext _database;

    public SaveRepository(DatabaseContext database)
    {
        _database = database;
    }

    public async Task Save()
    {
        var now = DateTime.Now;
        foreach (var entityEntry in _database.ChangeTracker.Entries().ToArray())
        {
            if (entityEntry is { State: EntityState.Added, Entity: IBaseModel createEntity })
            {
                createEntity.CreatedAt = now;
            }
        }

        await _database.SaveChangesAsync();
    }
}