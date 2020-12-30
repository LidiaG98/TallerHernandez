using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Extras
    {
        [Key]
        [Required]
        public int idExtra { get; set; }

        [Required]
        public int idAsignacionTarea { get; set; }
        public virtual AsignacionTarea asignacionTarea { get; set; }

        [Required]
        public int idFactura { get; set; }
        public virtual Factura factura { get; set; }

        [Required]
        public double total { get; set; }
    }
}
