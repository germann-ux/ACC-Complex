using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ACC.Data.Entities
{
    public class Aviso // clase creada para los docentes para que puedan publicar avisos en el muro de su aula
    {
        [Key]
        public int IdAviso { get; set; }

        [Required]
        [MaxLength(100)]
        public string TituloAviso { get; set; }

        [Required]
        public string ContenidoAviso { get; set; }

        [Required]
        public DateTime FechaAviso { get; set; } = DateTime.Now;

        [Required]
        public string DocenteId { get; set; }

        [Required]
        public int AulaId { get; set; }

        [JsonIgnore]
        [ForeignKey("AulaId")]
        public Aula? Aula { get; set; } // Relación 1:N con Aula
    }

    //AvisoRoot
    public class AvisoRoot
    {
        [JsonPropertyName("Values")]
        public List<Aviso> Values { get; set; } = new List<Aviso>();
    }
}
