using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Area
    {
        [Display(Name = "Código")]
        public int AreaID { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string areaNom { get; set; }
        public ICollection<Empleado> empleados { get; set; }

        public ICollection<Procedimiento> procedimiento { get; set; }
        public ICollection<Mantenimiento> mantenimiento { get; set; }

    }
}
