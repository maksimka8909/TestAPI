using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Domain.UseCases;

public class ClientUseCase
{
    private readonly IClientRepository _clientRepository;
    private readonly IGenericRepository<Client> _genericRepository;

    public ClientUseCase(IClientRepository clientRepository, IGenericRepository<Client> genericRepository)
    {
        _clientRepository = clientRepository;
        _genericRepository = genericRepository;
    }

    public async Task Add(Client item) =>
        await _genericRepository.Add(item);

    public async Task<IReadOnlyList<Client>> GetAll() =>
        await _genericRepository.GetAll();

    public async Task<Client> Get(int id) =>
        await _genericRepository.Get(id);

    public void AddFounder(Client client, Founder founder) =>
        _clientRepository.AddFounder(client, founder);

    public void RemoveFounder(Client client, Founder founder) =>
        _clientRepository.RemoveFounder(client, founder);

    public async Task<Client?> GetUserByTaxpayerNumber(string number) =>
        await _clientRepository.GetUserByTaxpayerNumber(number);
}