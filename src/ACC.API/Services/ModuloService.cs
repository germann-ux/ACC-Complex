using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ACC.API.Services
{
    public class ModuloService : IModuloService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper;

        public ModuloService(ACCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<ModuloDto> CreateModuloAsync(ModuloDto modulo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteModuloAsync(int moduloId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<ModuloDto>>> GetAllModulosAsync()
        {
            // Logica para obtener los modulos.
            var modulos = await _context.Modulos.ToListAsync();
            if (modulos.Count == 0)
            {
                return ServiceResult<IEnumerable<ModuloDto>>.Fail("No se encontraron módulos.");
            }
            else
            {
                var modulosDto = _mapper.Map<IEnumerable<ModuloDto>>(modulos);
                return ServiceResult<IEnumerable<ModuloDto>>.Ok(modulosDto);
            }
        }

        public Task<ModuloDto> GetModuloByIdAsync(int moduloId)
        {
            throw new NotImplementedException();
        }

        public Task<ModuloDto?> GetModuloSeleccionadoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioModulosDto> GetProgresoUsuarioModulos(string IdUsuario, int IdModulo)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ModuloDto>> SearchModulosByNameAsync(string nombreModulo)
        {
            throw new NotImplementedException();
        }

        public Task<ModuloDto> UpdateModuloAsync(ModuloDto modulo)
        {
            throw new NotImplementedException();
        }
    }
}
