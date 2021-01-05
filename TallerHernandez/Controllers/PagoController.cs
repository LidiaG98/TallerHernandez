using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.Models;
using TallerHernandez.ViewModels;

namespace TallerHernandez.Controllers
{
    public class PagoController : Controller
    {
        private readonly TallerHernandezContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public PagoController(TallerHernandezContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string Buscar)
        {
            int finalizada = 0;

            ViewData["Filtro"] = Buscar;
            //var tallerHernandezContext = _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.empleado).
            //    Include(r => r.mantenimiento).Include(r =>r.procedimiento);
            var recepciones = from s in _context.Recepcion
                              .Include(r => r.Automovil)
                              .Include(r => r.cliente)
                              .Include(r => r.empleado)
                              .Include(r => r.procedimientos)
                              .Where(l => l.estado == finalizada) select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                recepciones = recepciones
                    .Where(s => s.Automovil.placa.Contains(Buscar) || s.cliente.clienteID.Contains(Buscar) || s.cliente.nombre.Contains(Buscar))
                    .Where(l => l.estado == finalizada);
            }
            return View(await recepciones.AsNoTracking().ToListAsync());            
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int idRecepcion)
        {
            var recepcion = await _context.Recepcion
                .Include(r => r.Automovil)
                .Include(r => r.cliente)
                .Include(r => r.empleado)                
                .FirstOrDefaultAsync();
            var procedimientos = _context.Procedimiento
                .Where(l => l.recepcionID == idRecepcion)
                .ToList();
            var allTareas = _context.AsignacionTarea
                .Include(l => l.procedimiento)
                .ToList();
            List<AsignacionTarea> tareasRecepcion = new List<AsignacionTarea>();

            foreach (Procedimiento p in procedimientos)
            {
                var tarea = _context.AsignacionTarea
                    .Where(l => l.procedimientoID == p.procedimientoID)
                    .FirstOrDefault();
                if(tarea != null)
                {
                    tareasRecepcion.Add(tarea);
                }                
            }

            CheckoutViewModel model = new CheckoutViewModel()
            {
                recepcion = recepcion,
                tareasRecepcion = tareasRecepcion,
                procedimientos = procedimientos,
                preciosExtras = new double[tareasRecepcion.Count]
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Factura(CheckoutViewModel model)
        {            
                Factura factura = new Factura()
                {
                    idRecepcion = model.recepcion.recepcionID,
                    idCliente = model.recepcion.clienteID,
                    impuesto = model.impuesto,
                    total = model.total,                    
                    fechaEmision = model.fechaemision
                };
                factura.totalNeto = factura.total + (factura.total * (factura.impuesto / 100));
            // Aqui se agregan puntos al cliente que pagó 
            Cliente cliente = _context.Cliente.Where(s => s.clienteID == model.recepcion.clienteID).FirstOrDefault();
            cliente.puntos = cliente.puntos + 10;
            _context.Update(cliente);
            //aqui termnina
            _context.Add(factura);
                _context.SaveChanges();

                var f = _context.Factura
                    .Where(l => l.idRecepcion == model.recepcion.recepcionID)
                    .FirstOrDefault();                

                int i = 0;
                foreach (var tarea in model.tareasRecepcion)
                {
                    Extras e = new Extras()
                    {
                        idAsignacionTarea = tarea.asignacionTareaID,
                        idFactura = f.facturaId,
                        total = model.preciosExtras[i]
                    };
                    _context.Add(e);
                    i++;
                }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
