using System.Security.Claims;

namespace ACC.Shared.Interfaces;

public interface IApiJwtTokenService
{
    string? TryCreateAccessToken(ClaimsPrincipal user);
}
