using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.ViewModels
{
    public class UsuarioTrabajadorViewModel
    {
        public IdentityUser user { get; set; }
        public Empleado empleado { get; set; }
    }
}
