using Microsoft.AspNetCore.Mvc;
using TestAPI.Domain.Models;
using TestAPI.Services;
using TestAPI.ViewModels;

namespace TestAPI.Controllers;

[ApiController]
[Route("api/founders")]
public class FounderController
{
    private readonly FounderService _founderService;

    public FounderController(FounderService founderService)
    {
        _founderService = founderService;
    }

    [HttpGet]
    public async Task<IReadOnlyList<FounderMainInfo>> GetFounders() =>
        await _founderService.GetAll();

    [HttpGet("{id}")]
    public async Task<FounderMainInfo> GetFounderById(int id) =>
        await _founderService.Get(id);

    [HttpDelete("remove/{id}")]
    public async Task<IReadOnlyList<Message>> Remove(int id) =>
        await _founderService.Delete(id);

    [HttpPost]
    public async Task<IReadOnlyList<Message>> Create(FounderCreateInfo founderCreateInfo) =>
        await _founderService.Add(founderCreateInfo);

    [HttpPut]
    public async Task<IReadOnlyList<Message>> Update(FounderUpdate founderUpdate) =>
        await _founderService.Update(founderUpdate);
}