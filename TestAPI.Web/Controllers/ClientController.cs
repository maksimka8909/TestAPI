using Microsoft.AspNetCore.Mvc;
using TestAPI.Services;
using TestAPI.ViewModels;

namespace TestAPI.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientController : Controller
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    [Route("page/{pageNumber}")]
    public async Task<IReadOnlyList<ClientMainInfo>> GetClients(int pageNumber) =>
        await _clientService.GetAll(pageNumber, 3);

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientMainInfo>> GetClientById(int id)
    {
        var result = await _clientService.Get(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientMainInfo>> Remove(int id)
    {
        var result = await _clientService.Remove(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(ClientCreateInfo clientCreateInfo)
    {
        if (ModelState.IsValid)
        {
            var client = await _clientService.Add(clientCreateInfo);
            if (client == null)
            {
                return StatusCode(205, "Клиент с таким ИНН уже существует");
            }

            return Ok(client);
        }

        return BadRequest(ModelState);
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(ClientUpdate clientUpdate)
    {
        if (ModelState.IsValid)
        {
            var client = await _clientService.Update(clientUpdate);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("founder/add")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AddFounder(int idClient, int idFounder)
    {
        var result = await _clientService.AddFounder(idClient, idFounder);
        if (result != null)
        {
            return Ok(result);
        }

        return BadRequest();
    }

    [HttpDelete("founder/remove")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RemoveFounder(int idClient, int idFounder)
    {
        var result = await _clientService.RemoveFounder(idClient, idFounder);
        if (result != null)
        {
            return Ok(result);
        }

        return NotFound();
    }
}