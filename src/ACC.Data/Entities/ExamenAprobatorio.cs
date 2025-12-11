using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities;

/// <summary>
/// Registra el PRIMER intento aprobado de un usuario para un examen (cualquiera de los 3 tipos).
/// </summary>
public class ExamenAprobatorio
{
    public int Id { get; set; }

    public string UsuarioId { get; set; } = default!;
    public Usuario Usuario { get; set; } = default!;

    // Identidad abstracta del examen
    public ExamenTipo Tipo { get; set; }   // <- tu enum
    public int ExamenId { get; set; }      // Id del examen según el Tipo

    // Trazabilidad al intento que aprobó
    public int ExamenIntentoId { get; set; }
    public ExamenIntento ExamenIntento { get; set; } = default!;

    // Datos del aprobatorio
    public DateTimeOffset FechaAprobacion { get; set; }
    public double Calificacion { get; set; }

    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}