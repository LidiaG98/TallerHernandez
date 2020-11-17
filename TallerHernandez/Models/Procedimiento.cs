using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Procedimiento
    {
        [Required]
        public int procedimientoID { get; set; }

        [Required(ErrorMessage = "Debe de ingresar la descripción del procedimiento")]
        [Display(Name ="Procedimiento")]
        public string procedimiento { get; set; }

        [Required(ErrorMessage = "Debe de ingresar el precio del procedimiento")]
        [Display(Name ="Precio")]
        public float precio { get; set; }

        [Required]
        public int recepcionID { get; set; }
        public Recepcion recepcion { get; set; }

        [Required(ErrorMessage = "Debe de ingresar el área del procedimiento")]
        public int areaID { get; set; }
        public Area area { get; set; }

        [Range(0, 1)]
        public int estado { get; set; } = 1; // 1 = No Asignado, 0 = Asignado

        public ICollection<AsignacionTarea> asignacionTarea { get; set; }
    }
}
