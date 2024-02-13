using Microsoft.AspNetCore.Mvc;
using TestAPI.Services;
using TestAPI.ViewModels;

namespace TestAPI.Controllers;

[ApiController]
[Route("api/founders")]
public class FounderController : Controller
{
    private readonly FounderService _founderService;

    public FounderController(FounderService founderService)
    {
        _founderService = founderService;
    }

    [HttpGet]
    [Route("page/{pageNumber}")]
    public async Task<IReadOnlyList<FounderMainInfo>> GetFounders(int pageNumber) =>
        await _founderService.GetAll(pageNumber, 3);

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FounderMainInfo>> GetFounderById(int id)
    {
        var result = await _founderService.Get(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FounderMainInfo>> Remove(int id)
    {
        var result = await _founderService.Remove(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(FounderCreateInfo founderCreateInfo)
    {
        if (ModelState.IsValid)
        {
            var client = await _founderService.Add(founderCreateInfo);
            if (client == null)
            {
                return StatusCode(205, "Учредитель с таким ИНН уже существует");
            }

            return Ok(client);
        }

        return BadRequest(ModelState);
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update(FounderUpdate founderUpdate)
    {
        if (ModelState.IsValid)
        {
            var client = await _founderService.Update(founderUpdate);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        return BadRequest(ModelState);
    }
}