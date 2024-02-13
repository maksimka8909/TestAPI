using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;

namespace TestAPI.Domain.UseCases;

public class ClientUseCase : GenericUseCase<Client>
{
    private readonly IClientRepository _clientRepository;

    public ClientUseCase(IClientRepository clientRepository) : base(clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public void AddFounder(Client client, Founder founder) =>
        _clientRepository.AddFounder(client, founder);

    public async Task RemoveFounder(Client client, Founder founder) =>
        await _clientRepository.RemoveFounder(client, founder);

    public async Task<Client?> GetClientByTaxpayerNumber(string number) =>
        await _clientRepository.GetClientByTaxpayerNumber(number);
}