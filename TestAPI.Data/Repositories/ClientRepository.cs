using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(DatabaseContext database) : base(database)
    {
    }

    public async Task AddFounder(int clientId, Founder founder)
    {
        var client = await _dbSet.FindAsync(clientId);
        client.Founders.Add(founder);
    }

    public async Task RemoveFounder(int clientId, Founder founder)
    {
        var client = await _dbSet.Include(c => c.Founders)
            .FirstOrDefaultAsync(c => c.Id == clientId);
        client.Founders.Remove(founder);
    }

    public async Task<Client> GetUserByTaxpayerNumber(string number) =>
        await _dbSet.Where(c => c.TaxpayerNumber == number).FirstOrDefaultAsync();
}