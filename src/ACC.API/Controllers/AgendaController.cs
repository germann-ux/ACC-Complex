using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendaController : ControllerBase
    {
        // TODO: controlador de la agenda
        // uri de pruebas: https://localhost:7130/api/Agenda/user/dummy_user
        private readonly IAgendaService _agendaService;
        private readonly ILogger<AgendaController> _logger;

        public AgendaController(IAgendaService agendaService, ILogger<AgendaController> logger)
        {
            _agendaService = agendaService;
            _logger = logger;
        }
    }
}
