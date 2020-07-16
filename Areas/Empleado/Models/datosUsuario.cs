using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Areas.Empleado.Models
{
    public class datosUsuario
    {
        [DisplayName("Nombre de usuario")]
        public String correo { set; get; }
        [DisplayName("Contraseña")]
        public String contra { set; get; }
    }
}
