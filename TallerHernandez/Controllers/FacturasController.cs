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
    public class FacturasController : Controller
    {
        private readonly TallerHernandezContext _context;

        public FacturasController(TallerHernandezContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index(int? mes, int? anio)
        {
            var factura = from s in _context.Factura.Include(a => a.cliente) select s;
            double sumaMes = 0;
            Factura fact = factura.FirstOrDefault();
            DateTime fechahoy = DateTime.Now;
            List<int> anios = new List<int>();
            


            if (mes == null) 
            {
                
                DateTime esteMes = new DateTime(fechahoy.Year, fechahoy.Month, 1);
                DateTime oUltimoDiaDelMes = esteMes.AddMonths(1).AddDays(-10);
                factura = factura.Where(s => s.fechaEmision >= esteMes && s.fechaEmision <= fechahoy);         
            }
            else 
            {
                
                DateTime esteMes = new DateTime(fechahoy.Year, (int)mes, 1);
                DateTime oUltimoDiaDelMes = esteMes.AddMonths(1).AddDays(-1);
                factura = factura.Where(s => s.fechaEmision >= esteMes && s.fechaEmision <= oUltimoDiaDelMes);
            }
            List<Factura> facturasx = factura.ToList();
            foreach (Factura f in facturasx)
            {
                sumaMes = sumaMes + f.totalNeto;
            }
            for (int i = fact.fechaEmision.Year; i<=fechahoy.Year; i++ ) 
            {
                anios.Add(i);
            }
            List<SelectListItem> anios12 = anios.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ToString(),
                    Value = a.ToString(),
                    Selected = false
                };
            });
            ViewData["total"] = sumaMes;
            ViewData["anio"] = fechahoy.Year;
            ViewBag.Anios = anios12;
            return View(await factura.AsNoTracking().ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .FirstOrDefaultAsync(m => m.facturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("facturaId,idRecepcion,idCliente,impuesto,total,totalNeto,fechaEmision")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("facturaId,idRecepcion,idCliente,impuesto,total,totalNeto,fechaEmision")] Factura factura)
        {
            if (id != factura.facturaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.facturaId))
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
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factura = await _context.Factura
                .FirstOrDefaultAsync(m => m.facturaId == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Factura.FindAsync(id);
            _context.Factura.Remove(factura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(int id)
        {
            return _context.Factura.Any(e => e.facturaId == id);
        }
    }
}
