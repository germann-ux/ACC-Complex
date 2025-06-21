using ACC.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ACC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AulaController : ControllerBase
    {
        private readonly IAulaService _aulaService;

        public AulaController(IAulaService aulaService)
        {
            _aulaService = aulaService;
        }
        // TODO: seguir con este controllador luego lol
    }
}



