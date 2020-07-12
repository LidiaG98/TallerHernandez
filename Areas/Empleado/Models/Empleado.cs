using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Areas.Empleado.Models
{
    public class Empleado
    {
        [Display(Name = "Número")]
        [Key]
        public int idEmpleado { get; set; }
        [Required(ErrorMessage = "Elija un modo de pago")]
        [Display(Name ="Modo de pago")]
        public int idModo { get; set; }
        [Required(ErrorMessage = "Elija un área de trabajo")]
        [Display(Name = "Área")]
        public int idArea { get; set; }

        [Required(ErrorMessage = "Ingrese el nombre del empleado")]
        [Display(Name = "Nombre")]
        public String nombre { get; set; }
        [Required(ErrorMessage = "Ingrese el apellido del empleado")]
        [Display(Name = "Apellido")]
        public String apellido { get; set; }
        [Required(ErrorMessage = "Ingrese el DUI del empleado")]
        [Display(Name = "DUI")]
        public String dui { get; set; }
        [Required(ErrorMessage = "Ingrese el salario del empleado")]
        [Display(Name = "Salario")]
        public float salario { get; set; }
        [Required(ErrorMessage = "Ingrese el teléfono del empleado")]
        [Display(Name = "Teléfono")]
        public String telefono { get; set; }
        [Required(ErrorMessage = "Ingrese el correo del empleado")]
        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public String correo { get; set; }


    }
}
