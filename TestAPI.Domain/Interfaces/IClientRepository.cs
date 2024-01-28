using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IClientRepository : ICommonRepository<Client>, IPersonRepository<Client>
{
    public Task AddFounder(int clientId, int founderId);
    public Task RemoveFounder(int clientId, int founderId);
}