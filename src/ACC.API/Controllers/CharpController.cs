using ACC.ExternalClients.Common;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ACC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharpController(IChatbaseClient chatbaseClient) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("embed-url")]
    public ActionResult<ServiceResult<CharpEmbedConfigDto>> GetEmbedUrl()
    {
        var iframeUrl = chatbaseClient.GetIframeUrl();
        if (string.IsNullOrWhiteSpace(iframeUrl))
        {
            return StatusCode(
                HttpStatusCodes.NotFound,
                ServiceResult<CharpEmbedConfigDto>.NotFound("La integración de Charp no está configurada."));
        }

        return Ok(ServiceResult<CharpEmbedConfigDto>.Ok(new CharpEmbedConfigDto
        {
            IframeUrl = iframeUrl
        }));
    }
}
