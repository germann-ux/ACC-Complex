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
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == userId);
            if (usuario.Id == null)
            {
                return ServiceResult<ApplicationUserDto>.NotFound("Usuario no encontrado");
            }
            else
            {
                var usuarioDTO = _mapper.Map<ApplicationUserDto>(usuario);
                return ServiceResult<ApplicationUserDto>.Ok(usuarioDTO);
            }
        }

        public async Task<ServiceResult<ApplicationUserDto>> GetUserByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        // metodo responsable de sincronizar el usuario con la base de datos o de crearlo inicialmente.
        public async Task<ServiceResult<ApplicationUserDto>> SincUserAsync(ApplicationUserDto dto)
        {
            // se busca el usuario en la base de datos
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (usuario is null)
            {
                // si no existe el usuario se crea uno nuevo
                var nuevoUsuario = _mapper.Map<Usuario>(dto);
                await _context.Usuarios.AddAsync(nuevoUsuario);
            }
            else
            {
                // si ya existe el usuario se actualiza
                usuario.Nombre = dto.Nombre;
                usuario.Email = dto.Email;
                usuario.ProgresoGeneral = dto.ProgresoGeneral;
            }

            await _context.SaveChangesAsync();
            return ServiceResult<ApplicationUserDto>.Ok(dto);
        }

    }
}
