using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Mantenimiento
    {
        [Required]
        public int mantenimientoID { get; set; }

        [Required]
        [Display(Name = "Mantenimiento")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public float precio { get; set; }

        public ICollection<Recepcion> recepcion { get; set; }

        [Required]
        public int areaID { get; set; }
        public Area area { get; set; }
    }    
}
