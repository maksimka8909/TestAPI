using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;
using TestAPI.Domain.UseCases;
using TestAPI.ViewModels;

namespace TestAPI.Services;

public class ClientService
{
    private readonly ClientUseCase _clientUseCase;
    private readonly FounderUseCase _founderUseCase;
    private readonly ISaveRepository _saveRepository;

    public ClientService(ClientUseCase clientUseCase,
        FounderUseCase founderUseCase, ISaveRepository saveRepository)
    {
        _clientUseCase = clientUseCase;
        _founderUseCase = founderUseCase;
        _saveRepository = saveRepository;
    }

    public async Task<ClientMainInfo?> Add(ClientCreateInfo clientCreate)
    {
        var client = await _clientUseCase.GetClientByTaxpayerNumber(clientCreate.TaxpayerNumber);
        if (client != null)
            return null;
        var clientType = clientCreate.TaxpayerNumber.Length == 12 ? ClientType.Individual : ClientType.Legal;
        var newClient = new Client(clientCreate.TaxpayerNumber, clientCreate.Name, clientType);
        await _clientUseCase.Add(newClient);
        await _saveRepository.Save();
        return new ClientMainInfo(newClient);
    }

    public async Task<ClientMainInfo?> Update(ClientUpdate client)
    {
        var currentClient = await _clientUseCase.Get(client.Id);
        if (currentClient == null)
        {
            return null;
        }

        currentClient.SetName(client.Name);
        currentClient.SetUpdateDate();
        await _saveRepository.Save();

        return new ClientMainInfo(currentClient);
    }

    public async Task<ClientMainInfo?> Remove(int id)
    {
        var client = await _clientUseCase.Get(id);
        if (client == null)
        {
            return null;
        }

        client.SetDeleteDate();
        await _saveRepository.Save();
        return new ClientMainInfo(client);
    }

    public async Task<IReadOnlyList<ClientMainInfo>> GetAll(int pageNumber, int pageSize)
    {
        IReadOnlyList<Client?> clients = await _clientUseCase.GetAll(pageNumber, pageSize);
        var viewClients = clients.Select(x => new ClientMainInfo(x)).ToArray();
        return viewClients;
    }

    public async Task<ClientMainInfo?> Get(int id)
    {
        var client = await _clientUseCase.Get(id);
        return client == null ? null : new ClientMainInfo(client);
    }

    public async Task<ClientMainInfo?> AddFounder(int clientId, int founderId)
    {
        var founder = await _founderUseCase.Get(founderId);
        var client = await _clientUseCase.Get(clientId);
        if (client == null || founder == null)
        {
            return null;
        }

        if (client.Type == ClientType.Individual)
        {
            return null;
        }

        foreach (var currentFounder in client.Founders)
        {
            if (currentFounder.Id == founder.Id) return null;
        }

        _clientUseCase.AddFounder(client, founder);
        await _saveRepository.Save();
        return new ClientMainInfo(client);
    }

    public async Task<ClientMainInfo?> RemoveFounder(int clientId, int founderId)
    {
        var founder = await _founderUseCase.Get(founderId);
        var client = await _clientUseCase.Get(clientId);
        if (client == null || founder == null)
        {
            return null;
        }

        await _clientUseCase.RemoveFounder(client, founder);
        await _saveRepository.Save();
        return new ClientMainInfo(client);
    }
}