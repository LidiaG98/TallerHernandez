using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.Controllers
{
    [Authorize]
    public class RepuestoesController : Controller
    {
        private readonly TallerHernandezContext _context;

        public RepuestoesController(TallerHernandezContext context)
        {
            _context = context;
        }

        // GET: Repuestoes
        public async Task<IActionResult> Index(string OrdenA, string Buscar)
        {
            ViewData["OrdenNom"] = String.IsNullOrEmpty(OrdenA) ? "nom_desc" : "";
            ViewData["OrdenAp"] = OrdenA == "ap_asc" ? "ap_desc" : "ap_asc";
            ViewData["Ordentipo"] = OrdenA == "tipo_asc" ? "tipo_desc" : "tipo_asc";
            ViewData["Filtro"] = Buscar;
            var repuesto = from s in _context.Repuesto select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                repuesto = repuesto.Where(s => s.nombre.Contains(Buscar) || s.categoria.Contains(Buscar) || s.tipo.Contains(Buscar));
            }
            switch (OrdenA)
            {
                case "nom_desc":
                    repuesto = repuesto.OrderByDescending(s => s.nombre);
                    break;
                case "ap_asc":
                    repuesto = repuesto.OrderBy(s => s.categoria);
                    break;
                case "ap_desc":
                    repuesto = repuesto.OrderByDescending(s => s.categoria);
                    break;

                case "tipo_asc":
                    repuesto = repuesto.OrderBy(s => s.tipo);
                    break;
                case "tipo_desc":
                    repuesto = repuesto.OrderByDescending(s => s.tipo);
                    break;

                default:
                    repuesto = repuesto.OrderBy(s => s.nombre);
                    break;
            }
            return View(await repuesto.AsNoTracking().ToListAsync());
        }

        // GET: Repuestoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto
                .FirstOrDefaultAsync(m => m.repuestoID == id);
            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // GET: Repuestoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Repuestoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("repuestoID,nombre,categoria,anio,cantidad,tipo,estadorespuesto")] Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repuesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repuesto);
        }

        // GET: Repuestoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto.FindAsync(id);
            if (repuesto == null)
            {
                return NotFound();
            }
            return View(repuesto);
        }

        // POST: Repuestoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("repuestoID,nombre,categoria,anio,cantidad,tipo,estadorespuesto")] Repuesto repuesto)
        {
            if (id != repuesto.repuestoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repuesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepuestoExists(repuesto.repuestoID))
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
            return View(repuesto);
        }

        // GET: Repuestoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repuesto = await _context.Repuesto
                .FirstOrDefaultAsync(m => m.repuestoID == id);
            if (repuesto == null)
            {
                return NotFound();
            }

            return View(repuesto);
        }

        // POST: Repuestoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repuesto = await _context.Repuesto.FindAsync(id);
            _context.Repuesto.Remove(repuesto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepuestoExists(int id)
        {
            return _context.Repuesto.Any(e => e.repuestoID == id);
        }
    }
}
