namespace TestAPI.Data.Repositories;

public class CommonRepository
{
    private readonly ApiContext _db;

    public CommonRepository(ApiContext db)
    {
        _db = db;
    }

    public async Task Save() => await _db.SaveChangesAsync();
}