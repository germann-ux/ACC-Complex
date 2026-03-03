using Microsoft.AspNetCore.Components.Authorization;
using ACC.Shared.Interfaces;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace ACC.WebApp.Services;

public sealed class ApiJwtAuthDelegatingHandler(
    AuthenticationStateProvider authenticationStateProvider,
    IHttpContextAccessor httpContextAccessor,
    IApiJwtTokenService tokenService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization is null)
        {
            var user = await ResolveUserAsync();
            var token = user is null ? null : tokenService.TryCreateAccessToken(user);

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<ClaimsPrincipal?> ResolveUserAsync()
    {
        try
        {
            var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
            if (authState.User.Identity?.IsAuthenticated == true)
            {
                return authState.User;
            }
        }
        catch
        {
            // Fallback al HttpContext si no hay estado de autenticación del circuito.
        }

        var httpUser = httpContextAccessor.HttpContext?.User;
        return httpUser?.Identity?.IsAuthenticated == true ? httpUser : null;
    }
}
