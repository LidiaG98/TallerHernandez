using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Controllers;

namespace TallerHernandez.Models
{
    public class Automovil
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name="Placa")]
        public string automovilID { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name ="Marca")]
        
        public string marca { get; set; }
        [Display(Name = "Año")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int anio { get; set; }
        [NotMapped]
        public Imagen imagen { get; set; }
        [Display(Name = "Proceso")]
        [Required]
        public string proceso { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public bool estado { get; set; }
        [Display(Name = "Comentario")]
        public string comentario { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Dueño")]
        public string clienteID { get; set; }

        public Cliente cliente { get; set; }
       
        public string imagenN { get; set; }

        public ICollection<Recepcion> recepcion { get; set; }


    }
}
