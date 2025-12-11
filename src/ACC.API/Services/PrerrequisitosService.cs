using ACC.Data;
using ACC.Data.Entities;
using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ACC.API.Services; 

public class PrerrequisitosService : IPrerrequisitosService
{
    private readonly ACCDbContext _db;
    private readonly ILogger<PrerrequisitosService> _logger;

    public PrerrequisitosService(ACCDbContext db, ILogger<PrerrequisitosService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<bool> EstaHabilitadoAsync(string userId, ExamenRef examen)
    {
        return await _db.ExamenesHabilitados.AsNoTracking()
            .AnyAsync(e =>
                e.UsuarioId == userId &&
                e.Tipo == examen.Tipo &&
                e.RefId == examen.RefId &&
                e.Habilitado);
    }

    public async Task EvaluarDesbloqueosPorProgresoAsync(string userId, int subTemaId)
    {
        var subTema = await _db.SubTemas.AsNoTracking()
            .Include(st => st.Tema)
            .FirstOrDefaultAsync(st => st.Id_SubTema == subTemaId);

        if (subTema?.Tema is null) return;

        var subModuloId = subTema.Tema.Id_SubModulo;

        if (await SubmoduloCompletadoAsync(userId, subModuloId))
            await UpsertHabilitacionAsync(userId, new ExamenRef(ExamenTipo.SubModulo, subModuloId),
                "ReglaA:TodosSubtemasCompletados");
    }

    public async Task EvaluarDesbloqueosPorAprobacionAsync(string userId, int examenSubModuloId)
    {
        var examenSubM = await _db.ExamenesSubModulo.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == examenSubModuloId);
        if (examenSubM is null) return;

        var moduloId = await _db.SubModulos
            .Where(sm => sm.Id_SubModulo == examenSubM.SubModuloId)
            .Select(sm => sm.Id_Modulo)
            .FirstOrDefaultAsync();

        if (moduloId == default) return;

        if (await TodosExamenesSubModuloAprobadosAsync(userId, moduloId))
            await UpsertHabilitacionAsync(userId, new ExamenRef(ExamenTipo.Modulo, moduloId),
                "ReglaB:TodosSubModulosAprobados");
    }

    // Para la fachada vieja:
    public async Task EvaluarDesbloqueoSubmoduloAsync(string userId, int subModuloId)
    {
        if (await SubmoduloCompletadoAsync(userId, subModuloId))
            await UpsertHabilitacionAsync(userId, new ExamenRef(ExamenTipo.SubModulo, subModuloId),
                "Compat:RecalculoSubmodulo");
    }

    // ---- helpers de reglas ----
    private async Task<bool> SubmoduloCompletadoAsync(string userId, int subModuloId)
    {
        var temaIds = await _db.Temas
            .Where(t => t.Id_SubModulo == subModuloId)
            .Select(t => t.Id_Tema)
            .ToListAsync();
        if (temaIds.Count == 0) return false;

        var subTemaIds = await _db.SubTemas
            .Where(st => temaIds.Contains(st.Id_Tema))
            .Select(st => st.Id_SubTema)
            .ToListAsync();
        if (subTemaIds.Count == 0) return false;

        var completados = await _db.ProgresoUsuarios
            .Where(p => p.UsuarioId == userId && p.Completado && subTemaIds.Contains(p.SubTemaId))
            .Select(p => p.SubTemaId)
            .Distinct()
            .CountAsync();

        _logger.LogInformation("CHK user={User} submod={SM} temas={T} subtemas={ST} completados={C}",
            userId, subModuloId, temaIds.Count, subTemaIds.Count, completados);

        return completados >= subTemaIds.Count;
    }


    private async Task<bool> TodosExamenesSubModuloAprobadosAsync(string userId, int moduloId)
    {
        var subModuloIds = await _db.SubModulos
            .Where(sm => sm.Id_Modulo == moduloId)
            .Select(sm => sm.Id_SubModulo)
            .ToListAsync();
        if (subModuloIds.Count == 0) return false;

        var examenesSubMIds = await _db.ExamenesSubModulo
            .Where(e => subModuloIds.Contains(e.SubModuloId))
            .Select(e => e.Id)
            .ToListAsync();
        if (examenesSubMIds.Count == 0) return false;

        var aprobadosPorExamen = await _db.ExamenesIntentos
            .Where(i => i.IdUsuario == userId && i.ExamenSubModuloId != null &&
                        examenesSubMIds.Contains(i.ExamenSubModuloId.Value) && i.Aprobado)
            .GroupBy(i => i.ExamenSubModuloId!.Value)
            .Select(g => g.Key)
            .CountAsync();

        return aprobadosPorExamen >= examenesSubMIds.Count;
    }

    private async Task UpsertHabilitacionAsync(string userId, ExamenRef examen, string regla)
    {
        var strategy = _db.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("UPSERT start user={User} tipo={Tipo} ref={Ref} regla={Regla}",
                    userId, (int)examen.Tipo, examen.RefId, regla);

                var row = await _db.ExamenesHabilitados
                    .FirstOrDefaultAsync(e => e.UsuarioId == userId && e.Tipo == examen.Tipo && e.RefId == examen.RefId);

                if (row is null)
                {
                    _logger.LogWarning("No existe fila, se insertará habilitación");
                    _logger.LogInformation("Upsert EH: userId={UserId}, tipo={Tipo}, ref={RefId}", userId, examen.Tipo, examen.RefId);
                    row = new ExamenHabilitado
                    {
                        UsuarioId = userId,
                        Tipo = examen.Tipo,
                        RefId = examen.RefId,
                        Habilitado = true,
                        FechaHabilitacion = DateTimeOffset.UtcNow,
                        ReglaFuente = regla, 
                    };
                    _db.ExamenesHabilitados.Add(row);
                }
                else if (!row.Habilitado)
                {
                    _logger.LogWarning("Existe fila deshabilitada, se habilitará");
                    row.Habilitado = true;
                    row.FechaHabilitacion = DateTimeOffset.UtcNow;
                    row.ReglaFuente = regla;
                }
                else
                {
                    _logger.LogInformation("Ya estaba habilitado: no hay cambios");
                }

                var affected = await _db.SaveChangesAsync();
                _logger.LogInformation("SaveChanges afectados={Affected}", affected);

                await tx.CommitAsync();
                _logger.LogInformation("UPSERT commit OK");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "UP SERT rollback por excepción");
                throw;
            }
        });
    }

}
