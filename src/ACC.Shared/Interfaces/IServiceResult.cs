using ACC.Shared.DTOs;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ACC.Shared.Interfaces
{
    public interface IServiceResult
    {
        bool Exito { get; set; }
        string? Mensaje { get; set; }
    }
}