using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Controllers;


namespace TallerHernandez.Models
{
    public class Cliente
    {
        [Display(Name ="DUI")]
        [RegularExpression(@"^[0-9]{8}-[0-9]{1}$", ErrorMessage = "El formato de DUI no es correcto, ¿Está colocando el guión?")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
       
        public string clienteID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="Este campo es obligatorio")]
        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙÑñ.-]+", ErrorMessage ="Ingrese un nombre valido")]
        public string nombre { get; set; }
        [Display(Name = "Apellido")]
        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙÑñ.-]+", ErrorMessage = "Ingrese un apellido valido")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string apellido { get; set; }
        [Display(Name = "Correo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
     
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Ingrese un correo válido")]

        public string correo { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Ingrese un número de teléfono válido")]
        [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "Su número debe contener exactamente 8 digitos")]
        public string telefono { get; set; }
        
        //[Display(Name = "imagen")]
        //public string imagen { get; set; }
        [Display(Name = "Puntos")]
        
        [NotMapped]

        public  Imagen imagen { get; set; }
        public string imagenN { get; set; }
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Ingrese una cantidad correcta")]
        public int puntos { get; set; }



        public ICollection<Automovil> automovils { get; set; }

        public ICollection<Recepcion> recepcion { get; set; }


    }
}
