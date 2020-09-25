using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.ViewModels
{
    public class EditUsuarioViewModel
    {
        public EditUsuarioViewModel()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "El Nombre de usuario obligatorio")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
     
        [EmailAddress]
        public string EmailAntiguo { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //public string ConfirmPassword { get; set; }


        public IList<string> Roles { get; set; }
    }
}
