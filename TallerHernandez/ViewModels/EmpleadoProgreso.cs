using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.ViewModels
{
    public class EmpleadoProgreso
    {
        public int empleadoprogresoID { get; set; }
        public Empleado empleado { get; set; }
        public ICollection<AsignacionTarea> asignacionTarea {get;set;}
        public int actTerminadas { get; set; }
        public int actSinTerminar { get; set; }
        public double porcentajeLogrado { get; set;}
    }
}
