using Microsoft.AspNetCore.Mvc;
using TestAPI.Domain.Models;
using TestAPI.Services;
using TestAPI.ViewModels;

namespace TestAPI.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<ClientMainInfo>> GetClients() =>
        await _clientService.GetAll();

    [HttpGet("{id}")]
    public async Task<ClientMainInfo> GetClientById(int id) =>
        await _clientService.Get(id);

    [HttpDelete("remove/{id}")]
    public async Task<IReadOnlyList<Message>> Remove(int id) =>
        await _clientService.Delete(id);

    [HttpPost("create")]
    public async Task<IReadOnlyList<Message>> Create(ClientCreateInfo clientCreateInfo) =>
        await _clientService.Add(clientCreateInfo);

    [HttpPut("update")]
    public async Task<IReadOnlyList<Message>> Update(ClientUpdate clientUpdate) =>
        await _clientService.Update(clientUpdate);

    [HttpPost("addFounder")]
    public async Task<IReadOnlyList<Message>> AddFounder(int idClient, int idFounder) =>
        await _clientService.AddFounder(idClient, idFounder);

    [HttpDelete("removeFounder")]
    public async Task<IReadOnlyList<Message>> RemoveFounder(int idClient, int idFounder) =>
        await _clientService.RemoveFounder(idClient, idFounder);
}