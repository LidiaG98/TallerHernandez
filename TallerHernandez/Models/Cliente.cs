using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Cliente
    {
        [Display(Name ="DUI")]
        public string clienteID { get; set; }
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
        [Display(Name = "Puntos")]
        public int puntos { get; set; }
       

        public ICollection<Automovil> automovils { get; set; }


    }
}
