using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Entities;
using Microsoft.IdentityModel.Tokens;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;

    public JwtService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(_config["Jwt:ExpiryMinutes"])
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        // Clean and validate token first
        if (string.IsNullOrEmpty(token))
            throw new ArgumentException("Token cannot be null or empty");

        // Remove any quotes or extra whitespace
        token = token.Trim().Trim('"');

        // Remove "Bearer " prefix if present
        if (token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            token = token.Substring(7);

        // Check if token has 3 parts
        var parts = token.Split('.');
        if (parts.Length != 3)
            throw new ArgumentException($"Invalid token format. Expected 3 parts, got {parts.Length}");

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            ),
            ValidateLifetime = false 
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        // Check if token can be read
        if (!tokenHandler.CanReadToken(token))
            throw new ArgumentException("Token is not well-formed and cannot be read");

        var principal = tokenHandler.ValidateToken(
            token,
            tokenValidationParameters,
            out SecurityToken securityToken
        );

        var jwtToken = securityToken as JwtSecurityToken;

        if (jwtToken == null ||
            !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
            StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}