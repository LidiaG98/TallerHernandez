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
    public class AutomovilsController : Controller
    {
        private readonly TallerHernandezContext _context;

        public AutomovilsController(TallerHernandezContext context)
        {
            _context = context;
        }

        // GET: Automovils
        public async Task<IActionResult> Index(string OrdenA,string Buscar)
        {
            var auto = from s in _context.Automovil.Include(a => a.cliente) select s;

            ViewData["Ordenmarca"] = String.IsNullOrEmpty(OrdenA) ? "marca_desc" : "";
            ViewData["Ordenanio"] = OrdenA == "an_asc" ? "an_desc" : "an_asc";
            ViewData["Filtro"] = Buscar;
         
            if (!String.IsNullOrEmpty(Buscar))
            {
                auto = auto.Where(s => s.automovilID.Contains(Buscar) || s.marca.Contains(Buscar) || s.anio.Equals(Buscar) || s.clienteID.Contains(Buscar));
            }
            switch (OrdenA)
            {
                case "marca_desc":
                   auto  = auto.OrderByDescending(s => s.marca);
                    break;
                case "an_asc":
                    auto = auto.OrderBy(s => s.anio);
                    break;
                case "ap_desc":
                    auto = auto.OrderByDescending(s => s.anio);
                    break;
                default:
                    auto = auto.OrderBy(s => s.marca);
                    break;
            }

            return View(await auto.AsNoTracking().ToListAsync());

            //return View(await tallerHernandezContext.ToListAsync());
        }

        // GET: Automovils/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil
                .Include(a => a.cliente)
                .FirstOrDefaultAsync(m => m.automovilID == id);
            if (automovil == null)
            {
                return NotFound();
            }

            return View(automovil);
        }

        // GET: Automovils/Create
        public IActionResult Create()
        {
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "clienteID");
            return View();
        }

        // POST: Automovils/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("automovilID,marca,anio,proceso,estado,comentario,clienteID")] Automovil automovil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(automovil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "clienteID", automovil.clienteID);
            return View(automovil);
        }

        // GET: Automovils/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil.FindAsync(id);
            if (automovil == null)
            {
                return NotFound();
            }
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "clienteID", automovil.clienteID);
            return View(automovil);
        }

        // POST: Automovils/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("automovilID,marca,anio,proceso,estado,comentario,clienteID")] Automovil automovil)
        {
            if (id != automovil.automovilID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(automovil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomovilExists(automovil.automovilID))
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
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "clienteID", automovil.clienteID);
            return View(automovil);
        }

        // GET: Automovils/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil
                .Include(a => a.cliente)
                .FirstOrDefaultAsync(m => m.automovilID == id);
            if (automovil == null)
            {
                return NotFound();
            }

            return View(automovil);
        }

        // POST: Automovils/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var automovil = await _context.Automovil.FindAsync(id);
            _context.Automovil.Remove(automovil);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutomovilExists(string id)
        {
            return _context.Automovil.Any(e => e.automovilID == id);
        }
    }
}
