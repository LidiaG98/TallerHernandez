using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.ModelModal;
using TallerHernandez.Models;
using TallerHernandez.ViewModels;

namespace TallerHernandez.Controllers
{
    [Authorize]
    public class RecepcionsController : Controller
    {
        private readonly TallerHernandezContext _context;        
        private ProcedimientoModels procedimientoModels;
        private ProcedimientoesController procedimientoesController;        

        public RecepcionsController(TallerHernandezContext context)
        {
            _context = context;            
            procedimientoModels = new ProcedimientoModels(context);
            procedimientoesController = new ProcedimientoesController(context);           
        }

        // GET: Recepcions
        public async Task<IActionResult> Index(string Buscar)
        {
            ViewData["Filtro"] = Buscar;
            //var tallerHernandezContext = _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.empleado).
            //    Include(r => r.mantenimiento).Include(r =>r.procedimiento);
            var recepciones = from s in _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.empleado).Include(r =>r.procedimientos) select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                recepciones = recepciones.Where(s => s.Automovil.placa.Contains(Buscar) || s.cliente.clienteID.Contains(Buscar) || s.cliente.nombre.Contains(Buscar));
            }
            return View(await recepciones.AsNoTracking().ToListAsync());
        }

        
        public async Task<IActionResult> VehiculosProximos(DateTime? start, DateTime? end)
        {
            if(start.HasValue && end.HasValue)
            {
                if (start > end)
                {
                    ModelState.AddModelError("fechaSalida", "La fecha de entrada es mayor a la fecha de salida");
                    var r = from s in _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).
                                      Include(r => r.empleado).Include(r => r.procedimientos)
                                      select s;
                    ViewBag.start = start;
                    ViewBag.end = end;
                    return View("Index", await r.AsNoTracking().ToListAsync());
                }
                DateTime valorEnd = end.Value;
                DateTime fFinalMod = new DateTime(valorEnd.Year, valorEnd.Month, valorEnd.Day, 23, 59, 59);
                List<RecepcionViewModel> recepcionesViewModel = new List<RecepcionViewModel>();
                ViewBag.start = start;
                ViewBag.end = end;
                var recepciones = await _context.Recepcion
                    .Where(x => x.estado == 1
                    && x.fechaSalida >= start
                    && x.fechaSalida <= fFinalMod).Include(r => r.Automovil).Include(r => r.cliente).Include(r => r.procedimientos)
                    .OrderBy(x => x.fechaSalida)
                    .ToListAsync();
                foreach (var recepcion in recepciones)
                {
                    recepcionesViewModel.Add(new RecepcionViewModel
                    {
                        recepcionID = recepcion.recepcionID,
                        diagnostico = recepcion.diagnostico,
                        fechaEntrada = recepcion.fechaEntrada,
                        fechaSalida = recepcion.fechaSalida,
                        clienteID = recepcion.cliente.nombre + " " + recepcion.cliente.apellido,
                        automovilID = recepcion.automovilID,
                        marca = recepcion.Automovil.marca,
                        anio = recepcion.Automovil.anio,
                        placa = recepcion.Automovil.placa,
                        imagen = recepcion.Automovil.imagenN,
                        procedimientos = recepcion.procedimientos.ToList()
                    });
                }
                //return View("Index", recepciones);
                return View(recepcionesViewModel);
            }            
            else
            {
                var recepciones = from s in _context.Recepcion.Include(r => r.Automovil).Include(r => r.cliente).
                                  Include(r => r.empleado).Include(r => r.procedimientos) select s;
                return View("Index", await recepciones.AsNoTracking().ToListAsync());
            }            
        }      


        // GET: Recepcions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcion = await _context.Recepcion
                .Include(r => r.Automovil)
                .Include(r => r.cliente)
                .Include(r => r.empleado)                
                .Include(r => r.procedimientos)
                .FirstOrDefaultAsync(m => m.recepcionID == id);
            if (recepcion == null)
            {
                return NotFound();
            }

            return PartialView(recepcion);
        }

        [HttpPost]
        public List<IdentityError> agregarProcedimiento(string procedimiento, string precio, string areaID)
        {
            return procedimientoModels.agregarProcedimiento(procedimiento, precio, areaID);
        }

        [HttpPost]
        public async Task<IActionResult> editarProce (int id,string precio, string desc, int idArea)
        {
            Procedimiento p = new Procedimiento();
            p.procedimientoID = id;            
            p.precio = float.Parse(precio, System.Globalization.CultureInfo.InvariantCulture);
            p.procedimiento = desc;
            p.areaID = idArea;

            return await procedimientoesController.Edit(id, p);
        }

        public IActionResult obtenerDuenio(string? id) {
            List<Automovil> duenioId = new List<Automovil>();
            duenioId = _context.Automovil.FromSqlRaw("SELECT * FROM Automovil WHERE automovilID='"+id+"'").ToList();
            List<Cliente> duenio = new List<Cliente>();
            duenio = _context.Cliente.FromSqlRaw("SELECT * FROM Cliente WHERE clienteID='" + duenioId[0].clienteID + "'").ToList();
            List<SelectListItem> resp = duenio.ConvertAll(d => {
                return new SelectListItem()
                {
                    Text = d.nombre+" "+d.apellido,
                    Value = d.clienteID,
                    Selected = true
                };
            });
            string json = JsonSerializer.Serialize(resp);
            return Ok(json);
        }

        public IActionResult obtenerAreas()
        {
            List<Area> resp = _context.Area.ToList();
            return Json(resp);
        }

        // GET: Recepcions/Create
        public IActionResult Create()
        {
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "placa");
            List<Cliente> clientes = _context.Cliente.ToList();
            List<SelectListItem> c = clientes.ConvertAll(cc =>
            {
                return new SelectListItem()
                {
                    Text = cc.nombre+" "+cc.apellido,
                    Value = cc.clienteID,
                    Selected = false
                };
            });
            ViewData["clienteID"] = c;
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "nombre");           
            List<Area> areas = new List<Area>();
            areas= _context.Area.FromSqlRaw("SELECT * FROM Area").ToList();
            List<SelectListItem> a = areas.ConvertAll(ac =>
            {
                return new SelectListItem()
                {
                    Text = ac.areaNom,
                    Value = ac.AreaID.ToString(),
                    Selected = false
                };
            });
            ViewBag.areas = a;
            RecepcionViewModel r = new RecepcionViewModel();
            string fa = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            r.fechaEntrada = DateTime.ParseExact(fa, "dd/MM/yyyy HH:mm", null);            
            return View(r);
        }

        // POST: Recepcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("diagnostico,fechaEntrada,fechaSalida,clienteID,empleadoID,automovilID," +
        //    "procedimientoID,mantenimientoID,estado")] Recepcion recepcion)
        public async Task<IActionResult> Create(RecepcionViewModel recepcion)
        {
            if (ModelState.IsValid)
            {
                Recepcion r = new Recepcion { 
                    diagnostico = recepcion.diagnostico,
                    fechaEntrada = recepcion.fechaEntrada,
                    fechaSalida = recepcion.fechaSalida,
                    clienteID = recepcion.clienteID,
                    empleadoID = recepcion.empleadoID,
                    automovilID = recepcion.automovilID,
                    estado = recepcion.estado 
                };
                _context.Add(r);
                foreach (var procedimiento in recepcion.procedimientos)
                {
                    _context.Procedimiento.Add(new Procedimiento { 
                        procedimiento = procedimiento.procedimiento,
                        precio = procedimiento.precio,
                        areaID = procedimiento.areaID,
                        recepcion = r
                    });
                }                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "automovilID", recepcion.automovilID);
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "nombre", recepcion.clienteID);
            ViewData["empleadoID"] = new SelectList(_context.Empleado, "empleadoID", "nombre", recepcion.empleadoID);                       
            List<Area> areas = new List<Area>();
            areas = _context.Area.FromSqlRaw("SELECT * FROM Area").ToList();
            List<SelectListItem> a = areas.ConvertAll(ac =>
            {
                return new SelectListItem()
                {
                    Text = ac.areaNom,
                    Value = ac.AreaID.ToString(),
                    Selected = false
                };
            });
            ViewBag.areas = a;

            return View(recepcion);
        }

        public async Task<IActionResult> ImprimirFicha(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcion = await _context.Recepcion.Include(r => r.procedimientos).ThenInclude(a => a.area).Where(r => r.recepcionID == id).FirstOrDefaultAsync();
            RecepcionViewModel r = new RecepcionViewModel
            {
                recepcionID = recepcion.recepcionID,
                diagnostico = recepcion.diagnostico,
                fechaEntrada = recepcion.fechaEntrada,
                fechaSalida = recepcion.fechaSalida,
                clienteID = recepcion.clienteID,
                empleadoID = recepcion.empleadoID,
                automovilID = recepcion.automovilID,
                procedimientos = recepcion.procedimientos.ToList(),
                estado = recepcion.estado
            };
            if (recepcion == null)
            {
                return NotFound();
            }
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "placa", recepcion.automovilID);
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "nombre", recepcion.clienteID);
            List<Cliente> clientes = _context.Cliente.ToList();
            List<SelectListItem> c = clientes.ConvertAll(cc =>
            {
                if (cc.clienteID == recepcion.clienteID)
                {
                    return new SelectListItem()
                    {
                        Text = cc.nombre + " " + cc.apellido,
                        Value = cc.clienteID,
                        Selected = true
                    };
                }
                else
                {
                    return new SelectListItem()
                    {
                        Text = cc.nombre + " " + cc.apellido,
                        Value = cc.clienteID,
                        Selected = false
                    };
                }
            });
            ViewData["clienteID"] = c;
            List<Empleado> empleados = _context.Empleado.ToList();
            List<SelectListItem> e = empleados.ConvertAll(ee =>
            {
                if (ee.empleadoID == recepcion.empleadoID)
                {
                    return new SelectListItem()
                    {
                        Text = ee.nombre + " " + ee.apellido,
                        Value = ee.empleadoID.ToString(),
                        Selected = true
                    };
                }
                else
                {
                    return new SelectListItem()
                    {
                        Text = ee.nombre + " " + ee.apellido,
                        Value = ee.empleadoID.ToString(),
                        Selected = false
                    };
                }
            });
            ViewData["empleadoID"] = e;
            ViewBag.areas = _context.Area;
            ViewBag.area = new SelectList(_context.Area, "AreaID", "areaNom");
            return View(r);
        }

        // GET: Recepcions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcion = await _context.Recepcion.Include(r => r.procedimientos).ThenInclude(a => a.area).Where(r => r.recepcionID == id).FirstOrDefaultAsync();            
            RecepcionViewModel r = new RecepcionViewModel { 
                recepcionID = recepcion.recepcionID,
                diagnostico = recepcion.diagnostico,
                fechaEntrada = recepcion.fechaEntrada,
                fechaSalida = recepcion.fechaSalida,
                clienteID = recepcion.clienteID,
                empleadoID = recepcion.empleadoID,
                automovilID = recepcion.automovilID,
                procedimientos = recepcion.procedimientos.ToList(),
                estado = recepcion.estado
            }; 
            if (recepcion == null)
            {
                return NotFound();
            }
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "placa", recepcion.automovilID);
            ViewData["clienteID"] = new SelectList(_context.Cliente, "clienteID", "nombre", recepcion.clienteID);
            List<Cliente> clientes = _context.Cliente.ToList();
            List<SelectListItem> c = clientes.ConvertAll(cc =>
            {
                if(cc.clienteID == recepcion.clienteID)
                {
                    return new SelectListItem()
                    {
                        Text = cc.nombre + " " + cc.apellido,
                        Value = cc.clienteID,
                        Selected = true
                    };
                }
                else
                {
                    return new SelectListItem()
                    {
                        Text = cc.nombre + " " + cc.apellido,
                        Value = cc.clienteID,
                        Selected = false
                    };
                }                
            });
            ViewData["clienteID"] = c;
            List<Empleado> empleados = _context.Empleado.ToList();
            List<SelectListItem> e = empleados.ConvertAll(ee =>
            {
                if (ee.empleadoID == recepcion.empleadoID)
                {
                    return new SelectListItem()
                    {
                        Text = ee.nombre + " " + ee.apellido,
                        Value = ee.empleadoID.ToString(),
                        Selected = true
                    };
                }
                else
                {
                    return new SelectListItem()
                    {
                        Text = ee.nombre + " " + ee.apellido,
                        Value = ee.empleadoID.ToString(),
                        Selected = false
                    };
                }
            });
            ViewData["empleadoID"] = e;
            ViewBag.areas = _context.Area;
            ViewBag.area = new SelectList(_context.Area, "AreaID", "areaNom");
            return View(r);
        }

        // POST: Recepcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RecepcionViewModel recepcion)
        {
            if (id != recepcion.recepcionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Recepcion r = new Recepcion
                    {
                        recepcionID = recepcion.recepcionID,
                        diagnostico = recepcion.diagnostico,
                        fechaEntrada = recepcion.fechaEntrada,
                        fechaSalida = recepcion.fechaSalida,
                        clienteID = recepcion.clienteID,
                        empleadoID = recepcion.empleadoID,
                        automovilID = recepcion.automovilID,
                        estado = recepcion.estado,
                        procedimientos = recepcion.procedimientos
                    };
                    List<Procedimiento> pOriginales = _context.Procedimiento.Where(p => p.recepcionID == r.recepcionID).AsNoTracking().ToList();
                    var pEditados = pOriginales.Where(p => r.procedimientos.All(p2 => p2.procedimientoID != p.procedimientoID));
                    foreach (var pE in pEditados)
                    {
                        _context.Procedimiento.Remove(pE);
                    }
                    _context.Update(r);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecepcionExists(recepcion.recepcionID))
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
            ViewData["automovilID"] = new SelectList(_context.Automovil, "automovilID", "placa", recepcion.automovilID);
            List<Cliente> clientes = _context.Cliente.ToList();
            List<SelectListItem> c = clientes.ConvertAll(cc =>
            {
                if (cc.clienteID == recepcion.clienteID)
                {
                    return new SelectListItem()
                    {
                        Text = cc.nombre + " " + cc.apellido,
                        Value = cc.clienteID,
                        Selected = true
                    };
                }
                else
                {
                    return new SelectListItem()
                    {
                        Text = cc.nombre + " " + cc.apellido,
                        Value = cc.clienteID,
                        Selected = false
                    };
                }
            });
            ViewData["clienteID"] = c;
            List<Empleado> empleados = _context.Empleado.ToList();
            List<SelectListItem> e = empleados.ConvertAll(ee =>
            {
                if (ee.empleadoID == recepcion.empleadoID)
                {
                    return new SelectListItem()
                    {
                        Text = ee.nombre + " " + ee.apellido,
                        Value = ee.empleadoID.ToString(),
                        Selected = true
                    };
                }
                else
                {
                    return new SelectListItem()
                    {
                        Text = ee.nombre + " " + ee.apellido,
                        Value = ee.empleadoID.ToString(),
                        Selected = false
                    };
                }
            });
            ViewData["empleadoID"] = e;
            ViewBag.areas = _context.Area;
            ViewBag.area = new SelectList(_context.Area, "AreaID", "areaNom");
            return View(recepcion);
        }

        // GET: Recepcions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recepcion = await _context.Recepcion
                .Include(r => r.Automovil)
                .Include(r => r.cliente)
                .Include(r => r.empleado)
                .Include(r => r.procedimientos)
                .FirstOrDefaultAsync(m => m.recepcionID == id);
            if (recepcion == null)
            {
                return NotFound();
            }

            return PartialView(recepcion);
        }

        // POST: Recepcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recepcion = await _context.Recepcion.FindAsync(id);
            _context.Recepcion.Remove(recepcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecepcionExists(int id)
        {
            return _context.Recepcion.Any(e => e.recepcionID == id);
        }

        public async Task<IActionResult> AgregarVehiculo(int autoId)
        {
            if (autoId == null)
            {
                return NotFound();
            }

            var automovil = await _context.Automovil
                .Include(r => r.cliente)                
                .FirstOrDefaultAsync(m => m.automovilID == autoId);
            if (automovil == null)
            {
                return NotFound();
            }

            return View("Create",automovil);
        }
       
    }
}
