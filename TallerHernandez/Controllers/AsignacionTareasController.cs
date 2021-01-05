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
using TallerHernandez.ViewModels;

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
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.procedimiento).Include(a => a.procedimiento.recepcion).Include(a=>a.procedimiento.recepcion.Automovil).Include(a => a.procedimiento.area)
                                   select s;
            asignacionTareas = asignacionTareas.Where(s => s.estadoTarea == false);

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.procedimiento.recepcion.Automovil.placa.Contains(cadena) || s.empleado.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.procedimiento.recepcion.Automovil.placa);
                    break;
                case "nom_asc":
                    asignacionTareas = asignacionTareas.OrderBy(s => s.empleado.nombre);
                    break;
                case "nom_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.empleado.nombre);
                    break;
                default:
                    asignacionTareas = asignacionTareas.OrderBy(s => s.procedimiento.recepcion.Automovil.placa);
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
                .Include(a => a.procedimiento)
                .Include(a => a.procedimiento.recepcion)
                .Include(a => a.procedimiento.recepcion.cliente)
                .Include(a => a.procedimiento.recepcion.Automovil)
                .Include(a => a.procedimiento.area)
                .FirstOrDefaultAsync(m => m.asignacionTareaID == id);
            if (asignacionTarea == null)
            {
                return NotFound();
            }

            return View(asignacionTarea);
        }
        public async Task<IActionResult> DetailsAsigE(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionTarea = await _context.AsignacionTarea
                .Include(a => a.empleado)
                .Include(a => a.procedimiento)
                .Include(a => a.procedimiento.recepcion)
                .Include(a => a.procedimiento.recepcion.cliente)
                .Include(a => a.procedimiento.recepcion.Automovil)
                .Include(a => a.procedimiento.area)
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
            var procedimientos = _context.Procedimiento.Include(r => r.recepcion).Include(r => r.recepcion.Automovil).Include(r => r.recepcion.cliente).Include(r => r.recepcion.empleado).Include(r => r.area).Where(r=>r.estado == 1);

            ViewData["OrdenAuto"] = String.IsNullOrEmpty(OrdenAsig) ? "auto_desc" : "";
            ViewData["OrdenNom"] = OrdenAsig == "nom_asc" ? "nom_desc" : "nom_asc";
            ViewData["Filtro"] = cadena;

            if (!String.IsNullOrEmpty(cadena))
            {
                procedimientos = procedimientos.Where(r => r.recepcion.Automovil.placa.Contains(cadena) || r.recepcion.cliente.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    procedimientos = procedimientos.OrderByDescending(r => r.recepcion.Automovil.placa);
                    break;
                case "nom_asc":
                    procedimientos = procedimientos.OrderBy(r => r.recepcion.cliente.nombre);
                    break;
                case "nom_desc":
                    procedimientos = procedimientos.OrderByDescending(r => r.recepcion.cliente.nombre);
                    break;
                default:
                    procedimientos = procedimientos.OrderBy(s => s.recepcion.Automovil.placa);
                    break;
            }

            return View(await procedimientos.AsNoTracking().ToListAsync());
        }

        public IActionResult EmpleadoAsig(int id/*, string cadena, string OrdenAsig*/)
        {
            //ViewData["OrdenAp"] = String.IsNullOrEmpty(OrdenAsig) ? "ape_desc" : "";
            //ViewData["OrdenNom"] = OrdenAsig == "nom_asc" ? "nom_desc" : "nom_asc";
            //ViewData["Filtro"] = cadena;

            ViewBag.procedimientoID = id;

            Procedimiento procedimiento = _context.Procedimiento.First(i => i.procedimientoID == id);
            List<Empleado> empleado = _context.Empleado.Include(e => e.area).Where(e => e.areaID == procedimiento.areaID).ToList();
            List<AsignacionTarea> asignacionTareas = _context.AsignacionTarea.Where(s => s.estadoTarea == false).ToList();
            List<Empleado> encargado = new List<Empleado>();
            
            foreach(var e in empleado)
            {
                int cont = 0;
                for (int i = 0; i < asignacionTareas.Count; i++)
                {
                    if (e.empleadoID == asignacionTareas[i].empleadoID)
                    {
                        cont = cont + 1;
                    }
                }
                if(cont < 3)
                {
                    encargado.Add(e);
                }
            }

            //switch (OrdenAsig)
            //{
            //    case "ape_desc":
            //        encargado = encargado.OrderBy(r=>r.apellido);
            //        break;
            //    case "nom_asc":
            //        encargado = encargado.OrderBy(r => r.nombre);
            //        break;
            //    case "nom_desc":
            //        encargado = encargado.OrderByDescending(r => r.nombre);
            //        break;
            //    default:
            //        encargado = encargado.OrderBy(s => s.apellido);
            //        break;
            //}
            ViewBag.empleado = encargado.ToList();
            return View(encargado.ToList());
        }

        // POST: AsignacionTareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("{empleadoID:string}/{recepcionID:int}")]
        //[HttpPatch]
        public async Task<IActionResult> CreateAsig(string empleadoID, string procedimientoID)
        {
            int IDprocedimiento = Convert.ToInt16(procedimientoID);
            int IDempleado = Convert.ToInt16(empleadoID);
            AsignacionTarea asignacionTarea = new AsignacionTarea();
            asignacionTarea.procedimientoID = IDprocedimiento;
            asignacionTarea.empleadoID = IDempleado;

            _context.Add(asignacionTarea);
            await _context.SaveChangesAsync();
            if (asignacionTarea.estadoTarea == false)
            {
                Procedimiento procedimiento = _context.Procedimiento.Find(asignacionTarea.procedimientoID);
                procedimiento.estado = 0; //Actualizando el estado de procedimiento a "Asignado"
                _context.Update(procedimiento);
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

            List<AsignacionTarea> asigTarea = _context.AsignacionTarea.Include(e => e.empleado).Include(e => e.procedimiento).Include(e => e.procedimiento.recepcion)
                .Include(e => e.procedimiento.area).Include(e => e.procedimiento.recepcion.Automovil).Include(e=>e.procedimiento.recepcion.Automovil.cliente).Where(e => e.asignacionTareaID == id).ToList();

            List<Empleado> empleado = _context.Empleado.Where(e => e.empleadoID == asigTarea[0].empleadoID).ToList();
            var nomEmpleado = empleado[0].nombre + " " + empleado[0].apellido;
            ViewData["nomEmpleado"] = nomEmpleado;
            var asignacionTarea = asigTarea[0];
            if (asignacionTarea == null)
            {
                return NotFound();
            }
            //var empleado = from s in _context.Empleado.Include(e => e.area).Include(e => e.modoPago).Include(e => e.rol) select s;
            //empleado = empleado.Where(s => s.rol.rolNom.Equals("Mecánico"));
            //ViewData["empleadoID"] = new SelectList(empleado, "empleadoID", "empleadoID", asignacionTarea.empleadoID);
            //ViewData["recepcionID"] = new SelectList(_context.Recepcion, "recepcionID", "automovilID", asignacionTarea.recepcionID);
            return View(asignacionTarea);
        }

        // POST: AsignacionTareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(/*int id, [Bind("*/int asignacionTareaID,string descripcion, bool estadoTarea/*")] AsignacionTarea asignacionTarea*/)
        {
            //Consultando el registro de la base que coincide con el id del registro que se quiere editar
            List<AsignacionTarea> asignacionTarea = _context.AsignacionTarea.Include(e=>e.empleado).Where(e => e.asignacionTareaID == asignacionTareaID).ToList();
            //Actualizando registro
            asignacionTarea[0].descripcion = descripcion;
            asignacionTarea[0].estadoTarea = estadoTarea;
            //Verificando si el objeto es null para enviar al usuario que no fue encontrado
            if (asignacionTarea == null)
            {
                return NotFound();
            }
            //Verificando que el modelo es valido
            if (ModelState.IsValid)
            {
                try
                {
                    //Actualizando registro de asignacion de tareas en la base de datos
                    _context.Update(asignacionTarea[0]);
                    await _context.SaveChangesAsync();
                    //Consultando en la base de datos el registro del procedimiento que coincide con el id del procedimiento de la asignacion de tarea
                    Procedimiento procedimiento = await _context.Procedimiento.FindAsync(asignacionTarea[0].procedimientoID);
                    //Luego de tener el objeto de procedimiento, se consulta el objeto de recepcion que coincide con el id recepcion del procedimiento
                    Recepcion recepcion = await _context.Recepcion.FindAsync(procedimiento.recepcionID);
                    //Se consulta la lista de asignaciones de tarea donde los procedimientos tengan el mismo id de la recepcion creada anteriormente
                    List<AsignacionTarea> asigTareas = _context.AsignacionTarea.Include(e=>e.procedimiento).Where(e => e.procedimiento.recepcionID == recepcion.recepcionID).ToList();
                    //Se crea una lista de procedimientos donde el id recepcion sea igual al id del objeto recepcion creado anteriormente
                    List<Procedimiento> procedimientos = _context.Procedimiento.Where(e=>e.recepcionID == recepcion.recepcionID).ToList();
                    //Se crea una variable contador que me ayudara a contar la cantidad de procedimientos 
                    int cont = procedimientos.Count;
                    //Se crea una segunda variable contador inicializada en cero
                    int contAT = 0; 
                    //Se crea un bucle for donde j = 0 y j es menor que la variable cont
                    for(int j=0; j<asigTareas.Count; j++)
                    {
                        //Se comprueba que los estados de las asignaciones de tarea sean true y en caso de ser cierta la condicion, la variable contAT se incrementa en 1
                        if (asigTareas[j].estadoTarea == true)
                        {
                            contAT += 1;
                        }
                    }
                    //Se realiza la condicion si cont(que es el numero de procedimientos pertenecientes a la misma recepcion) es igual a contAT (que es el numero de asignaciones de tarea con los mismos id procedimiento y que tienen estado == true (estan finalizadas))
                    if(cont == contAT)
                    {
                        //En caso de cumplirse la condicion, quiere decir que todos los procedimientos de una recepcion ya han sido asignados y ademas finalizados, y se actualiza el estado de la recepcion a terminada (la recepcion que se creo al principio)
                        recepcion.estado = 0;
                        _context.Update(recepcion);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        recepcion.estado = 1;
                        _context.Update(recepcion);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionTareaExists(asignacionTarea[0].asignacionTareaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //Se obtiene el usuario logueado
                ClaimsPrincipal currentUser = this.User;
                //Se crea una variable que contendra el userName del usuario logueado
                var currentUserID = currentUser.Identity.Name;
                //Se evalua que el estado de tarea sea igual a true y el empleado encargado de la asignacion tarea sea distinto del usuario logueado
                if (estadoTarea.Equals(true) && asignacionTarea[0].empleado.correo != currentUserID)
                {
                    //si se cumple se retorna la vista tareas finalizadas
                    return RedirectToAction(nameof(TareasFinalizadas));
                }
                //Se evalua que el estado de tarea sea igual a false y el empleado encargado de la asignacion tareas sea distinto del usuario logueado
                else if (estadoTarea.Equals(false) && asignacionTarea[0].empleado.correo != currentUserID)
                {
                    //si se cumple se retorna la vista Index
                    return RedirectToAction(nameof(Index));
                }
                else if(estadoTarea.Equals(true) && asignacionTarea[0].empleado.correo == currentUserID)
                {
                    return RedirectToAction(nameof(TareasFinalizadasEmpleado));
                }
                else if (estadoTarea.Equals(false) && asignacionTarea[0].empleado.correo == currentUserID)
                {
                    return RedirectToAction(nameof(TareasEnProcesoEmpleado));
                }
            }
            //ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "empleadoID", asignacionTarea.empleadoID);
            //ViewData["recepcionID"] = new SelectList(_context.Recepcion, "recepcionID", "automovilID", asignacionTarea.recepcionID);
            return View(asignacionTarea[0]);
        }

        public async Task<IActionResult> EditarAsigE(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<AsignacionTarea> asigTarea = _context.AsignacionTarea.Include(e => e.empleado).Include(e => e.procedimiento).Include(e => e.procedimiento.recepcion)
                .Include(e => e.procedimiento.area).Include(e => e.procedimiento.recepcion.Automovil).Include(e => e.procedimiento.recepcion.Automovil.cliente).Where(e => e.asignacionTareaID == id).ToList();

            List<Empleado> empleado = _context.Empleado.Where(e => e.empleadoID == asigTarea[0].empleadoID).ToList();
            var nomEmpleado = empleado[0].nombre + " " + empleado[0].apellido;
            ViewData["nomEmpleado"] = nomEmpleado;
            var asignacionTarea = asigTarea[0];
            if (asignacionTarea == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> EmpleadoProgreso(string OrdenA, string Buscar)
        {
            ViewData["Filtro"] = Buscar;
            var empleadoP = from s in _context.EmpleadoProgreso.Include(e=>e.empleado) select s;
            var emple = from x in _context.Empleado select x;
            List<EmpleadoProgreso> empleadoProgresos = new List<EmpleadoProgreso>();

            var tareasAs = from x in _context.AsignacionTarea.Include(e => e.empleado) select x;
            EmpleadoProgreso jose;

            foreach (var em in emple)
            {
                
                if (tareasAs.Count() != 0)
                {
                    jose = new EmpleadoProgreso()
                    {
                        
                        empleado = em,
                        asignacionTarea = tareasAs.Where(l => l.empleadoID == em.empleadoID).ToList(),
                        actTerminadas = tareasAs.Where(x => x.estadoTarea == true && x.empleadoID == em.empleadoID).Count(),
                        actSinTerminar = tareasAs.Where(x => x.estadoTarea == false && x.empleadoID == em.empleadoID).Count(),
                        porcentajeLogrado = (Convert.ToDouble(tareasAs.Where(x => x.estadoTarea == true && x.empleadoID == em.empleadoID).Count()) /Convert.ToDouble(tareasAs.Where(l => l.empleadoID == em.empleadoID).Count()))*100


                    };
                }
                else
                {
                   jose = new EmpleadoProgreso()
                    {
                        
                        empleado = em,
                        asignacionTarea = tareasAs.Where(l => l.empleadoID == em.empleadoID).ToList(),
                        actTerminadas = tareasAs.Where(x => x.estadoTarea == true).Count(),
                        actSinTerminar = tareasAs.Where(x => x.estadoTarea == false).Count(),
                        porcentajeLogrado = -1
                    };
                }
                empleadoProgresos.Add(jose);
            }
            if (!String.IsNullOrEmpty(Buscar))
            {
                empleadoProgresos = empleadoProgresos.Where(s => s.empleado.emploDUI.Contains(Buscar) || s.empleado.apellido.Contains(Buscar) || s.empleado.nombre.Contains(Buscar)).ToList();
            }
          


           


            ViewBag.progreso = empleadoProgresos;


            return View(await empleadoP.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> Detalle(int? id, string OrdenA)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.area = _context.Empleado.Include(e => e.area).FirstOrDefault(m => m.empleadoID == id).area.areaNom;
            var empleado = await _context.Empleado.FirstOrDefaultAsync(m => m.empleadoID == id);
            var asignacionTarea = _context.AsignacionTarea.Where(m => m.empleadoID == id);


           
            switch (OrdenA)
            {
                case "completa":
                    asignacionTarea = _context.AsignacionTarea.Include(a => a.procedimiento).Include(b => b.procedimiento.recepcion).Include(c => c.procedimiento.recepcion.Automovil).Where(m => m.empleadoID == id).Where(x => x.estadoTarea == true);
                    break;
                case "incompleta":
                    asignacionTarea = _context.AsignacionTarea.Where(m => m.empleadoID == id).Include(a => a.procedimiento).Include(b => b.procedimiento.recepcion).Include(c => c.procedimiento.recepcion.Automovil).Where(x => x.estadoTarea == false);
                    break;
               
                default:
                    asignacionTarea = _context.AsignacionTarea.Include(a => a.procedimiento).Include(b => b.procedimiento.recepcion).Include(c => c.procedimiento.recepcion.Automovil).Where(m => m.empleadoID == id);
                    break;
            }
          
            ViewBag.asigTarea = asignacionTarea;

            return View(empleado);
        }

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
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.procedimiento).Include(a => a.procedimiento.recepcion).Include(a => a.procedimiento.recepcion.Automovil).Include(a => a.procedimiento.area)
                                   select s;
            asignacionTareas = asignacionTareas.Where(s => s.estadoTarea == true);

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.procedimiento.recepcion.Automovil.placa.Contains(cadena) || s.empleado.nombre.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.procedimiento.recepcion.Automovil.placa);
                    break;
                case "nom_asc":
                    asignacionTareas = asignacionTareas.OrderBy(s => s.empleado.nombre);
                    break;
                case "nom_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.empleado.nombre);
                    break;
                default:
                    asignacionTareas = asignacionTareas.OrderBy(s => s.procedimiento.recepcion.Automovil.placa);
                    break;
            }

            return View(await asignacionTareas.AsNoTracking().ToListAsync());

            //return View(await tareas.ToListAsync());
        }


        public async Task<List<AsignacionTarea>> GetAsignacionTarea(int id)
        {
            var tarea = from a in _context.AsignacionTarea select a;
            tarea = tarea.Where(a => a.asignacionTareaID == id);
            return await tarea.ToListAsync();
        }
        
        //public async Task<string> EditarAsignacionTarea(int id, int recepcion, string encargado, Boolean estado, AsignacionTarea asignacionTarea)
        //{
        //    var respuesta = "";
        //    try
        //    {
        //        asignacionTarea = new AsignacionTarea
        //        {
        //            asignacionTareaID = id,
        //            recepcionID = recepcion,
        //            empleadoID = encargado,
        //            estadoTarea = estado
        //        };

        //        //Actualizando los datos
        //        _context.Update(asignacionTarea);
        //        await _context.SaveChangesAsync();

        //        respuesta = "Save";
        //    }
        //    catch
        //    {
        //        respuesta = "No Save";
        //    }
        //    return respuesta;
        //}
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
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.procedimiento).Include(a => a.procedimiento.recepcion).Include(a => a.procedimiento.recepcion.Automovil).Include(a => a.procedimiento.area)
                                   select s;

            if (!String.IsNullOrEmpty(currentUserID))
            {
                asignacionTareas = asignacionTareas.Where(s => (s.estadoTarea == false) && (s.empleado.correo == currentUserID));
            }

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.procedimiento.recepcion.Automovil.placa.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.procedimiento.recepcion.Automovil.placa);
                    break;
                default:
                    asignacionTareas = asignacionTareas.OrderBy(s => s.procedimiento.recepcion.Automovil.placa);
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
            var asignacionTareas = from s in _context.AsignacionTarea.Include(a => a.empleado).Include(a => a.procedimiento).Include(a => a.procedimiento.recepcion).Include(a => a.procedimiento.recepcion.Automovil).Include(a => a.procedimiento.area)
                                   select s;

            if (!String.IsNullOrEmpty(currentUserID))
            {
                asignacionTareas = asignacionTareas.Where(s => (s.estadoTarea == true) && (s.empleado.correo == currentUserID));
            }

            if (!String.IsNullOrEmpty(cadena))
            {
                asignacionTareas = asignacionTareas.Where(s => s.procedimiento.recepcion.Automovil.placa.Contains(cadena));
            }

            switch (OrdenAsig)
            {
                case "auto_desc":
                    asignacionTareas = asignacionTareas.OrderByDescending(s => s.procedimiento.recepcion.Automovil.placa);
                    break;
                default:
                    asignacionTareas = asignacionTareas.OrderBy(s => s.procedimiento.recepcion.Automovil.placa);
                    break;
            }

            return View(await asignacionTareas.AsNoTracking().ToListAsync());

            //return View(await asignacionTareas.ToListAsync());
        }

        public async Task<IActionResult> GenerarInforme(string actividades)
        {
            var procedimientos = _context.Procedimiento.Include(r => r.recepcion).Include(r => r.recepcion.Automovil).Include(r => r.recepcion.cliente).Include(r => r.recepcion.empleado).Include(r => r.area).ToList();
            List<Procedimiento> proc = new List<Procedimiento>();

            var asignacionTareas = _context.AsignacionTarea.Include(r => r.procedimiento).Where(r=>r.estadoTarea==false).ToList();

            if (actividades == "asignadas")
            {
                for(int i = 0; i < procedimientos.Count; i++)
                {
                    for(int x = 0; x < asignacionTareas.Count; x++)
                    {
                        if (procedimientos[i].estado == 0 && procedimientos[i].procedimientoID == asignacionTareas[x].procedimientoID)
                        {
                            proc.Add(procedimientos[i]);
                        }
                    }
                }
                
            }else if(actividades == "noasignadas")
            {
                for (int i = 0; i < procedimientos.Count; i++)
                {
                    if (procedimientos[i].estado == 1)
                    {
                        proc.Add(procedimientos[i]);
                    }
                }
            }
            if (proc.Count != 0)
            {
                ViewBag.estado = proc[0].estado;
            }
            
            return View("GenerarInforme", proc); ;
        }

    }
}
