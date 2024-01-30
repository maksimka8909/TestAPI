using TestAPI.Domain.Interfaces;

namespace TestAPI.Data.Repositories;

public class SaveRepository : ISaveRepository
{
    private readonly DatabaseContext _database;

    public SaveRepository(DatabaseContext database)
    {
        _database = database;
    }

    public async Task Save() => await _database.SaveChangesAsync();
}