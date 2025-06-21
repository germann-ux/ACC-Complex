using ACC.Shared.Enums;
using ACC.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.DTOs
{
    public class NodoJerarquicoDto : INodoJerarquico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int? IdPadre { get; set; }
        public TipoNodoJerarquico Tipo { get; set; }

        public NodoJerarquicoDto(int id, string nombre, int? idPadre, string descripcion, TipoNodoJerarquico tipo)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion; 
            IdPadre = idPadre;
            Tipo = tipo;
        }
    }
}
