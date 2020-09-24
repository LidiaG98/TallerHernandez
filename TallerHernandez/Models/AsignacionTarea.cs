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
        public Boolean estadoTarea { get; set; } = false; //Tarea finalizada o no finalizada

        [Required]
        [Display(Name = "Trabajo asignado")]
        public int recepcionID { get; set; }
        public Recepcion recepcion { get; set; }

        [Required]
        [ForeignKey("encargadoID")]
        [Display(Name = "Encargado")]
        public string empleadoID { get; set; }
        public Empleado empleado { get; set; }
    }
}
