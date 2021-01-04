using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Factura
    {
        [Key]
        [Required]
        public int facturaId { get; set; }

        [Required]
        public int idRecepcion { get; set; }
        public virtual Recepcion recepcion { get; set; }

        [Required]
        public string idCliente { get; set; }
        public virtual Cliente cliente { get; set; }

        [Required]
        public double impuesto { get; set; }

        [Required]
        public double total { get; set; }

        public double totalNeto { get; set; }

        [Required]
        public DateTime fechaEmision { get; set; }

    }
}
