using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UniqueAccountController : ControllerBase
{
    private readonly ILogger<UniqueAccountController> _logger;

    public UniqueAccountController(ILogger<UniqueAccountController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "getUniqueAccount")]
    public IEnumerable<bool> Get()
    {
        throw new NotImplementedException();
    }
}
