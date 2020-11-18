using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class AsignacionTarea
    {
        [Required]
        public int asignacionTareaID { get; set; }
        [Required]
        public Boolean estadoTarea { get; set; } = false; //Tarea finalizada o no finalizada; false = "No finalizada"
        [Display(Name = "Especificación")]
        [MaxLength(2000)]
        public string descripcion { get; set; }

        [Required]
        [Display(Name = "Procedimiento")]
        public int procedimientoID { get; set; }
        public Procedimiento procedimiento { get; set; }

        [Required]
        [ForeignKey("encargadoID")]
        [Display(Name = "Encargado")]
        public int empleadoID { get; set; }
        public Empleado empleado { get; set; }
    }
}
