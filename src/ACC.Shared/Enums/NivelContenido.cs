using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Enums; 

public enum NivelContenido
{
    [Display(Name = "General")]
    General = 1,

    [Display(Name = "Principiante")]
    Principiante = 2,

    [Display(Name = "Intermedio")]
    Intermedio = 3,

    [Display(Name = "Avanzado")]
    Avanzado = 4
}
