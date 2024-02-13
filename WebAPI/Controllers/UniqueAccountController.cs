using Domain.Aggregate;
using Domain.Command;
using Domain.DTO;
using Domain.Service;
using Infra.DataFromMongo.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
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

    // GET: uniqueaccounts
    [HttpGet]
    public async Task<IActionResult> GetAllUniqueAccounts()
    {
        List<UniqueAccountAggregate> uniqueAccounts;
        try
        {
            uniqueAccounts = await _uniqueAccountService.GetAllUniqueAccountsAsync();
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (uniqueAccounts == null)
        {
            return NotFound();
        }
        return Ok(UniqueAccountResultDTO.GenerateListFromAggregates(uniqueAccounts));
    }

    // GET: uniqueaccounts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUniqueAccountById([FromRoute] string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return BadRequest();
        UniqueAccountAggregate uniqueAccount;
        try
        {
            uniqueAccount = await _uniqueAccountService.GetUniqueAccountByIdAsync(id);
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (uniqueAccount == null)
        {
            return NotFound();
        }
        return Ok(new UniqueAccountResultDTO(uniqueAccount));
    }

    // POST: uniqueaccounts
    [HttpPost]
    public async Task<IActionResult> CreateUniqueAccount([FromBody] AddUniqueAccountCommand uniqueAccount)
    {
        if (uniqueAccount == null) return BadRequest();
        bool createdUniqueAccount;
        try
        {
            createdUniqueAccount = await _uniqueAccountService.CreateUniqueAccountAsync(new UniqueAccountAggregate
            {
                DisplayName = uniqueAccount.DisplayName,
                Email = uniqueAccount.Email,
                IsAdmin = uniqueAccount.IsAdmin,
                Name = uniqueAccount.Name,
                Password = uniqueAccount.Password,
            });
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        return Ok(new { success = createdUniqueAccount });
    }

    // PUT: uniqueaccounts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUniqueAccount([FromRoute] string id, [FromBody] UpdUniqueAccountCommand uniqueAccount)
    {
        if (uniqueAccount == null) return BadRequest();
        if (string.IsNullOrWhiteSpace(id)) return BadRequest();
        bool updatedUniqueAccount;
        try
        {
            updatedUniqueAccount = await _uniqueAccountService.UpdateUniqueAccountAsync(new UniqueAccountAggregate
            {
                Id = id,
                DisplayName = uniqueAccount.DisplayName,
                Email = uniqueAccount.Email,
                IsAdmin = uniqueAccount.IsAdmin,
                Name = uniqueAccount.Name,
                Password = uniqueAccount.Password
            });
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (!updatedUniqueAccount)
        {
            return NotFound();
        }
        return Ok(new { success = updatedUniqueAccount });
    }

    // DELETE: uniqueaccounts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUniqueAccount([FromRoute] string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return BadRequest();
        bool isDeleted;
        try
        {
            isDeleted = await _uniqueAccountService.DeleteUniqueAccountAsync(id);
        }
        catch (Exception ex) when (ex is AggregateException)
        {
            return UnprocessableEntity(ex);
        }
        if (!isDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
