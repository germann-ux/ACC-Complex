using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaController(IAulaService aulaService) : ControllerBase
    {
        private readonly IAulaService _aulaService = aulaService;
        // TODO: seguir con este controllador luego lol
    }
}



