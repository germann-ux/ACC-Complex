using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Enums; 

public enum CategoriaTag
{
    [Display(Name = "Lenguaje")]
    Lenguaje = 1,

    [Display(Name = "Framework")]
    Framework = 2,

    [Display(Name = "Runtime")]
    Runtime = 3,

    [Display(Name = "Concepto")]
    Concepto = 4,

    [Display(Name = "Arquitectura")]
    Arquitectura = 5,

    [Display(Name = "Patrón")]
    Patron = 6,

    [Display(Name = "Herramienta")]
    Herramienta = 7,

    [Display(Name = "Performance")]
    Performance = 8,

    [Display(Name = "Seguridad")]
    Seguridad = 9,

    [Display(Name = "Datos")]
    Datos = 10
}
