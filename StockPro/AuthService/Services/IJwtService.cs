using AuthService.Entities;
using System.Security.Claims;

public interface IJwtService
{
    string GenerateToken(User user);

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token); 
}