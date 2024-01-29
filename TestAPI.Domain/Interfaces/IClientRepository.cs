using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IClientRepository : IGenericRepository<Client>
{
    public Task AddFounder(int clientId, Founder founder);
    public Task RemoveFounder(int clientId, Founder founder);
    public Task<Client> GetUserByTaxpayerNumber(string number);
}