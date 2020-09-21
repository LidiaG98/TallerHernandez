using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.Controllers
{
    public class AsignacionTareasController : Controller
    {
        private readonly TallerHernandezContext _context;

        public AsignacionTareasController(TallerHernandezContext context)
        {
            _context = context;
        }

        // GET: AsignacionTareas
        public async Task<IActionResult> Index(string cadena)
        {
            ViewData["Filtro"] = cadena;
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.recepcion) select s;
            asignacionTareas = asignacionTareas.Where(s => s.estadoTarea == false);

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.recepcion.automovilID.Contains(cadena) || s.empleado.nombre.Contains(cadena));
            }
       
            return View(await asignacionTareas.ToListAsync());
        }

        // GET: AsignacionTareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionTarea = await _context.AsignacionTarea
                .Include(a => a.empleado)
                .Include(a => a.recepcion)
                .FirstOrDefaultAsync(m => m.asignacionTareaID == id);
            if (asignacionTarea == null)
            {
                return NotFound();
            }

            return View(asignacionTarea);
        }

        // GET: AsignacionTareas/Create
        public IActionResult Create()
        {
            var empleado = from s in _context.Empleado.Include(e => e.area).Include(e => e.modoPago).Include(e => e.rol) select s;
            empleado = empleado.Where(s => s.rol.rolNom.Equals("Mecánico"));
 
            ViewData["empleadoID"] = new SelectList(empleado, "empleadoID", "nombre");
            ViewData["recepcionID"] = new SelectList(_context.Recepcion, "recepcionID", "automovilID");
            return View();
        }

        // POST: AsignacionTareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("asignacionTareaID,estadoTarea,recepcionID,empleadoID")] AsignacionTarea asignacionTarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignacionTarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "empleadoID", asignacionTarea.empleadoID);
            ViewData["recepcionID"] = new SelectList(_context.Recepcion, "recepcionID", "automovilID", asignacionTarea.recepcionID);
            return View(asignacionTarea);
        }

        // GET: AsignacionTareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionTarea = await _context.AsignacionTarea.FindAsync(id);
            if (asignacionTarea == null)
            {
                return NotFound();
            }
            var empleado = from s in _context.Empleado.Include(e => e.area).Include(e => e.modoPago).Include(e => e.rol) select s;
            empleado = empleado.Where(s => s.rol.rolNom.Equals("Mecánico"));

            ViewData["empleadoID"] = new SelectList(empleado, "empleadoID", "empleadoID", asignacionTarea.empleadoID);
            ViewData["recepcionID"] = new SelectList(_context.Recepcion, "recepcionID", "automovilID", asignacionTarea.recepcionID);
            return View(asignacionTarea);
        }

        // POST: AsignacionTareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("asignacionTareaID,estadoTarea,recepcionID,empleadoID")] AsignacionTarea asignacionTarea)
        {
            if (id != asignacionTarea.asignacionTareaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacionTarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionTareaExists(asignacionTarea.asignacionTareaID))
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
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "empleadoID", asignacionTarea.empleadoID);
            ViewData["recepcionID"] = new SelectList(_context.Recepcion, "recepcionID", "automovilID", asignacionTarea.recepcionID);
            return View(asignacionTarea);
        }

        // GET: AsignacionTareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionTarea = await _context.AsignacionTarea
                .Include(a => a.empleado)
                .Include(a => a.recepcion)
                .FirstOrDefaultAsync(m => m.asignacionTareaID == id);
            if (asignacionTarea == null)
            {
                return NotFound();
            }

            return View(asignacionTarea);
        }

        // POST: AsignacionTareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacionTarea = await _context.AsignacionTarea.FindAsync(id);
            _context.AsignacionTarea.Remove(asignacionTarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TareasFinalizadas()
        {
            var tareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.recepcion) select s;
            tareas = tareas.Where(s => (s.estadoTarea==true));
            
            return View(await tareas.ToListAsync());
        }

        private bool AsignacionTareaExists(int id)
        {
            return _context.AsignacionTarea.Any(e => e.asignacionTareaID == id);
        }

        public async Task<List<Recepcion>> ListadoAsync(string cadena)
        {
            var tallerHernandezContext = _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.empleado).
                Include(r => r.mantenimiento).Include(r => r.procedimiento);

            var recepciones = from s in _context.Recepcion select s;

            ViewData["CurrentFilter"] = cadena;
            if (!String.IsNullOrEmpty(cadena))
            {
                recepciones = recepciones.Where(s => s.automovilID.Contains(cadena));
            }

            List<Recepcion> lists = await tallerHernandezContext.ToListAsync();
            return (List<Recepcion>)(IActionResult)lists;
        }
    }
}
