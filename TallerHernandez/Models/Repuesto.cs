using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Repuesto
    {
        [Display(Name = "Codigo")]
        public int repuestoID { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string nombre { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string categoria { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int anio { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int cantidad { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string tipo { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string estadorespuesto { get; set; }
    }
}
