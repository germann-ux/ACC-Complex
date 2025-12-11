using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

// TODO: Implementar los métodos de la interfaz IAulaService

namespace ACC.API.Services
{
    public class AulaService(ACCDbContext context, IMapper mapper) : IAulaService
    {
        public async Task<ServiceResult<InvitacionGeneradaDto>> GenerarInvitacionAsync
            (int aulaId,
            string currentUserId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (aulaId <= 0)
                    return ServiceResult<InvitacionGeneradaDto>.Fail("El ID del aula es inválido.");
                
                if (string.IsNullOrWhiteSpace(currentUserId))
                    return ServiceResult<InvitacionGeneradaDto>.Unauthorized("Usuario no autenticado.");

                var existe = await context.Aulas
                    .AsNoTracking()
                    .AnyAsync(a => a.AulaId == aulaId, cancellationToken);

                if (!existe)
                    return ServiceResult<InvitacionGeneradaDto>.NotFound("El aula no existe.");

                string token = Guid.NewGuid().ToString("N");
                var invitacion = new InvitacionAula
                {
                    AulaId = aulaId,
                    Token = token,
                    Activa = true,
                    ExpiraEn = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    NumUsos = 0
                };

                await context.InvitacionesAula.AddAsync(invitacion, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var link = $"{ServiceRoots.ACC_WEBAPP_Url}invitacion/aula/{token}"; 

                var invitacionDto = new InvitacionGeneradaDto
                {
                    Token = token,
                    LinkInvitacion = link,
                    ExpiraEn = invitacion.ExpiraEn
                };

                return ServiceResult<InvitacionGeneradaDto>.Ok(invitacionDto, "Invitación generada correctamente.");
            }
            catch(Exception ex)
            {
                return ServiceResult<InvitacionGeneradaDto>.Error(ex);
            }
        }

        public Task<ServiceResult<AulaConfigDto>> GetConfigAsync(int aulaId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<AulaConfigDto>> UpdateConfigAsync(int aulaId, AulaConfigUpdateDto updateDto, string currentUserId, CancellationToken cancellationToken = default)
        {
            //if(aulaId <= 0)
            //    return ServiceResult<AulaConfigDto>.Fail("El ID del aula es inválido.");

            //var aula = await context.Aulas
            //    .FirstOrDefaultAsync(a => a.AulaId == aulaId, cancellationToken);
            
            //if(aula == null)
            //    return ServiceResult<AulaConfigDto>.NotFound("El aula no existe.");

            return ServiceResult<AulaConfigDto>.Fail("Not implemented yet.");
        }
    }
}



