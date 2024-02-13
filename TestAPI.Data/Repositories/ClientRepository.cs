using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(DatabaseContext context) : base(context)
    {
    }

    public void AddFounder(Client client, Founder founder)
    {
        client.Founders.Add(founder);
    }

    public async Task RemoveFounder(Client client, Founder founder)
    {
        var clientFullInfo = await _dbSet.Include(c => c.Founders).Where(c => c == client).FirstOrDefaultAsync();
        clientFullInfo.Founders.Remove(founder);
    }

    public async Task<Client?> GetClientByTaxpayerNumber(string number) =>
        await _dbSet.FirstOrDefaultAsync(c => c.TaxpayerNumber == number);

    public async Task<IReadOnlyList<Client>> GetAll(int pageNumber, int pageSize)
    {
        var skipCount = (pageNumber - 1) * pageSize;
        return await _dbSet.Include(c => c.Founders).OrderBy(t => t.Id)
            .Where(t => t.DeletedAt == null)
            .Skip(skipCount)
            .Take(pageSize)
            .ToArrayAsync();
    }

    public async Task<Client?> Get(int id) =>
        await _dbSet.Include(c => c.Founders).FirstOrDefaultAsync(c => c.DeletedAt == null && c.Id == id);
}