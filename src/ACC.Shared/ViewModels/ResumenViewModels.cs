using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.ViewModels;

public sealed record NotasSerieVm(
    IReadOnlyList<string> Labels,
    IReadOnlyList<decimal> Valores);

public sealed record ProgresoVm(
    int ModulosCompletados,
    int ModulosTotales)
{
    public int Porcentaje => ModulosTotales is 0 ? 0 : (int)Math.Round((decimal)ModulosCompletados * 100 / ModulosTotales);
}

public sealed record TareaResumenVm(
    string Id,
    string Titulo,
    string Descripcion,
    DateTime FechaLimite,
    string Tipo); // "Asignada" | "Personal"
