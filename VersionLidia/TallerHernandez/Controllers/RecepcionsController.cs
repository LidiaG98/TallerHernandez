using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.ModelModal;
using TallerHernandez.Models;

namespace TallerHernandez.Controllers
{
    public class RecepcionsController : Controller
    {
        private readonly TallerHernandezContext _context;
        private MantenimientoModels mantenimientoModels;
        private ProcedimientoModels procedimientoModels;

        public RecepcionsController(TallerHernandezContext context)
        {
            _context = context;
            mantenimientoModels = new MantenimientoModels(context);
            procedimientoModels = new ProcedimientoModels(context);
        }

        // GET: Recepcions
        public async Task<IActionResult> Index(string Buscar)
        {
            ViewData["Filtro"] = Buscar;
            //var tallerHernandezContext = _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.empleado).
            //    Include(r => r.mantenimiento).Include(r =>r.procedimiento);
            var recepciones = from s in _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.empleado).Include(r => r.mantenimiento).Include(r =>r.procedimiento) select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                recepciones = recepciones.Where(s => s.automovilID.Contains(Buscar) || s.cliente.clienteID.Contains(Buscar) || s.cliente.nombre.Contains(Buscar));
            }
            return View(await recepciones.AsNoTracking().ToListAsync());
        }

        // GET: Recepcions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcion = await _context.Recepcion
                .Include(r => r.Automovil)
                .Include(r => r.cliente)
                .Include(r => r.empleado)
                .Include(r => r.mantenimiento)
                .Include(r => r.procedimiento)
                .FirstOrDefaultAsync(m => m.recepcionID == id);
            if (recepcion == null)
            {
                return NotFound();
            }

            return View(recepcion);
        }

        [HttpPost]
        public List<IdentityError> agregarMantenimiento(string nombre, string precio, string areaID)
        {
            return mantenimientoModels.agregarMantenimiento(nombre, precio, areaID);
        }

        [HttpPost]
        public List<IdentityError> agregarProcedimiento(string procedimiento, string precio, string areaID)
        {
            return procedimientoModels.agregarProcedimiento(procedimiento, precio, areaID);
        }

        // GET: Recepcions/Create
        public IActionResult Create()
        {
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "automovilID");
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "nombre");
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "nombre");
            ViewData["ListaP"] = new SelectList(_context.Procedimiento, "procedimientoID", "procedimiento");
            ViewData["ListaM"] = new SelectList(_context.Mantenimiento, "mantenimientoID", "nombre");
            List<Area> areas = new List<Area>();
            areas= _context.Area.FromSqlRaw("SELECT * FROM Area").ToList();
            List<SelectListItem> a = areas.ConvertAll(ac =>
            {
                return new SelectListItem()
                {
                    Text = ac.areaNom,
                    Value = ac.AreaID.ToString(),
                    Selected = false
                };
            });
            ViewBag.areas = a;
            Recepcion r = new Recepcion();
            string fa = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            r.fechaEntrada = DateTime.ParseExact(fa, "dd/MM/yyyy HH:mm", null);
            r.mantenimientoID = null;
            
            return View(r);
        }

        // POST: Recepcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("diagnostico,fechaEntrada,fechaSalida,clienteID,empleadoID,automovilID," +
            "procedimientoID,mantenimientoID,estado")] Recepcion recepcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recepcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "automovilID", recepcion.automovilID);
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "nombre", recepcion.clienteID);
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "nombre", recepcion.empleadoID);
            
            var l = _context.Mantenimiento.ToList();
            var lp = _context.Procedimiento.ToList();
            var last = l.Last();            
            recepcion.mantenimientoID = last.mantenimientoID;
            recepcion.procedimientoID = lp.Last().procedimientoID;

            ViewData["ListaP"] = new SelectList(_context.Procedimiento, "procedimientoID", "procedimiento", recepcion.procedimientoID);
            ViewData["ListaM"] = new SelectList(_context.Mantenimiento, "mantenimientoID", "nombre",recepcion.mantenimientoID);
            List<Area> areas = new List<Area>();
            areas = _context.Area.FromSqlRaw("SELECT * FROM Area").ToList();
            List<SelectListItem> a = areas.ConvertAll(ac =>
            {
                return new SelectListItem()
                {
                    Text = ac.areaNom,
                    Value = ac.AreaID.ToString(),
                    Selected = false
                };
            });
            ViewBag.areas = a;

            return View(recepcion);
        }

        // GET: Recepcions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcion = await _context.Recepcion.FindAsync(id);
            if (recepcion == null)
            {
                return NotFound();
            }
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "automovilID", recepcion.automovilID);
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "clienteID", recepcion.clienteID);
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "empleadoID", recepcion.empleadoID);
            ViewData["procedimientoID"] = new SelectList(_context.Procedimiento, "procedimientoID", "procedimientoID");
            ViewData["mantenimientoID"] = new SelectList(_context.Mantenimiento, "mantenimientoID", "mantenimientoID");
            return View(recepcion);
        }

        // POST: Recepcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("recepcionID,diagnostico,fechaEntrada,fechaSalida,clienteID,empleadoID,automovilID," +
            "procedimientoID,mantenimientoID,estado")] Recepcion recepcion)
        {
            if (id != recepcion.recepcionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recepcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecepcionExists(recepcion.recepcionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "automovilID", recepcion.automovilID);
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "clienteID", recepcion.clienteID);
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "empleadoID", recepcion.empleadoID);
            ViewData["procedimientoID"] = new SelectList(_context.Procedimiento, "procedimientoID", "procedimeintoID");
            ViewData["mantenimientoID"] = new SelectList(_context.Mantenimiento, "mantenimientoID", "mantenimientoID");            
            return View(recepcion);
        }

        // GET: Recepcions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcion = await _context.Recepcion
                .Include(r => r.Automovil)
                .Include(r => r.cliente)
                .Include(r => r.empleado)
                .Include(r => r.mantenimiento)
                .Include(r => r.procedimiento)
                .FirstOrDefaultAsync(m => m.recepcionID == id);
            if (recepcion == null)
            {
                return NotFound();
            }

            return View(recepcion);
        }

        // POST: Recepcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recepcion = await _context.Recepcion.FindAsync(id);
            _context.Recepcion.Remove(recepcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecepcionExists(int id)
        {
            return _context.Recepcion.Any(e => e.recepcionID == id);
        }

        public async Task<IActionResult> AgregarVehiculo(string autoId)
        {
            if (autoId == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil
                .Include(r => r.cliente)                
                .FirstOrDefaultAsync(m => m.automovilID == autoId);
            if (automovil == null)
            {
                return NotFound();
            }

            return View("Create",automovil);
        }
    }
}
