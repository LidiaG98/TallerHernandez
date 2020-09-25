using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Rol
    {
        [Display(Name = "Código")]
        public int rolID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        public string rolNom { get; set; }
        public ICollection<Empleado> empleados { get; set; }
    }
}
