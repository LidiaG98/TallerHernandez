using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.Controllers
{
    public class EmpleadoesController : Controller
    {
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly TallerHernandezContext _context;

        public EmpleadoesController(TallerHernandezContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index(string OrdenA,string Buscar)

        {
            var tallerHernandezContext = _context.Empleado.Include(e => e.area).Include(e => e.modoPago).Include(e => e.rol);
            
            ViewData["OrdenNom"] = String.IsNullOrEmpty(OrdenA) ? "nom_desc" : "";
            ViewData["OrdenAp"] = OrdenA == "ap_asc" ? "ap_desc" : "ap_asc";
            ViewData["Filtro"] = Buscar;
            var empleado = from s in _context.Empleado.Include(e => e.area).Include(e => e.modoPago).Include(e => e.rol) select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                empleado = empleado.Where(s => s.empleadoID.Contains(Buscar) || s.nombre.Contains(Buscar) || s.apellido.Contains(Buscar));
            }
            switch (OrdenA)
            {
                case "nom_desc":
                    empleado = empleado.OrderByDescending(s => s.nombre);
                    break;
                case "ap_asc":
                    empleado = empleado.OrderBy(s => s.apellido);
                    break;
                case "ap_desc":
                    empleado =empleado.OrderByDescending(s => s.apellido);
                    break;
                default:
                    empleado = empleado.OrderBy(s => s.nombre);
                    break;
            }
      
            return View(await empleado.AsNoTracking().ToListAsync());
            
          //  return View(await tallerHernandezContext.ToListAsync());
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .Include(e => e.area)
                .Include(e => e.modoPago)
                .Include(e => e.rol)
                .FirstOrDefaultAsync(m => m.empleadoID == id);
           var lml = await _context.Area.FirstOrDefaultAsync(d => d.AreaID == empleado.areaID);
            ViewData["area"] = lml.areaNom;
            var xmx = await _context.ModoPago.FirstOrDefaultAsync(s => s.modopagoID == empleado.modopagoID);
            ViewData["pago"] = xmx.tipo;
            var sms = await _context.Rol.FirstOrDefaultAsync(l => l.rolID == empleado.rolID);
            ViewData["rol"] = sms.rolNom;
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            ViewData["id"] = "falso";
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom");
            ViewData["modopagoID"] = new SelectList(_context.ModoPago, "modopagoID", "tipo");
            ViewData["rolID"] = new SelectList(_context.Rol, "rolID", "rolNom");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("empleadoID,nombre,apellido,correo,telefono,imagen,salario,areaID,rolID,modopagoID")] Empleado empleado)
        {
            ViewData["id"] = "falso";
            if (!(EmpleadoExists(empleado.empleadoID)))
            {
                bool imagenNula = false;

            try
            {
                if (empleado.imagen == null)
                {
                    imagenNula = true;
                }
            }
            catch (Exception e) { Console.WriteLine(e); }  //Verifica si ha subido o no una imagen a la hora de crear


            if (ModelState.IsValid && imagenNula)
            {
                empleado.imagenN = "logoTaller.png";
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if(ModelState.IsValid && empleado.imagen != null) //Modelo valido y si subio una imagen
            {
                Imagen i = empleado.imagen;
                string rootPath = hostEnvironment.WebRootPath;
                string fileName = empleado.nombre;
                fileName = fileName.Replace(" ", "");
                string extension = Path.GetExtension(i.imageFile.FileName);
                i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(rootPath + "/uploads/", fileName);
                //cliente.imagen.imagePath = path;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await i.imageFile.CopyToAsync(fileStream);
                }
                empleado.imagenN = fileName;
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                

               
            }
        }
            else
            {
                ViewData["id"] = "verdad";
            }
    ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", empleado.areaID);
            ViewData["modopagoID"] = new SelectList(_context.ModoPago, "modopagoID", "tipo", empleado.modopagoID);
            ViewData["rolID"] = new SelectList(_context.Rol, "rolID", "rolNom", empleado.rolID);
            return View(empleado);
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", empleado.areaID);
            ViewData["modopagoID"] = new SelectList(_context.ModoPago, "modopagoID", "tipo", empleado.modopagoID);
            ViewData["rolID"] = new SelectList(_context.Rol, "rolID", "rolNom", empleado.rolID);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("empleadoID,nombre,apellido,correo,telefono,imagen,imageN,salario,areaID,rolID,modopagoID")] Empleado empleado)
        {
            bool imagenNula = false;
            try
            {
                if (empleado.imagen == null)
                {
                    imagenNula = true;
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
            if (id != empleado.empleadoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imagenNula)
                    {
                        _context.Update(empleado);
                        await _context.SaveChangesAsync();

                    }
                    else if (empleado.imagen != null)
                    {
                        Imagen i = empleado.imagen;
                        string rootPath = hostEnvironment.WebRootPath;
                        string fileName = empleado.empleadoID;
                        fileName = fileName.Replace(" ", "");
                        string extension = Path.GetExtension(i.imageFile.FileName);
                        i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(rootPath + "/uploads/", fileName);
                        //cliente.imagen.imagePath = path;
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await i.imageFile.CopyToAsync(fileStream);
                        }
                        empleado.imagenN = fileName;

                        _context.Update(empleado);
                        await _context.SaveChangesAsync();

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.empleadoID))
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
            ViewData["areaID"] = new SelectList(_context.Area, "AreaID", "areaNom", empleado.areaID);
            ViewData["modopagoID"] = new SelectList(_context.ModoPago, "modopagoID", "tipo", empleado.modopagoID);
            ViewData["rolID"] = new SelectList(_context.Rol, "rolID", "rolNom", empleado.rolID);
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .Include(e => e.area)
                .Include(e => e.modoPago)
                .Include(e => e.rol)
                .FirstOrDefaultAsync(m => m.empleadoID == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(string id)
        {
            return _context.Empleado.Any(e => e.empleadoID == id);
        }

        public async Task<List<Empleado>> VeniteEmpleado(string id)
        {
            var bombolbi = from awa in _context.Empleado select awa;
            bombolbi = bombolbi.Where(awa => awa.empleadoID == id);
            return await bombolbi.ToListAsync();
        }
        public async Task<String> EliminarEmpleado(string id)
        {

            var respuesta = "";
            try
            {
                var bombolbi = await _context.Empleado.FindAsync(id);
                _context.Empleado.Remove(bombolbi);
                await _context.SaveChangesAsync();
                respuesta = "Delete";
            }
            catch
            {
                respuesta = "NoDelete";
            }
            return respuesta;
        }
    }
}
