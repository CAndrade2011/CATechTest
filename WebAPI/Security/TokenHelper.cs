using Domain.Service;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Security;

public class TokenHelper: ITokenHelper
{
    private readonly IConfiguration _configuration;
    private readonly IUniqueAccountService _uniqueAccountService;

    public TokenHelper(IConfiguration configuration, IUniqueAccountService uniqueAccountService)
    {
        _configuration = configuration;
        _uniqueAccountService = uniqueAccountService;
    }

    public async Task<string?> Authenticate(string email, string password)
    {
        var query = new Domain.Query.GetUniqueAccountQuery { Email = email, Password = password };
        var uaDb = await _uniqueAccountService.GetUniqueAccountQueryHandlerAsync(query);

        if (uaDb == null || uaDb.Email != email) return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, uaDb.Name),
                new Claim(ClaimTypes.Role, uaDb.IsAdmin ? "Admin" : "NormalUser")
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Tempo de expiração do token de acesso
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false, // Importante: desabilitar a validação de tempo de vida
            ClockSkew = TimeSpan.Zero // Zero o ClockSkew para que o token expire exatamente no tempo especificado
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid Token");

        return principal;
    }
}
