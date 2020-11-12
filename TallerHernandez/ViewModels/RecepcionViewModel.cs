using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.ViewModels
{
    public class RecepcionViewModel
    {
        [Required]
        public int recepcionID { get; set; }

        [Required(ErrorMessage = "Debe de ingresar la descripción del diagnóstico")]
        [StringLength(300, ErrorMessage = "No puede escribir más de 300 caracteres")]
        [Display(Name = "Diagnóstico")]
        public string diagnostico { get; set; }

        [Required(ErrorMessage = "Debe de especificar la fecha de entrada del automóvil")]
        [Display(Name = "Ingreso")]
        public DateTime fechaEntrada { get; set; }

        [Required(ErrorMessage = "Debe de especificar la fecha aproximada de salida del automóvil")]
        [Display(Name = "Salida")]
        public DateTime fechaSalida { get; set; }

        [Required]
        [Display(Name = "Dueño")]
        public string clienteID { get; set; }       

        [Required]
        [Display(Name = "Recibió")]
        public string empleadoID { get; set; }

        [Required(ErrorMessage = "Seleccione un automóvil")]
        [Display(Name = "Automóvil")]
        public int automovilID { get; set; }
        [Display(Name = "Marca")]
        public string marca { get; set; }
        [Display(Name = "Año")]
        public int anio { get; set; }

        [Display(Name = "Procedimientos")]
        public List<Procedimiento> procedimientos { get; set; }

        [Display(Name = "Estado")]
        [Range(0, 1)]
        public int estado { get; set; } // 1 = No asignado, 0 = Asignado
    }
}
