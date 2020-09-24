using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Recepcion
    {
        [Required]
        public int recepcionID { get; set; }

        [Required(ErrorMessage = "Debe de ingresar la descripción del diagnóstico")]
        [StringLength(300, ErrorMessage = "No puede escribir más de 300 caracteres")]
        [Display(Name ="Diagnóstico")]
        public string diagnostico { get; set; }

        [Required(ErrorMessage = "Debe de especificar la fecha de entrada del automóvil")]
        [Display(Name = "Ingreso")]
        //[DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.DateTime)]
        public DateTime fechaEntrada { get; set; }

        [Required(ErrorMessage = "Debe de especificar la fecha aproximada de salida del automóvil")]
        [Display(Name = "Salida")]
        public DateTime fechaSalida { get; set; }

        [Required]
        [Display(Name = "Dueño")]
        public string clienteID { get; set; }
        public Cliente cliente { get; set; }

        [Required]
        [Display(Name = "Recibió")]
        public string empleadoID { get; set; }
        public Empleado empleado { get; set; }

        [Required(ErrorMessage ="Seleccione un automóvil")]
        [Display(Name = "Automóvil")]
        public string automovilID { get; set; }
        public Automovil Automovil { get; set; }

        [Display(Name = "Mantenimiento")]
        public int? mantenimientoID { get; set; }
        public Mantenimiento mantenimiento { get; set; }

        [Display(Name = "Procedimiento")]
        public int? procedimientoID { get; set; }
        public Procedimiento procedimiento { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [Range(0,1)]
        public int estado { get; set; } // 1 = en proceso, 0 = finalizado

        public ICollection<AsignacionTarea> asignacionTarea { get; set; }

    }
}
