﻿using TestAPI.Domain.Interfaces;
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
        var result = await _createValidator.ValidateAsync(clientCreate);
        if (result.IsValid)
        {
            var client = await _clientUseCase.GetUserByTaxpayerNumber(clientCreate.TaxpayerNumber);
            if (client != null)
                return SendMessage("Клиент с таким ИНН уже есть");
            var clientType = clientCreate.TaxpayerNumber.Length == 12 ? ClientType.Individual : ClientType.Legal;
            await _clientUseCase.Add(new Client(clientCreate.TaxpayerNumber, clientCreate.Name, clientType));
            await _saveRepository.Save();
            return SendMessage("Клиент успешно создан");
        }

        return result.Errors.Select(error => new Message(error.ErrorMessage)).ToList();
    }

    public async Task<IReadOnlyList<Message>> Update(ClientUpdate client)
    {
        var result = await _updateValidator.ValidateAsync(client);
        if (result.IsValid)
        {
            var currentClient = await _clientUseCase.Get(client.Id);
            if (currentClient == null)
            {
                return SendMessage("Клиент не найден");
            }

            currentClient.Name = client.Name;
            currentClient.UpdatedAt = DateTime.Now;
            await _saveRepository.Save();
            return SendMessage("Клиент успешно изменен");
        }

        return result.Errors.Select(error => new Message(error.ErrorMessage)).ToList();
    }

    public async Task<IReadOnlyList<Message>> Delete(int id)
    {
        var client = await _clientUseCase.Get(id);
        if (client == null)
        {
            return SendMessage("Клиент не найден");
        }

        client.DeletedAt = DateTime.Now;
        await _saveRepository.Save();
        return SendMessage("Клиент удален");
    }

    public async Task<IReadOnlyList<ClientMainInfo>> GetAll()
    {
        IReadOnlyList<Client?> clients = await _clientUseCase.GetAll();
        var viewClients = clients.Select(x => new ClientMainInfo(x)).ToArray();
        return viewClients;
    }

    public async Task<ClientMainInfo> Get(int id)
    {
        var client = await _clientUseCase.Get(id);
        if (client == null)
        {
            return new ClientMainInfo();
        }

        return new ClientMainInfo(client);
    }

    public async Task<IReadOnlyList<Message>> AddFounder(int clientId, int founderId)
    {
        var founder = await _founderUseCase.Get(founderId);
        var client = await _clientUseCase.Get(clientId);
        if (client == null || founder == null)
        {
            return SendMessage("Ошибка добавления, клиент или учредитель не найден");
        }

        if (client.Type == ClientType.Individual)
        {
            return SendMessage("Ошибка добавления, только у юридических лиц могут быть учредители");
        }
        _clientUseCase.AddFounder(client, founder);
        await _saveRepository.Save();
        return SendMessage("Учредитель добавлен");
    }

    public async Task<IReadOnlyList<Message>> RemoveFounder(int clientId, int founderId)
    {
        var founder = await _founderUseCase.Get(founderId);
        var client = await _clientUseCase.Get(clientId);
        if (client == null || founder == null)
        {
            return SendMessage("Ошибка удаления, клиент или учредитель не найден");
        }

        await _clientUseCase.RemoveFounder(client, founder);
        await _saveRepository.Save();
        return SendMessage("Учредитель удален");
    }

    private List<Message> SendMessage(string message) =>
        new List<Message>() { new Message(message) };
}