using ACC.API.Extensions;
using ACC.API.Interfaces;
using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ACC.API.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioService(ACCDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResult<double>> GetProgresoUserByIdAsync(string idUsuario)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == idUsuario);
            if (usuario == null)
            {
                return ServiceResult<double>.NotFound("Usuario no encontrado");
            }
            else
            {
                double progreso = (double)usuario.ProgresoGeneral;
                await _context.SaveChangesAsync();
                return ServiceResult<double>.Ok(progreso);
            }
        }

        public async Task<ServiceResult<ApplicationUserDto>> GetUserByIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return ServiceResult<ApplicationUserDto>.Fail("userId es requerido.");

            var usuario = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (usuario is null)
                return ServiceResult<ApplicationUserDto>.NotFound("Usuario no encontrado");

            var dto = _mapper.Map<ApplicationUserDto>(usuario);
            return ServiceResult<ApplicationUserDto>.Ok(dto);
        }

        public async Task<ServiceResult<ApplicationUserDto>> GetUserByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        // metodo responsable de sincronizar el usuario con la base de datos o de crearlo inicialmente.
        public async Task<ServiceResult<ApplicationUserDto>> SincUserAsync(ApplicationUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Id))
                return ServiceResult<ApplicationUserDto>.Fail("El Id del usuario (Identity) es obligatorio.");

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (usuario is null)
            {
                var nuevoUsuario = _mapper.Map<Usuario>(dto);
                // Asegura que preserve el Id del DTO:
                nuevoUsuario.Id = dto.Id;
                await _context.Usuarios.AddAsync(nuevoUsuario);
            }
            else
            {
                usuario.Nombre = dto.Nombre;
                usuario.Email = dto.Email;
                usuario.ProgresoGeneral = dto.ProgresoGeneral;
            }

            await _context.SaveChangesAsync();
            return ServiceResult<ApplicationUserDto>.Ok(dto);
        }
    }
}
