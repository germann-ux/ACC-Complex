using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACC.Data.Entities
{
    /// <summary>
    /// Representa una lección dentro del sistema educativo.
    /// Cada lección pertenece a un SubTema y puede estar asociada a capítulos.
    /// Implementa la interfaz INodoJerarquico para navegación jerárquica.
    /// </summary>
    public class Leccion : INodoJerarquico
    {
        /// <summary>
        /// Identificador único de la lección (clave primaria).
        /// </summary>
        [Key]
        public int IdLeccion { get; set; }

        /// <summary>
        /// Título de la lección.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string TituloLeccion { get; set; } = string.Empty;

        /// <summary>
        /// Descripción breve de la lección.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string DescripcionLeccion { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del SubTema al que pertenece la lección (clave foránea).
        /// </summary>
        [Required]
        public int SubtemaId { get; set; }

        /// <summary>
        /// Identificador opcional del aula cuando la lección pertenece a APP.
        /// </summary>
        public int? AulaId { get; set; }

        /// <summary>
        /// Origen lógico de la lección: contenido oficial o contenido APP.
        /// </summary>
        [Required]
        public OrigenLeccion OrigenLeccion { get; set; } = OrigenLeccion.Oficial;

        /// <summary>
        /// Estado editorial de la lección.
        /// </summary>
        [Required]
        public EstadoLeccion EstadoLeccion { get; set; } = EstadoLeccion.Borrador;

        /// <summary>
        /// Referencia de navegación al SubTema padre.
        /// </summary>
        [ForeignKey("SubtemaId")]
        public SubTema? SubTema { get; set; }

        /// <summary>
        /// Referencia opcional al aula de origen cuando la lección se crea desde APP.
        /// </summary>
        [ForeignKey(nameof(AulaId))]
        public Aula? Aula { get; set; }

        /// <summary>
        /// Colección de capítulos que pueden estar asociados a esta lección (relación inversa).
        /// </summary>
        public ICollection<Capitulo>? Capitulos { get; set; }

        /// <summary>
        /// Bloques ordenados que componen la experiencia didactica de la leccion.
        /// </summary>
        public ICollection<BloqueLeccion> Bloques { get; set; } = [];

        // --- // ------------------ Implementación de la interfaz INodoJerarquico ------------------ // --- //

        /// <summary>
        /// Identificador del nodo jerárquico (Id de la lección).
        /// </summary>
        int INodoJerarquico.Id => IdLeccion;

        /// <summary>
        /// Nombre del nodo jerárquico (título de la lección).
        /// </summary>
        string INodoJerarquico.Nombre => TituloLeccion;

        /// <summary>
        /// Descripción del nodo jerárquico (descripción de la lección).
        /// </summary>
        string INodoJerarquico.Descripcion => DescripcionLeccion;

        /// <summary>
        /// Identificador del nodo padre (Id del SubTema).
        /// </summary>
        int? INodoJerarquico.IdPadre => SubtemaId;

        /// <summary>
        /// Tipo de nodo jerárquico (Leccion).
        /// </summary>
        TipoNodoJerarquico INodoJerarquico.Tipo => TipoNodoJerarquico.Leccion;
    }
}
