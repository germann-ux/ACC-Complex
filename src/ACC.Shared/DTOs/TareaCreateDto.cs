using ACC.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs;

/// <summary>
/// Envío desde el cliente para crear una nueva tarea en un aula (lado docente).
/// </summary>
public class TareaCreateDto
{
    public int AulaId { get; set; }
    public string Titulo { get; set; } = null!;
    public DateTime FechaLimite { get; set; }
    public TareaScope Scope { get; set; } = TareaScope.AulaCompleta;
    public string Enunciado { get; set; } = null!;
    // Si Scope=Subconjunto, puedes aceptar opcionalmente la lista:
    public List<string>? UsuarioIds { get; set; }
}