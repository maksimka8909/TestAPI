using Microsoft.AspNetCore.Mvc;
using TestAPI.Domain.Models;
using TestAPI.Services;
using TestAPI.ViewModels;

namespace TestAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FounderController
{
    private readonly FounderService _founderService;
    
    private readonly ILogger<FounderController> _logger;

    public FounderController(FounderService founderService, ILogger<FounderController> logger)
    {
        _founderService = founderService;
        _logger = logger;
    }
    
    [HttpGet("getall")]
    public async Task<IReadOnlyList<FounderMainInfo>> Get() =>
        await _founderService.GetAll();
    
    [HttpGet("get/{id}")]
    public async Task<FounderMainInfo> Get(int id) =>
        await _founderService.Get(id);
    
    [HttpDelete("remove/{id}")]
    public async Task Remove(int id) =>
        await _founderService.Delete(id);
    
    [HttpPost("create")]
    public async Task<IReadOnlyList<Message>> Create(FounderCreateInfo founderCreateInfo) =>
        await _founderService.Add(founderCreateInfo);
    
    [HttpPut("update")]
    public async Task<IReadOnlyList<Message>> Update(FounderUpdate founderUpdate) =>
        await _founderService.Update(founderUpdate);
}