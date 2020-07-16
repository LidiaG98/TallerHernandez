using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Empleado.Models
{
    public class Empleado
    {
        [Display(Name = "#")]
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
        [RegularExpression(@"^[0-9]{8}-[0-9]{1}$",ErrorMessage ="El formato de DUI no es correcto, ¿Está colocando el guión?")]
        public String dui { get; set; }
        [Required(ErrorMessage = "Ingrese el salario del empleado")]
        [Display(Name = "Salario")]
        [RegularExpression(@"[0-9]*[\.]?[0-9]{0,2}$", ErrorMessage = "El salario debe ser un número")]
        public float salario { get; set; }

        [Required(ErrorMessage = "Ingrese el teléfono del empleado")]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Ingrese un número de teléfono válido")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Ingrese un número de teléfono válido")]
        public String telefono { get; set; }
        [Required(ErrorMessage = "Ingrese el correo del empleado")]
        [Display(Name = "Correo electrónico")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
        public String correo { get; set; }

        public String idUsuario { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Elija un rol para el empleado")]
        public String idRol { get; set; }
        public Imagen imagen { get; set; }


    }
}
