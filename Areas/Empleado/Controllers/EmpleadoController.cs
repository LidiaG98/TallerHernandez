using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using TallerHernandez.Areas.Empleado.Models;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Empleado.Controllers
{
    [Area("Empleado")]
    public class EmpleadoController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public EmpleadoController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;

        }

        ControlDB dBempleado = new ControlDB();
        public IActionResult Empleado()
        {
            List<Models.Empleado> empList = new List<Models.Empleado>();
            empList = dBempleado.GetAllEmployee().ToList();
            ViewModel model = new ViewModel();
            model.E = new Models.Empleado();
            model.Empleados = empList;
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<AreaViewModel> ma = new List<AreaViewModel>();
            ma = dBempleado.ObtenerArea().ToList();
            List<SelectListItem> items = ma.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombreArea.ToString(),
                    Value = d.idArea.ToString(),
                    Selected = false
                };
            });            

            List<MPagoViewModel> mPago = new List<MPagoViewModel>();
            mPago = dBempleado.ObtenerModoPago().ToList();
            List<SelectListItem> pagos = mPago.ConvertAll(b =>
            {
                return new SelectListItem()
                {
                    Text = b.tipoPago.ToString(),
                    Value = b.idModo.ToString(),
                    Selected = false
                };
            });

            var roles = roleManager.Roles.ToList();
            List<SelectListItem> rol = roles.ConvertAll(r =>
            {
                return new SelectListItem()
                {
                    Text = r.Name.ToString(),
                    Value = r.Name.ToString(),
                    Selected = false
                };
            });

            ViewBag.pagos = pagos;
            ViewBag.items = items;
            ViewBag.roles = rol;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind] Models.Empleado objEmp)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = objEmp.correo, Email = objEmp.correo };
                var result = await userManager.CreateAsync(user, dBempleado.crearContra(objEmp.nombre));
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, objEmp.idRol);
                    objEmp.idUsuario = user.Id;
                    if (result.Succeeded)
                    {
                        dBempleado.InsertarEmpleado(objEmp);
                        return RedirectToAction("Empleado");
                    }                    
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(objEmp);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Empleado emp = dBempleado.BuscarEmpleado(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Models.Empleado objEmp)
        {                       
            if (ModelState.IsValid)
            {
                dBempleado.ActualizarEmpleado(objEmp);
                return RedirectToAction("Empleado");
            }
            return View(objEmp);
        }

        /*[HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Empleado emp = dBempleado.BuscarEmpleado(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }*/

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Empleado emp = dBempleado.BuscarEmpleado(id);
            if (emp == null)
            {
                return NotFound();
            }
            return View(emp);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp([Bind]Models.Empleado emp)
        {
            if (emp != null)
            {
                dBempleado.EliminarEmpleado(emp.idEmpleado);
                return RedirectToAction("Empleado");
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Busqueda([Bind] ViewModel vm)
        {
            List<Empleado.Models.Empleado> listEmpleado = new List<Empleado.Models.Empleado>();
            if (vm.E.nombre == null)
            {
                return NotFound();
            }
            listEmpleado = dBempleado.BusquedaEmpleado(vm.E).ToList();
            vm.Empleados = listEmpleado;
            return View("Empleado", vm);
        }
    }
    public class ViewModel
    {
        public IEnumerable<Models.Empleado> Empleados { get; set; }
        public Models.Empleado E { get; set; }
    }
}
