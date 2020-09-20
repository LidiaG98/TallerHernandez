using System;
using System.Collections.Generic;
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
    public class AreasController : Controller
    {
        private readonly TallerHernandezContext _context;
        public AreaModal areaModal;

        public AreasController(TallerHernandezContext context)
        {
            _context = context;
            areaModal = new AreaModal(context);
        }

        // GET: Areas
        public async Task<IActionResult> Index(string OrdenA,string Buscar)
        {

            ViewData["OrdenNom"] = String.IsNullOrEmpty(OrdenA) ? "nom_desc" : "";
           
            ViewData["Filtro"] = Buscar;
            var area = from s in _context.Area select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                area = area.Where(s =>s.areaNom.Contains(Buscar) );
            }
            switch (OrdenA)
            {
                case "nom_desc":
                    area = area.OrderByDescending(s => s.areaNom);
                    break;
                
                  
                default:
                    area = area.OrderBy(s => s.areaNom);
                    break;
            }

            return View(await area.AsNoTracking().ToListAsync());

        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Area
                .FirstOrDefaultAsync(m => m.AreaID == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
       
        public List<IdentityError> nuevoArea(string areaNom)
        {
           
            return areaModal.nuevoArea(areaNom);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AreaID,areaNom")] Area area)
        {
            if (ModelState.IsValid)
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Area.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AreaID,areaNom")] Area area)
        {
            if (id != area.AreaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.AreaID))
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
            return View(area);
        }

        // GET: Areas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var area = await _context.Area
                .FirstOrDefaultAsync(m => m.AreaID == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var area = await _context.Area.FindAsync(id);
            _context.Area.Remove(area);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(int id)
        {
            return _context.Area.Any(e => e.AreaID == id);
        }
    }
}
