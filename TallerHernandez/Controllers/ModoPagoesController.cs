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
    public class ModoPagoesController : Controller
    {
        private readonly TallerHernandezContext _context;

        public ModoPagoesController(TallerHernandezContext context)
        {
            _context = context;
        }

        // GET: ModoPagoes
        public async Task<IActionResult> Index(string OrdenA,string Buscar)
        {

            ViewData["OrdenNom"] = String.IsNullOrEmpty(OrdenA) ? "nom_desc" : "";

            ViewData["Filtro"] = Buscar;
            var mp = from s in _context.ModoPago select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                mp = mp.Where(s => s.tipo.Contains(Buscar));
            }
            switch (OrdenA)
            {
                case "nom_desc":
                    mp = mp.OrderByDescending(s => s.tipo);
                    break;


                default:
                    mp = mp.OrderBy(s => s.tipo);
                    break;
            }

            return View(await mp.AsNoTracking().ToListAsync());
        }

        // GET: ModoPagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modoPago = await _context.ModoPago
                .FirstOrDefaultAsync(m => m.modopagoID == id);
            if (modoPago == null)
            {
                return NotFound();
            }

            return View(modoPago);
        }

        // GET: ModoPagoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ModoPagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("modopagoID,tipo")] ModoPago modoPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(modoPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(modoPago);
        }

        // GET: ModoPagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modoPago = await _context.ModoPago.FindAsync(id);
            if (modoPago == null)
            {
                return NotFound();
            }
            return View(modoPago);
        }

        // POST: ModoPagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("modopagoID,tipo")] ModoPago modoPago)
        {
            if (id != modoPago.modopagoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(modoPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModoPagoExists(modoPago.modopagoID))
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
            return View(modoPago);
        }

        // GET: ModoPagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var modoPago = await _context.ModoPago
                .FirstOrDefaultAsync(m => m.modopagoID == id);
            if (modoPago == null)
            {
                return NotFound();
            }

            return View(modoPago);
        }

        // POST: ModoPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var modoPago = await _context.ModoPago.FindAsync(id);
            _context.ModoPago.Remove(modoPago);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModoPagoExists(int id)
        {
            return _context.ModoPago.Any(e => e.modopagoID == id);
        }
    }
}
