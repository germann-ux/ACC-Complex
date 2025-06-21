using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Data.Entities
{
    /// <summary>
    /// quizas es inutil
    /// entidad para representar una evaluación binaria, que consiste en preguntas con respuestas de tipo "Sí" o "No".
    /// </summary>
    public class EvaluacionBinaria
    {
        public string Tipo { get; set; } = "EvaluacionBinaria";
        public List<PreguntaBinaria> preguntas { get; set; } = new List<PreguntaBinaria>();
    }

    public class PreguntaBinaria
    {
        public int Id { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public string RespuestaCorrecta { get; set; } = "Sí"; // o "No"
        public string Explicacion { get; set; } = string.Empty;
    }
}
