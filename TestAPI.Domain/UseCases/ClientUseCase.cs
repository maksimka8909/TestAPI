using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Domain.UseCases;

public class ClientUseCase : IClientRepository
{
    private readonly IClientRepository _clientRepository;

    public ClientUseCase(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task Add(Client item) =>
        await _clientRepository.Add(item);

    public void Update(Client item) =>
        _clientRepository.Update(item);

    public async Task Delete(int id) =>
        await _clientRepository.Delete(id);

    public async Task<IReadOnlyList<Client>> GetAll() =>
        await _clientRepository.GetAll();

    public async Task<Client> Get(int id) =>
        await _clientRepository.Get(id);

    public async Task AddFounder(int clientId, Founder founder) =>
        await _clientRepository.AddFounder(clientId, founder);

    public async Task RemoveFounder(int clientId, Founder founder) =>
        await _clientRepository.RemoveFounder(clientId, founder);

    public async Task<Client> GetUserByTaxpayerNumber(string number) =>
        await _clientRepository.GetUserByTaxpayerNumber(number);
}