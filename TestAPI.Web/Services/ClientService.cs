using TestAPI.Data.Repositories;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;
using TestAPI.Domain.UseCases;
using TestAPI.Validators;
using TestAPI.ViewModels;

namespace TestAPI.Services;

public class ClientService
{
    private readonly ClientUseCase _clientUseCase;
    private readonly FounderUseCase _founderUseCase;
    private readonly ISaveRepository _saveRepository;
    private readonly ClientCreateValidator _createValidator;
    private readonly ClientUpdateValidator _updateValidator;

    public ClientService(ClientUseCase clientUseCase,
        ClientCreateValidator createValidator, ClientUpdateValidator updateValidator,
        FounderUseCase founderUseCase, ISaveRepository saveRepository)
    {
        _clientUseCase = clientUseCase;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _founderUseCase = founderUseCase;
        _saveRepository = saveRepository;
    }

    public async Task<IReadOnlyList<Message>> Add(ClientCreateInfo clientCreate)
    {
        var result = _createValidator.Validate(clientCreate);
        if (result.IsValid)
        {
            var client = await _clientUseCase.GetUserByTaxpayerNumber(clientCreate.TaxpayerNumber);
            if (client != null)
                return new List<Message>() { new Message("Клиент с таким ИНН уже есть") };
            var clientType = clientCreate.TaxpayerNumber.Length == 12 ? ClientType.Individual : ClientType.Legal;
            await _clientUseCase.Add(new Client(clientCreate.TaxpayerNumber, clientCreate.Name, clientType));
            await _saveRepository.Save();
            return new List<Message>() { new Message("Клиент успешно создан") };
        }
        else
        {
            List<Message> errors = new List<Message>();
            foreach (var error in result.Errors)
            {
                errors.Add(new Message(error.ErrorMessage));
            }

            return errors;
        }
    }

    public async Task<IReadOnlyList<Message>> Update(ClientUpdate client)
    {
        var result = _updateValidator.Validate(client);
        if (result.IsValid)
        {
            var c = await _clientUseCase.Get(client.Id);
            c.Name = client.Name;
            _clientUseCase.Update(c);
            await _saveRepository.Save();
            return new List<Message>() { new Message("Клиент успешно создан") };
        }
        else
        {
            List<Message> errors = new List<Message>();
            foreach (var error in result.Errors)
            {
                errors.Add(new Message(error.ErrorMessage));
            }

            return errors;
        }
    }

    public async Task Delete(int id)
    {
        await _clientUseCase.Delete(id);
        await _saveRepository.Save();
    }

    public async Task<IReadOnlyList<ClientMainInfo>> GetAll()
    {
        var clients = await _clientUseCase.GetAll();
        var viewClients = clients.Select(x => new ClientMainInfo(x)).ToArray();
        return viewClients;
    }

    public async Task<ClientMainInfo> Get(int id)
    {
        var client = await _clientUseCase.Get(id);
        return new ClientMainInfo(client);
    }

    public async Task AddFounder(int clientId, int founderId)
    {
        var founder = await _founderUseCase.Get(founderId);
        await _clientUseCase.AddFounder(clientId, founder);
        await _saveRepository.Save();
    }

    public async Task RemoveFounder(int clientId, int founderId)
    {
        var founder = await _founderUseCase.Get(founderId);
        await _clientUseCase.RemoveFounder(clientId, founder);
        await _saveRepository.Save();
    }
}