using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Security;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly ITokenHelper _tokenHelper;

    public AuthController(ILogger<AuthController> logger, ITokenHelper tokenHelper)
    {
        _logger = logger;
        _tokenHelper = tokenHelper;
    }

    [HttpPost(Name = "Login")]
    public async Task<IActionResult> PostLogin([FromBody] LoginModel model)
    {
        var token = await _tokenHelper.Authenticate(model.Email, model.Password);
        if (token == null)
            return Unauthorized();

        return Ok(new { Token = token });
    }
}
