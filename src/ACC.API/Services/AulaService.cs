using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Core;
using ACC.Shared.DTOs;
using ACC.Shared.Interfaces;
using ACC.Shared.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ACC.API.Services
{
    public class AulaService : IAulaService
    {
        private readonly ACCDbContext _context;
        private readonly IMapper _mapper;
        private readonly ServiceEndpointsOptions _endpoints;

        public AulaService(ACCDbContext context, IMapper mapper, IOptions<ServiceEndpointsOptions> endpointOptions)
        {
            _context = context;
            _mapper = mapper;
            _endpoints = endpointOptions.Value;
        }

        public async Task<ServiceResult<InvitacionGeneradaDto>> GenerarInvitacionAsync(
            int aulaId,
            string currentUserId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (aulaId <= 0)
                    return ServiceResult<InvitacionGeneradaDto>.Fail("El ID del aula es invalido.");

                if (string.IsNullOrWhiteSpace(currentUserId))
                    return ServiceResult<InvitacionGeneradaDto>.Unauthorized("Usuario no autenticado.");

                var existe = await _context.Aulas
                    .AsNoTracking()
                    .AnyAsync(a => a.AulaId == aulaId, cancellationToken);

                if (!existe)
                    return ServiceResult<InvitacionGeneradaDto>.NotFound("El aula no existe.");

                var token = Guid.NewGuid().ToString("N");
                var invitacion = new InvitacionAula
                {
                    AulaId = aulaId,
                    Token = token,
                    Activa = true,
                    ExpiraEn = DateTime.UtcNow.AddDays(7),
                    CreatedAt = DateTime.UtcNow,
                    NumUsos = 0
                };

                await _context.InvitacionesAula.AddAsync(invitacion, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var webAppBase = NormalizeBaseUrl(_endpoints.WebAppBaseUrl);
                var link = $"{webAppBase}invitacion/aula/{token}";

                var invitacionDto = new InvitacionGeneradaDto
                {
                    Token = token,
                    LinkInvitacion = link,
                    ExpiraEn = invitacion.ExpiraEn
                };

                return ServiceResult<InvitacionGeneradaDto>.Ok(invitacionDto, "Invitacion generada correctamente.");
            }
            catch (Exception ex)
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
            await Task.CompletedTask;
            return ServiceResult<AulaConfigDto>.Fail("Not implemented yet.");
        }

        private static string NormalizeBaseUrl(string? rawBaseUrl)
        {
            if (string.IsNullOrWhiteSpace(rawBaseUrl))
            {
                throw new InvalidOperationException("ServiceEndpoints:WebAppBaseUrl no esta configurado.");
            }

            var normalized = rawBaseUrl.Trim();
            return normalized.EndsWith("/", StringComparison.Ordinal) ? normalized : $"{normalized}/";
        }
    }
}
