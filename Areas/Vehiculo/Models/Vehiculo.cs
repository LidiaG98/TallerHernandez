using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Vehiculo.Models
{
    public class Vehiculo
    {
        [Required]
        public String placa { get; set; }
        [Required]
        public int idcliente { get; set; }
        [Required]
        public String marca { get; set; }
        [Required]
        public String modelo { get; set; }
        [Required]
        public String ano { get; set; }
        [Required]
        public String estado { get; set; }
        [Required]
        public String procedimiento { get; set; }
        [Required]
        public String comentario { get; set; }

        public Imagen image { get; set; }

    }
}
