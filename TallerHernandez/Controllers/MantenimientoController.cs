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
    public class MantenimientoController : Controller
    {
        private readonly TallerHernandezContext _context;

        public MantenimientoController(TallerHernandezContext context)
        {
            _context = context;
        }

        // GET: Mantenimiento
        public async Task<IActionResult> Index()
        {
            var tallerHernandezContext = _context.Mantenimiento.Include(m => m.area);
            return View(await tallerHernandezContext.ToListAsync());
        }

        // GET: Mantenimiento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimiento
                .Include(m => m.area)
                .FirstOrDefaultAsync(m => m.mantenimientoID == id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            return View(mantenimiento);
        }

        // GET: Mantenimiento/Create
        public IActionResult Create()
        {
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom");
            return View();
        }

        // POST: Mantenimiento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("mantenimientoID,nombre,precio,areaID")] Mantenimiento mantenimiento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mantenimiento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", mantenimiento.areaID);
            return View(mantenimiento);
        }

        // GET: Mantenimiento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimiento.FindAsync(id);
            if (mantenimiento == null)
            {
                return NotFound();
            }
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", mantenimiento.areaID);
            return PartialView(mantenimiento);
        }

        // POST: Mantenimiento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("mantenimientoID,nombre,precio,areaID")] Mantenimiento mantenimiento)
        {
            if (id != mantenimiento.mantenimientoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mantenimiento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MantenimientoExists(mantenimiento.mantenimientoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { SAVE = "Y" });
            }
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", mantenimiento.areaID);
            return PartialView("Edit", mantenimiento);
        }

        // GET: Mantenimiento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mantenimiento = await _context.Mantenimiento
                .Include(m => m.area)
                .FirstOrDefaultAsync(m => m.mantenimientoID == id);
            if (mantenimiento == null)
            {
                return NotFound();
            }

            return View(mantenimiento);
        }

        // POST: Mantenimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mantenimiento = await _context.Mantenimiento.FindAsync(id);
            _context.Mantenimiento.Remove(mantenimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MantenimientoExists(int id)
        {
            return _context.Mantenimiento.Any(e => e.mantenimientoID == id);
        }
    }
}
