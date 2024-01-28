using Microsoft.AspNetCore.Mvc;
using TestAPI.Domain.Models;
using TestAPI.Services;
using TestAPI.ViewModels;

namespace TestAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController
{
    private readonly ClientService _clientService;

    private readonly ILogger<ClientController> _logger;

    public ClientController(ClientService clientService, ILogger<ClientController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IReadOnlyList<ClientMainInfo>> Get() =>
        await _clientService.GetAll();
    
    [HttpGet("/{id}")]
    public async Task<ClientMainInfo> Get(int id) =>
        await _clientService.Get(id);
    
    [HttpDelete("remove/{id}")]
    public async Task Remove(int id) =>
        await _clientService.Delete(id);
    
    [HttpPost("create")]
    public async Task<IReadOnlyList<Message>> Create(ClientCreateInfo clientCreateInfo) =>
        await _clientService.Add(clientCreateInfo);
    
    [HttpPut("update")]
    public async Task<IReadOnlyList<Message>> Update(ClientUpdate clientUpdate) =>
        await _clientService.Update(clientUpdate);
    
    [HttpPost("AddFounder")]
    public async Task AddFounder(int idClient, int idFounder) =>
        await _clientService.AddFounder(idClient,idFounder);
    
    [HttpDelete("RemoveFounder")]
    public async Task RemoveFounder(int idClient, int idFounder) =>
        await _clientService.RemoveFounder(idClient,idFounder);
}