using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class ModoPago
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name="Código")]
        public int modopagoID { get; set; }
        [Display(Name ="Tipo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string tipo { get; set; }
        public ICollection<Empleado> empleados { get; set; }
    }
}
