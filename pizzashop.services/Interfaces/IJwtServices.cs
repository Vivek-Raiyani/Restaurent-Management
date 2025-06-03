using System.Security.Claims;

namespace pizzashop.service.Interface;

public interface IJwtService
{
    string GenerateJwtToken(string email, string role);
    ClaimsPrincipal? ValidateToken(string token);
}
