using Domain.Aggregate;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

// TODO: Autorization needs to be fixed
//[Authorize]
[ApiController]
[Route("[controller]")]
public class UniqueAccountController : ControllerBase
{
    private readonly ILogger<UniqueAccountController> _logger;
    private readonly IUniqueAccountService _uniqueAccountService;

    public UniqueAccountController(ILogger<UniqueAccountController> logger, IUniqueAccountService uniqueAccountService)
    {
        _logger = logger;
        _uniqueAccountService = uniqueAccountService;
    }

    // GET: api/uniqueaccounts
    [HttpGet]
    public async Task<IActionResult> GetAllUniqueAccounts()
    {
        var uniqueAccounts = await _uniqueAccountService.GetAllUniqueAccountsAsync();
        return Ok(uniqueAccounts);
    }

    // GET: api/uniqueaccounts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUniqueAccountById(string id)
    {
        var uniqueAccount = await _uniqueAccountService.GetUniqueAccountByIdAsync(id);
        if (uniqueAccount == null)
        {
            return NotFound();
        }
        return Ok(uniqueAccount);
    }

    // POST: api/uniqueaccounts
    [HttpPost]
    public async Task<IActionResult> CreateUniqueAccount([FromBody] UniqueAccountAggregate uniqueAccount)
    {
        var createdUniqueAccount = await _uniqueAccountService.CreateUniqueAccountAsync(uniqueAccount);
        //return CreatedAtAction(nameof(GetUniqueAccountById), new { success = createdUniqueAccount }, createdUniqueAccount);
        return Ok(new { success = createdUniqueAccount });
    }

    // PUT: api/uniqueaccounts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUniqueAccount(string id, [FromBody] UniqueAccountAggregate uniqueAccount)
    {
        var updatedUniqueAccount = await _uniqueAccountService.UpdateUniqueAccountAsync(uniqueAccount);
        if (!updatedUniqueAccount)
        {
            return NotFound();
        }
        return Ok(updatedUniqueAccount);
    }

    // DELETE: api/uniqueaccounts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUniqueAccount(string id)
    {
        var isDeleted = await _uniqueAccountService.DeleteUniqueAccountAsync(id);
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
