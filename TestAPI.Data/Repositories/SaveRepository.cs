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
        foreach (var entityEntry in _database.ChangeTracker.Entries().ToArray())
        {
            if (entityEntry.State == EntityState.Added && entityEntry.Entity is IBaseModel createEntity)
            {
                createEntity.SetCreateDate();
            }
        }

        await _database.SaveChangesAsync();
    }
}