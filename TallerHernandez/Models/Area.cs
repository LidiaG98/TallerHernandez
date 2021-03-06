﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Models
{
    public class Area
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [Display(Name = "Código")]
        public int AreaID { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string areaNom { get; set; }
        public ICollection<Empleado> empleados { get; set; }

        public ICollection<Procedimiento> procedimiento { get; set; }        

    }
}
