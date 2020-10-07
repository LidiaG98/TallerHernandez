using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.Controllers
{    
    [Authorize]
    public class AsignacionTareasController : Controller
    {
        private readonly TallerHernandezContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AsignacionTareasController(TallerHernandezContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: AsignacionTareas        
        public async Task<IActionResult> Index(string OrdenAsig, string cadena)
        {
            ViewData["OrdenAuto"] = String.IsNullOrEmpty(OrdenAsig) ? "auto_desc" : "";
            ViewData["OrdenNom"] = OrdenAsig == "nom_asc" ? "nom_desc" : "nom_asc";
            ViewData["Filtro"] = cadena;
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.recepcion).Include(a => a.recepcion.procedimiento).Include(a => a.recepcion.mantenimiento)
                                   select s;
            asignacionTareas = asignacionTareas.Where(s => s.estadoTarea == false);

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.recepcion.automovilID.Contains(cadena) || s.empleado.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.recepcion.automovilID);
                    break;
                case "nom_asc":
                    asignacionTareas = asignacionTareas.OrderBy(s => s.empleado.nombre);
                    break;
                case "nom_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.empleado.nombre);
                    break;
                default:
                    asignacionTareas = asignacionTareas.OrderBy(s => s.recepcion.automovilID);
                    break;
            }

            return View(await asignacionTareas.AsNoTracking().ToListAsync());


            //return View(await asignacionTareas.ToListAsync());
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

        public async Task<IActionResult> CrearAsignacion(string cadena, string OrdenAsig)
        {
            var recepcions = _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.empleado).
                Include(r => r.mantenimiento).Include(r => r.procedimiento).Where(r=>r.estado == 1);

            ViewData["OrdenAuto"] = String.IsNullOrEmpty(OrdenAsig) ? "auto_desc" : "";
            ViewData["OrdenNom"] = OrdenAsig == "nom_asc" ? "nom_desc" : "nom_asc";
            ViewData["Filtro"] = cadena;

            if (!String.IsNullOrEmpty(cadena))
            {
                recepcions = recepcions.Where(r => r.automovilID.Contains(cadena) || r.empleado.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    recepcions = recepcions.OrderByDescending(r => r.automovilID);
                    break;
                case "nom_asc":
                    recepcions = recepcions.OrderBy(r => r.empleado.nombre);
                    break;
                case "nom_desc":
                    recepcions = recepcions.OrderByDescending(r => r.empleado.nombre);
                    break;
                default:
                    recepcions = recepcions.OrderBy(s => s.automovilID);
                    break;
            }

            return View(await recepcions.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> EmpleadoAsig(int id, string cadena, string OrdenAsig)
        {
            ViewData["OrdenAp"] = String.IsNullOrEmpty(OrdenAsig) ? "ape_desc" : "";
            ViewData["OrdenNom"] = OrdenAsig == "nom_asc" ? "nom_desc" : "nom_asc";
            ViewData["Filtro"] = cadena;

            ViewBag.recepcionID = id;
            var encargado = from a in _context.Empleado.Include(a => a.area).Include(a => a.modoPago).Include(a => a.rol)
                            select a;

            Recepcion recepcion = _context.Recepcion.First(i => i.recepcionID == id);
            List<Empleado> empleado = new List<Empleado>();

            if (!recepcion.mantenimientoID.Equals(null) && recepcion.procedimientoID.Equals(null))
            {
                Mantenimiento mantenimiento = _context.Mantenimiento.First(i => i.mantenimientoID == recepcion.mantenimientoID);
                encargado = encargado.Where(a => a.areaID == mantenimiento.areaID);
            }
            else if (recepcion.mantenimientoID.Equals(null) && !recepcion.procedimientoID.Equals(null))
            {
                Procedimiento procedimiento = _context.Procedimiento.First(i => i.procedimientoID == recepcion.procedimientoID);
                encargado = encargado.Where(a => a.areaID == procedimiento.areaID);
            }
            else if (!recepcion.mantenimientoID.Equals(null) && !recepcion.procedimientoID.Equals(null))
            {
                encargado = encargado.Where(a => a.area.areaNom.Equals("Mecánica"));
            }
            if (!String.IsNullOrEmpty(cadena))
            {
                encargado = encargado.Where(r => r.apellido.Contains(cadena) || r.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "ape_desc":
                    encargado = encargado.OrderByDescending(r => r.apellido);
                    break;
                case "nom_asc":
                    encargado = encargado.OrderBy(r => r.nombre);
                    break;
                case "nom_desc":
                    encargado = encargado.OrderByDescending(r => r.nombre);
                    break;
                default:
                    encargado = encargado.OrderBy(s => s.apellido);
                    break;
            }

            return View(await encargado.AsNoTracking().ToListAsync());
        }

        // POST: AsignacionTareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("{empleadoID:string}/{recepcionID:int}")]
        //[HttpPatch]
        public async Task<IActionResult> CreateAsig(string empleadoID, string recepcionID)
        {
            int IDrecepcion = Convert.ToInt16(recepcionID);
            AsignacionTarea asignacionTarea = new AsignacionTarea();
            asignacionTarea.recepcionID = IDrecepcion;
            asignacionTarea.empleadoID = empleadoID;
            
                _context.Add(asignacionTarea);
                await _context.SaveChangesAsync();
                if (asignacionTarea.estadoTarea == false)
                {
                    Recepcion recepcion1 = _context.Recepcion.Find(asignacionTarea.recepcionID);
                    recepcion1.estado = 0; //Actualizando el estado de recepcion a "Asignado"
                    _context.Update(recepcion1);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
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
        /*
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
       */

        public async Task<String> DeleteAsignacionTarea(int id)
        {
            
            var respuesta = "";
            try
            {
                var asignacionTarea = await _context.AsignacionTarea.FindAsync(id);
                _context.AsignacionTarea.Remove(asignacionTarea);
                await _context.SaveChangesAsync();
                respuesta = "Delete";
            }
            catch
            {
                respuesta = "NoDelete";
            }
            return respuesta;
        }

        public async Task<IActionResult> TareasFinalizadas(string OrdenAsig, string cadena)
        {
            ViewData["OrdenAuto"] = String.IsNullOrEmpty(OrdenAsig) ? "auto_desc" : "";
            ViewData["OrdenNom"] = OrdenAsig == "nom_asc" ? "nom_desc" : "nom_asc";
            ViewData["Filtro"] = cadena;
            var tareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.recepcion).Include(a => a.recepcion.procedimiento).Include(a => a.recepcion.mantenimiento)
                         select s;
            tareas = tareas.Where(s => (s.estadoTarea==true));

            if (!String.IsNullOrEmpty(cadena))
            {
                tareas = tareas.Where(s => s.recepcion.automovilID.Contains(cadena) || s.empleado.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    tareas = tareas.OrderByDescending(s => s.recepcion.automovilID);
                    break;
                case "nom_asc":
                    tareas = tareas.OrderBy(s => s.empleado.nombre);
                    break;
                case "nom_desc":
                    tareas = tareas.OrderByDescending(s => s.empleado.nombre);
                    break;
                default:
                    tareas = tareas.OrderBy(s => s.recepcion.automovilID);
                    break;
            }

            return View(await tareas.AsNoTracking().ToListAsync());

            //return View(await tareas.ToListAsync());
        }

        
        public async Task<List<AsignacionTarea>> GetAsignacionTarea(int id)
        {
            var tarea = from a in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.recepcion).Include(a => a.recepcion.procedimiento).
                Include(a => a.recepcion.mantenimiento) select a;
            tarea = tarea.Where(a => a.asignacionTareaID == id);
            return await tarea.ToListAsync();
        }
        public async Task<List<Empleado>> GetListadoEncargado(int id)
        {
            var encargado = from a in _context.Empleado.Include(a => a.area).Include(a => a.modoPago).Include(a => a.rol)
                            select a;

            Recepcion recepcion = _context.Recepcion.First(i => i.recepcionID == id);

            if (!recepcion.mantenimientoID.Equals(null) && recepcion.procedimientoID.Equals(null))
            {
                encargado = encargado.Where(a => a.areaID == recepcion.mantenimiento.areaID);
            }
            else if (recepcion.mantenimientoID.Equals(null) && !recepcion.procedimientoID.Equals(null))
            {
                encargado = encargado.Where(a => a.areaID == recepcion.procedimiento.areaID);
            }
            else if (!recepcion.mantenimientoID.Equals(null) && !recepcion.procedimientoID.Equals(null))
            {
                var user = _userManager.Users;
                string idE = "";
                List<Empleado> empleado = new List<Empleado>();
                foreach (var i in user)
                {
                    if (await _userManager.IsInRoleAsync(i, "Mecánico"))
                    {
                        idE = i.Id;
                        encargado = encargado.Where(a => a.correo == idE);
                        empleado.Add((Empleado)encargado);
                    }
                }

                encargado = (IQueryable<Empleado>)empleado;
            }
            //encargado = encargado.Where(a => a.asignacionTareaID == id);
            return await encargado.ToListAsync();
        }
        public async Task<string> EditarAsignacionTarea(int id, int recepcion, string encargado, Boolean estado, AsignacionTarea asignacionTarea)
        {
            var respuesta = "";
            try
            {
                asignacionTarea = new AsignacionTarea
                {
                    asignacionTareaID = id,
                    recepcionID = recepcion,
                    empleadoID = encargado,
                    estadoTarea = estado
                };

                //Actualizando los datos
                _context.Update(asignacionTarea);
                await _context.SaveChangesAsync();

                respuesta = "Save";
            }
            catch
            {
                respuesta = "No Save";
            }
            return respuesta;
        }
        private bool AsignacionTareaExists(int id)
        {
            return _context.AsignacionTarea.Any(e => e.asignacionTareaID == id);
        }

        //AsignacionTareas para empleado especifico

        public async Task<IActionResult> TareasEnProcesoEmpleado(string OrdenAsig, string cadena)
        {
            ViewData["OrdenAuto"] = String.IsNullOrEmpty(OrdenAsig) ? "auto_desc" : "";
            ViewData["Filtro"] = cadena;

            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.Identity.Name;
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.recepcion).Include(a => a.recepcion.procedimiento).Include(a => a.recepcion.mantenimiento)
                                   select s;

            if (!String.IsNullOrEmpty(currentUserID))
            {
                asignacionTareas = asignacionTareas.Where(s => (s.estadoTarea == false) && (s.empleado.correo == currentUserID));
            }

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.recepcion.automovilID.Contains(cadena) || s.empleado.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.recepcion.automovilID);
                    break;
                default:
                    asignacionTareas = asignacionTareas.OrderBy(s => s.recepcion.automovilID);
                    break;
            }

            return View(await asignacionTareas.AsNoTracking().ToListAsync());

            //return View(await asignacionTareas.ToListAsync());
        }
        public async Task<IActionResult> TareasFinalizadasEmpleado(string OrdenAsig, string cadena)
        {
            ViewData["OrdenAuto"] = String.IsNullOrEmpty(OrdenAsig) ? "auto_desc" : "";
            ViewData["Filtro"] = cadena;

            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.Identity.Name;
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.recepcion).Include(a => a.recepcion.procedimiento).Include(a => a.recepcion.mantenimiento)
                                   select s;

            if (!String.IsNullOrEmpty(currentUserID))
            {
                asignacionTareas = asignacionTareas.Where(s => (s.estadoTarea == true) && (s.empleado.correo == currentUserID));
            }

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.recepcion.automovilID.Contains(cadena) || s.empleado.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.recepcion.automovilID);
                    break;
                default:
                    asignacionTareas = asignacionTareas.OrderBy(s => s.recepcion.automovilID);
                    break;
            }

            return View(await asignacionTareas.AsNoTracking().ToListAsync());

            //return View(await asignacionTareas.ToListAsync());
        }

    }
}
