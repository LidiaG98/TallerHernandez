using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class ModoPago
    {
        [Display(Name="Código")]
        public int modopagoID { get; set; }
        [Display(Name ="Tipo")]
        [Required]
        public string tipo { get; set; }
        public ICollection<Empleado> empleados { get; set; }
    }
}
