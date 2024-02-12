using System.Security.Claims;

namespace WebAPI.Security;

public interface ITokenHelper
{
    Task<string?> Authenticate(string email, string password);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
