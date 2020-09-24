using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Automovil
    {
        [Display(Name="Placa")]
        public string automovilID { get; set; }
        [Display(Name ="Marca")]
        [Required]
        public string marca { get; set; }
        [Display(Name = "Año")]
        [Required]
        public int anio { get; set; }
        [Display(Name = "Proceso")]
        [Required]
        public string proceso { get; set; }
        [Display(Name = "Estado")]
        [Required]
        public bool estado { get; set; }
        [Display(Name = "Comentario")]
        public string comentario { get; set; }
        [Required]
        [Display(Name = "Dueño")]
        public string clienteID { get; set; }

        public Cliente cliente { get; set; }

        public ICollection<Recepcion> recepcion { get; set; }


    }
}
