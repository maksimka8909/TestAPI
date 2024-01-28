using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApiContext _db;

    public ClientRepository(ApiContext db)
    {
        _db = db;
    }

    public async Task Add(Client item)
    {
        await _db.Clients.AddAsync(item);
    }

    public async Task Update(Client item)
    {
        item.UpdatedAt = DateTime.Now;
    }

    public async Task Delete(int id)
    {
        var client = await _db.Clients.FindAsync(id);
        client.DeletedAt = DateTime.Now;
    }

    public async Task<IReadOnlyList<Client>> GetAll() =>
        await _db.Clients.Include(c => c.Founders).Where(c => c.DeletedAt == null).ToListAsync();

    public async Task<Client> Get(int id) =>
        await _db.Clients.Where(c => c.DeletedAt == null && c.Id == id).FirstAsync();

    public async Task AddFounder(int clientId, int founderId)
    {
        var client = await _db.Clients.FindAsync(clientId);
        var founder = await _db.Founders.FindAsync(founderId);
        client.Founders.Add(founder);
    }

    public async Task RemoveFounder(int clientId, int founderId)
    {
        var client = await _db.Clients.Include(c => c.Founders)
            .FirstOrDefaultAsync(c => c.Id == clientId);
        client.Founders.Remove(await _db.Founders.FindAsync(founderId));
    }

    public async Task<Client> GetUserByTaxpayerNumber(string number)=>
        await _db.Clients.Where(c => c.Inn == number).FirstOrDefaultAsync();

}