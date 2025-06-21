using ACC.API.Interfaces;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BibliotecaController : ControllerBase
    {
        private readonly IBibliotecaService _servicio;

        public BibliotecaController(IBibliotecaService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet("contenidos")]
        public async Task<ServiceResult<List<ContenidoCapituloDto>>> ObtenerContenidos()
        {
            var resultado = await _servicio.ObtenerContenidosAsync();
            if (resultado.Success)
            {
                return ServiceResult<List<ContenidoCapituloDto>>.Ok(resultado.Data, resultado.Message);
            }
            else
            { 
                return ServiceResult<List<ContenidoCapituloDto>>.Fail(resultado.Message, resultado.StatusCode);
            }
        }

        [HttpGet("Capitulo/{Id}")]
        public async Task<ServiceResult<ContenidoCapituloDto>> ObtenerCapituloAsync(int Id)
        {
            var resultado = await _servicio.ObtenerCapituloAsync(Id);
            if (resultado.Success)
            {
                return ServiceResult<ContenidoCapituloDto>.Ok(resultado.Data, resultado.Message); 
            }
            else
            {
                return ServiceResult<ContenidoCapituloDto>.Fail(resultado.Message, resultado.StatusCode); 
            }
        }
    }
}