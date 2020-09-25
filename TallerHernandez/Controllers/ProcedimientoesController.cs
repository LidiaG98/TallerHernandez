using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.Controllers
{
    public class ProcedimientoesController : Controller
    {
        private readonly TallerHernandezContext _context;

        public ProcedimientoesController(TallerHernandezContext context)
        {
            _context = context;
        }

        // GET: Procedimientoes
        public async Task<IActionResult> Index()
        {
            var tallerHernandezContext = _context.Procedimiento.Include(p => p.area);
            return View(await tallerHernandezContext.ToListAsync());
        }

        // GET: Procedimientoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimiento
                .Include(p => p.area)
                .FirstOrDefaultAsync(m => m.procedimientoID == id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            return View(procedimiento);
        }

        // GET: Procedimientoes/Create
        public IActionResult Create()
        {
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom");
            return View();
        }

        // POST: Procedimientoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("procedimientoID,procedimiento,precio,areaID")] Procedimiento proc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", proc.areaID);
            return View(proc);
        }

        // GET: Procedimientoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimiento.FindAsync(id);
            if (procedimiento == null)
            {
                return NotFound();
            }
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", procedimiento.areaID);
            return PartialView(procedimiento);
        }

        // POST: Procedimientoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("procedimientoID,procedimiento,precio,areaID")] Procedimiento proc)
        {
            if (id != proc.procedimientoID)
            {
                return NotFound();
                
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimientoExists(proc.procedimientoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { SAVE = "Y"});
            }
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", proc.areaID);
            return PartialView("Edit",proc);
        }

        // GET: Procedimientoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimiento = await _context.Procedimiento
                .Include(p => p.area)
                .FirstOrDefaultAsync(m => m.procedimientoID == id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            return View(procedimiento);
        }

        // POST: Procedimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedimiento = await _context.Procedimiento.FindAsync(id);
            _context.Procedimiento.Remove(procedimiento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimientoExists(int id)
        {
            return _context.Procedimiento.Any(e => e.procedimientoID == id);
        }
    }
}
