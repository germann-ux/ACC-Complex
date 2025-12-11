using ACC.Data.Entities;
using ACC.Shared.Enums;

namespace ACC.API.Extensions; 

public static class ExamenHelpers
{
    public static (ExamenTipo tipo, int examenId) GetIdentidadExamen(this ExamenIntento intento)
    {
        if (intento.ExamenSubModuloId.HasValue) return (ExamenTipo.SubModulo, intento.ExamenSubModuloId.Value);
        if (intento.ExamenModuloId.HasValue) return (ExamenTipo.Modulo, intento.ExamenModuloId.Value);
        if (intento.ExamenId.HasValue) return (ExamenTipo.Libre, intento.ExamenId.Value);
        throw new InvalidOperationException("El intento no referencia ningún examen.");
    }

    public static IQueryable<ExamenIntento> FiltrarIntentosMismoExamen(this IQueryable<ExamenIntento> q, ExamenIntento e)
    {
        if (e.ExamenSubModuloId.HasValue) return q.Where(i => i.IdUsuario == e.IdUsuario && i.ExamenSubModuloId == e.ExamenSubModuloId);
        if (e.ExamenModuloId.HasValue) return q.Where(i => i.IdUsuario == e.IdUsuario && i.ExamenModuloId == e.ExamenModuloId);
        if (e.ExamenId.HasValue) return q.Where(i => i.IdUsuario == e.IdUsuario && i.ExamenId == e.ExamenId);
        return q.Where(_ => false);
    }
}
