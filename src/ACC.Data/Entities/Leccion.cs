using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        /// Contenido principal de la lección el cual es renderizado (puede incluir HTML, markdown, etc.).
        /// </summary>
        [Obsolete("Campo obsoleto, se fragmento en las nuevas propiedades 'Teoria', 'Practica', 'Ejemplo'")]
        public string HtmlBody { get; set; } = string.Empty;
        
        /// <summary>
        /// Indica si la lección tiene una actividad asociada.
        /// </summary>
        [Required]
        public bool TieneActividad { get; set; } = false;

        /// <summary>
        /// URL de la actividad asociada, si existe.
        /// </summary>
        [AllowNull]
        public string? UrlActividad { get; set; } = null;

        /// <summary>
        /// Indica si la lección tiene una evaluación asociada.
        /// </summary>
        [AllowedValues("true", "false")]
        public bool TieneEvaluacion { get; set; } = false;

        /// <summary>
        /// Identificador de la evaluación asociada, si existe.
        /// </summary>
        //public int? IdEvaluacion { get; set; } = null;

        /// <summary>
        /// Indica si la lección incluye un compilador interactivo.
        /// </summary>
        public bool TieneCompilador { get; set; } = false;

        /// <summary>
        /// Orden de las secciones de la lección (por ejemplo: ["introducción", "teoría", "ejercicios"]).
        /// </summary>
        [Required]
        public List<string> OrdenSecciones { get; set; } = [];

        /// <summary>
        /// Identificador del SubTema al que pertenece la lección (clave foránea).
        /// </summary>
        [Required]
        public int SubtemaId { get; set; }

        /// <summary>
        /// Referencia de navegación al SubTema padre.
        /// </summary>
        [ForeignKey("SubtemaId")]
        public SubTema? SubTema { get; set; }

        /// <summary>
        /// Colección de capítulos que pueden estar asociados a esta lección (relación inversa).
        /// </summary>
        public ICollection<Capitulo>? Capitulos { get; set; }

        /// <summary>
        /// propiedad para manejar el cuerpo de las evaluaciones y renderizarlas luego
        /// </summary>
        public string? CuerpoEvaluacion { get; set; } = null;

        /// <summary>
        /// Seccion teorica de las lecciones, en formato html
        /// </summary>
        public string Teoria { get; set; } = string.Empty;
        /// <summary>
        /// Seccion conectora a la practica, no nesesariamente una practica redactada u tarea por definicion
        /// </summary>
        public string Practica { get; set; } = string.Empty;
        /// <summary>
        /// Seccion de ejemplos en la leccion, simples y sencillos o mas extensos si es nesesario.
        /// </summary>
        public string Ejemplo { get; set; } = string.Empty;


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
