using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.DTOs;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ACC.API.Services
{
    public class ProgresoUsuarioService : IProgresoUsuarioService
    {
        private readonly ACCDbContext _dbContext;
        private readonly IPrerrequisitosService _prerrequisitosService;

        public ProgresoUsuarioService(ACCDbContext dbContext, IPrerrequisitosService prerrequisitosService)
        {
            _dbContext = dbContext;
            _prerrequisitosService = prerrequisitosService;
        }

        public async Task GuardarProgresoAsync(string usuarioId, int SubTemaId)
        {
            if (string.IsNullOrEmpty(usuarioId))
                throw new ArgumentException("El ID del usuario no puede estar vacío.", nameof(usuarioId));

            var progreso = await _dbContext.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == SubTemaId);

            if (progreso == null)
            {
                progreso = new ProgresoUsuario
                {
                    UsuarioId = usuarioId,
                    SubTemaId = SubTemaId,
                    Fecha = DateTimeOffset.UtcNow
                };
                await _dbContext.ProgresoUsuarios.AddAsync(progreso);
            }
            else
            {
                progreso.Fecha = DateTimeOffset.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<int?> ObtenerUltimoTemaAsync(string usuarioId)
        {
            var subTemaId = await _dbContext.ProgresoUsuarios
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.Fecha)
                .Select(p => (int?)p.SubTemaId)
                .FirstOrDefaultAsync();

            return subTemaId;
        }

        public async Task MarcarSubtemaComoCompletadoAsync(string usuarioId, int subTemaId)
        {
            var progreso = await _dbContext.ProgresoUsuarios
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == subTemaId);

            if (progreso is null)
            {
                progreso = new ProgresoUsuario
                {
                    UsuarioId = usuarioId,
                    SubTemaId = subTemaId,
                    Fecha = DateTimeOffset.UtcNow,
                    Completado = true
                };
                _dbContext.ProgresoUsuarios.Add(progreso);
            }
            else
            {
                if (!progreso.Completado)
                    progreso.Completado = true;

                progreso.Fecha = DateTimeOffset.UtcNow;
            }

            await _dbContext.SaveChangesAsync();
            await _prerrequisitosService.EvaluarDesbloqueosPorProgresoAsync(usuarioId, subTemaId);
        }

        public Task<bool> EstaSubtemaCompletadoAsync(string usuarioId, int subTemaId)
        {
            return _dbContext.ProgresoUsuarios
                .AsNoTracking()
                .AnyAsync(p => p.UsuarioId == usuarioId && p.SubTemaId == subTemaId && p.Completado);
        }

        public async Task<GuiaResumenDto> ObtenerResumenGuiaAsync(string usuarioId)
        {
            if (string.IsNullOrWhiteSpace(usuarioId))
                throw new ArgumentException("El ID del usuario no puede estar vacio.", nameof(usuarioId));

            var modulos = await _dbContext.Modulos
                .AsNoTracking()
                .OrderBy(m => m.Id_Modulo)
                .Select(m => new GuiaModuloResumenDto
                {
                    ModuloId = m.Id_Modulo,
                    NombreModulo = m.NombreModulo,
                    DescripcionModulo = m.DescripcionModulo,
                    Orden = m.Id_Modulo,
                    SubModulosCount = m.SubModulos.Count(),
                    TemasCount = m.SubModulos.SelectMany(sm => sm.Temas).Count(),
                    SubTemasCount = m.SubModulos.SelectMany(sm => sm.Temas).SelectMany(t => t.SubTemas).Count(),
                    LeccionesCount = m.SubModulos.SelectMany(sm => sm.Temas).SelectMany(t => t.SubTemas).SelectMany(st => st.Lecciones).Count(),
                    PracticasCount = m.SubModulos
                        .SelectMany(sm => sm.Temas)
                        .SelectMany(t => t.SubTemas)
                        .SelectMany(st => st.Lecciones)
                        .Count(l => l.Bloques.Any(b =>
                            b.TipoBloque == TipoBloqueLeccion.ActividadExterna ||
                            b.TipoBloque == TipoBloqueLeccion.Compilador ||
                            b.TipoBloque == TipoBloqueLeccion.OpcionMultiple ||
                            b.TipoBloque == TipoBloqueLeccion.VerdaderoFalso ||
                            b.TipoBloque == TipoBloqueLeccion.RespuestaCorta ||
                            b.TipoBloque == TipoBloqueLeccion.Checklist)),
                    EvaluacionesCount = m.SubModulos.SelectMany(sm => sm.Examenes!).Count()
                        + _dbContext.ExamenesModulos.Count(em => em.ModuloId == m.Id_Modulo)
                })
                .ToListAsync();

            var subTemasCompletadosPorModulo = await _dbContext.ProgresoUsuarios
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId && p.Completado)
                .Select(p => new
                {
                    p.SubTemaId,
                    ModuloId = p.SubTema!.Tema.SubModulo.Id_Modulo
                })
                .Distinct()
                .GroupBy(x => x.ModuloId)
                .Select(g => new
                {
                    ModuloId = g.Key,
                    Total = g.Count()
                })
                .ToDictionaryAsync(x => x.ModuloId, x => x.Total);

            foreach (var modulo in modulos)
            {
                modulo.SubTemasCompletados = subTemasCompletadosPorModulo.GetValueOrDefault(modulo.ModuloId);
                modulo.ProgresoPorcentaje = modulo.SubTemasCount == 0
                    ? 0
                    : (int)Math.Round((double)modulo.SubTemasCompletados * 100 / modulo.SubTemasCount, MidpointRounding.AwayFromZero);
            }

            var totalSubTemas = modulos.Sum(m => m.SubTemasCount);
            var totalSubTemasCompletados = modulos.Sum(m => m.SubTemasCompletados);

            return new GuiaResumenDto
            {
                TotalModulos = modulos.Count,
                TotalLecciones = modulos.Sum(m => m.LeccionesCount),
                TotalEvaluaciones = modulos.Sum(m => m.EvaluacionesCount),
                TotalPracticas = modulos.Sum(m => m.PracticasCount),
                TotalSubTemas = totalSubTemas,
                SubTemasCompletados = totalSubTemasCompletados,
                ProgresoPorcentaje = totalSubTemas == 0
                    ? 0
                    : (int)Math.Round((double)totalSubTemasCompletados * 100 / totalSubTemas, MidpointRounding.AwayFromZero),
                Modulos = modulos
            };
        }

        public Task<bool> ExamenHabilitadoAsync(string userId, ExamenRef examen)
            => _prerrequisitosService.EstaHabilitadoAsync(userId, examen);

        public async Task<bool> ExamenSubModuloHabilitadoAsync(string userId, int subModuloId)
        {
            var examenId = await _dbContext.ExamenesSubModulo
                .Where(e => e.SubModuloId == subModuloId)
                .Select(e => (int?)e.Id)
                .FirstOrDefaultAsync();

            return examenId is int id
                && id > 0
                && await ExamenHabilitadoAsync(userId, new ExamenRef(ExamenTipo.SubModulo, id));
        }

        public async Task<bool> ExamenModuloHabilitadoAsync(string userId, int moduloId)
        {
            var examenId = await _dbContext.ExamenesModulos
                .Where(e => e.ModuloId == moduloId)
                .Select(e => (int?)e.Id)
                .FirstOrDefaultAsync();

            return examenId is int id
                && id > 0
                && await ExamenHabilitadoAsync(userId, new ExamenRef(ExamenTipo.Modulo, id));
        }

        public Task<bool> ExamenLibreHabilitadoAsync(string userId, int examenId)
            => ExamenHabilitadoAsync(userId, new ExamenRef(ExamenTipo.Libre, examenId));
    }
}
