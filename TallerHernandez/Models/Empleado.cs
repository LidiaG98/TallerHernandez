using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Empleado
    {
        [Display(Name = "DUI")]

        public string empleadoID { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        [Required]
        public string apellido { get; set; }
        [Display(Name = "Correo")]
        [Required]
        public string correo { get; set; }
        [Display(Name = "Teléfono")]
        [Required]
        public string telefono { get; set; }

        [Display(Name = "imagen")]
        public string imagen { get; set; }
        [Display(Name = "Salario")]
        [Required]
        public int salario { get; set; }
        [Display(Name = "Area")]
        [Required]
        public int areaID { get; set; }
        public Area area { get; set; }
        [Display(Name = "Puesto")]
        [Required]
        public int rolID { get; set; }
        public Rol rol { get; set; }
        [Required]
        [Display(Name = "Pago")]
        public int modopagoID { get; set; }
        public ModoPago modoPago { get; set; }
    }
}
