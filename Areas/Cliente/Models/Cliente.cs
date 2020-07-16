using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Cliente.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        [Required]
        public String dui { get; set; }
        [Required]
        public String nombre { get; set; }
        [Required]
        public String apellido { get; set; }
        [Required]
        public string telefono { get; set; }
        [Required]
        public string correo { get; set; }      
        public int puntaje { get; set; }
        public Imagen image { get; set; }

    }
}
