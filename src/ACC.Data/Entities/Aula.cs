using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities;

public class Aula
{
    public int AulaId { get; set; }

    public string Nombre { get; set; } = null!;
    public string? Descripcion { get; set; }

    public bool CerrarAula { get; set; } = false;
    public bool ArchivarAula { get; set; } = false;

    public DateTime FechaCreacion { get; set; } // UTC
    public DateTime FechaActualizacion { get; set; } // UTC

    public int? ModuloId { get; set; }
    public int? SubModuloId { get; set; }

    public string DocenteId { get; set; } = null!;

    // Navs académicas (tipos Modulo/SubModulo/Usuario existen fuera de este snippet)
    public Modulo? Modulo { get; set; }
    public SubModulo? SubModulo { get; set; }
    public Usuario? Docente { get; set; }

    // Navs propias
    public ICollection<AulaEstudiante> AulaEstudiantes { get; set; } = [];
    public ICollection<Anuncio> Anuncios { get; set; } = [];
    public ICollection<Tarea> Tareas { get; set; } = [];
    public ICollection<Evaluacion> Evaluaciones { get; set; } = [];
    public ICollection<InvitacionAula> Invitaciones { get; set; } = [];
    public ICollection<Notificacion> Notificaciones { get; set; } = [];
}