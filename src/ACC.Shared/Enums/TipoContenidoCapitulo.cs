using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACC.Shared.Enums;

public enum TipoContenidoCapitulo
{
    [Display(Name = "Documentación")]
    Documentacion = 1,

    [Display(Name = "Concepto")]
    Concepto = 2,

    [Display(Name = "Modelo mental")]
    ModeloMental = 3,

    [Display(Name = "Sintaxis")]
    Sintaxis = 4,

    [Display(Name = "Ejemplo")]
    Ejemplo = 5,

    [Display(Name = "Ejercicio")]
    Ejercicio = 6,

    [Display(Name = "Checklist")]
    Checklist = 7,

    [Display(Name = "Errores comunes")]
    ErroresComunes = 8,

    [Display(Name = "Buenas prácticas")]
    BuenasPracticas = 9,

    [Display(Name = "Performance")]
    Performance = 10,

    [Display(Name = "Seguridad")]
    Seguridad = 11,

    [Display(Name = "Referencias")]
    Referencias = 12,

    [Display(Name = "FAQ")]
    FAQ = 13,

    [Display(Name = "Glosario")]
    Glosario = 14,

    [Display(Name = "Proyecto")]
    Proyecto = 15
}