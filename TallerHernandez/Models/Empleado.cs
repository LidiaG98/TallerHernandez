using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Controllers;

namespace TallerHernandez.Models
{
    public class Empleado
    {
        [RegularExpression(@"^[0-9]{8}-[0-9]{1}$", ErrorMessage = "El formato de DUI no es correcto, ¿Está colocando el guión?")]
        [Display(Name = "DUI")]
        [Key]
        public string empleadoID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\']+[\s])+([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])+[\s]?([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])?$", ErrorMessage = "Ingrese un nombre valido")]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        [RegularExpression(@"^([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\']+[\s])+([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])+[\s]?([A-Za-zÁÉÍÓÚñáéíóúÑ]{0}?[A-Za-zÁÉÍÓÚñáéíóúÑ\'])?$", ErrorMessage = "Ingrese un apellido valido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string apellido { get; set; }
        [Display(Name = "Correo")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Ingrese un correo válido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Ingrese un número de teléfono válido")]
       
        public string correo { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Ingrese un número de teléfono válido")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Ingrese un número de teléfono válido")]
        public string telefono { get; set; }
        [NotMapped]
        public Imagen imagen { get; set; }
        // [Display(Name = "imagen")]
        //public string imagen { get; set; }
        [Display(Name = "Salario")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Ingrese un número de teléfono válido")]
        public int salario { get; set; }
       
        
        [Display(Name = "Area")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int areaID { get; set; }
        public Area area { get; set; }
        [Display(Name = "Puesto")]
        [Required(ErrorMessage = "Este campo es obligatorio")]

        public int rolID { get; set; }
        public Rol rol { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Pago")]
        public int modopagoID { get; set; }
        public ModoPago modoPago { get; set; }
       
        public string imagenN { get; set; }

        public ICollection<Recepcion> recepcion { get; set; }
        public ICollection<AsignacionTarea> asignacionTarea { get; set; }
    }
}
