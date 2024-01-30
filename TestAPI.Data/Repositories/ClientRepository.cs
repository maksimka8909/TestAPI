using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Repositories;

public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(DatabaseContext context) : base(context)
    {
    }

    public void AddFounder(Client? client, Founder? founder)
    {
        client.Founders.Add(founder);
    }

    public async Task RemoveFounder(Client? client, Founder? founder)
    {
        var clientFullInfo = await _dbSet.Include("Founders").Where(c => c == client).FirstOrDefaultAsync();
        clientFullInfo.Founders.Remove(founder);
    }

    public async Task<Client?> GetUserByTaxpayerNumber(string number) =>
        await _dbSet.Where(c => c.TaxpayerNumber == number).FirstOrDefaultAsync();
}