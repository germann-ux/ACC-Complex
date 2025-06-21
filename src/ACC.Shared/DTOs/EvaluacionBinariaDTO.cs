using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class EvaluacionBinariaDTO
    {
        public string Tipo { get; set; } = "EvaluacionBinaria";
        public List<PreguntaBinariaDTO> preguntas { get; set; } = new List<PreguntaBinariaDTO>();
    }

    public class PreguntaBinariaDTO
    {
        public int Id { get; set; }
        public string Enunciado { get; set; } = string.Empty;
        public string RespuestaCorrecta { get; set; } = "Sí"; // o "No"
        public string Explicacion { get; set; } = string.Empty;
    }
}
