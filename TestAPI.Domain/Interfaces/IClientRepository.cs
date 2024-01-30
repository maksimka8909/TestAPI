using TestAPI.Domain.Models;

namespace TestAPI.Domain.Interfaces;

public interface IClientRepository
{
    public void AddFounder(Client? client, Founder? founder);

    public Task RemoveFounder(Client? client, Founder? founder);

    public Task<Client?> GetUserByTaxpayerNumber(string number);
}