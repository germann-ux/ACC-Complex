using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Enums; 

public enum DificultadContenido
{
    [Display(Name = "Fácil")]
    Facil = 1,

    [Display(Name = "Media")]
    Media = 2,

    [Display(Name = "Difícil")]
    Dificil = 3
}
