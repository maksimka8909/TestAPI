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
        var entities = _database.ChangeTracker.Entries().ToArray();
        foreach (var entityEntry in entities)
        {
            if (entityEntry.State == EntityState.Added && entityEntry.Entity is IBaseModel createEntity)
            {
                createEntity.SetCreateDate();
            }

            if (entityEntry.State == EntityState.Modified && entityEntry.Entity is IBaseModel updateEntity)
            {
                updateEntity.SetUpdateDate();
            }

            if (entityEntry.State == EntityState.Deleted && entityEntry.Entity is IBaseModel deleteEntity)
            {
                deleteEntity.SetDeleteDate();
                entityEntry.State = EntityState.Modified;
            }
        }

        await _database.SaveChangesAsync();
    }
}