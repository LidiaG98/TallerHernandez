using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.ViewModels
{
    public class CheckoutViewModel
    {
        [BindProperty]
        public Recepcion recepcion { get; set; }
        [BindProperty]
        public List<Procedimiento> procedimientos { get; set; }
        [BindProperty]
        public List<AsignacionTarea> tareasRecepcion { get; set; }
        [BindProperty]
        public double[] preciosExtras { get; set; }
        [BindProperty]
        public double total { get; set; }
        [BindProperty]
        public double impuesto { get; set; }
        [BindProperty]
        public DateTime fechaemision { get; set; }
    }
}
