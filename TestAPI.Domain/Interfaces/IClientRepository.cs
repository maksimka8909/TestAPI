using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    public void AddFounder(Client? client, Founder? founder);

    public Task RemoveFounder(Client? client, Founder? founder);

    public Task<Client?> GetUserByTaxpayerNumber(string number);
}