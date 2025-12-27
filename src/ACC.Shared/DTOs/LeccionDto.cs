using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    //public class LeccionDto
    //{
    //    public int IdLeccion { get; set; }
    //    public string TituloLeccion { get; set; } = string.Empty;
    //    public string DescripcionLeccion { get; set; } = string.Empty;
    //    public string ContenidoLeccion { get; set; } = string.Empty;
    //    public int SubtemaId { get; set; }

    //    // Agregado para comportamiento dinámico
    //    public bool TieneActividad { get; set; }
    //    // Actividades externas de otros provedores, requerida por el componente modal de las lecciones 
    //    public string? UrlActividad { get; set; }
    //    // tiene una evaluacion?, si es asi entonces true
    //    public bool TieneEvaluacion { get; set; }
    //    // id de la evaluacion para ubicarla y usarla
    //    public int? IdEvaluacion { get; set; }
    //    // tiene modal?, entonces true
    //    public bool TieneModal { get; set; }
    //    // Para manejar si tiene compilador ACC
    //    public bool TieneCompilador { get; set; }
    //}

    /// <summary>
    /// LeccionDto ---> Version mejorada.
    /// </summary>
    public class LeccionDto
    {
        /// <summary>
        /// Id de la lección.
        /// </summary>
        public int IdLeccion { get; set; }

        ///<summary>
        /// Título de la lección.
        /// </summary>
        public string TituloLeccion { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la lección.
        /// </summary>
        public string DescripcionLeccion { get; set; } = string.Empty;

        /// <summary>
        /// Contenido HTML principal (modo antiguo, sigue siendo compatible).
        /// </summary>
        [Obsolete("Se remplazara por propiedades separadas, Teoria, practica y Ejemplo")]
        public string? HtmlBody { get; set; } = string.Empty;
        //  |
        //  |
        // \ /
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

        /// <summary>
        /// Agregado para manejar el tip Charp en las lecciones.
        /// </summary>
        public string? CharpTip { get; set; } = null;
        /// <summary>
        /// Añadido para manejar el diálogo Charp en las lecciones.
        /// </summary>
        public string? CharpDialog { get; set; } = null;

        /// <summary>
        /// El nivel de la taxonomia de Bloom.
        /// </summary>
        public string? NivelBloom { get; set; } = null;

        /// <summary>
        /// ¿Tiene actividad externa?
        /// </summary>
        public bool TieneActividad { get; set; }

        /// <summary>
        /// URL de la actividad externa.
        /// </summary>
        public string? UrlActividad { get; set; }

        /// <summary>
        /// ¿Tiene evaluación posterior?
        /// </summary>
        public bool TieneEvaluacion { get; set; }

        /// <summary>
        /// Id de evaluación, si aplica.
        /// </summary>
        public int? IdEvaluacion { get; set; }

        /// <summary>
        /// ¿Incluye el compilador ACC?
        /// </summary>
        public bool TieneCompilador { get; set; }

        /// <summary>
        /// Lista ordenada de secciones a renderizar (modo clásico).
        /// Ejemplo: ["html(Teoria, Practica y Teoria)", "actividad", "compilador"]
        /// </summary>
        public List<string> OrdenSecciones { get; set; } = [];
    }

}
