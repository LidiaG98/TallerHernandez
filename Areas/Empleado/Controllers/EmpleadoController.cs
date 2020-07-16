using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TallerHernandez.Areas.Empleado.Models;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Empleado.Controllers
{
    [Area("Empleado")]
    public class EmpleadoController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IWebHostEnvironment hostEnvironment;

        public EmpleadoController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.hostEnvironment = hostEnvironment;
        }

        ControlDB dBempleado = new ControlDB();
        public IActionResult Empleado()
        {
            List<Models.Empleado> empList = new List<Models.Empleado>();
            empList = dBempleado.GetAllEmployee().ToList();
            ViewModel model = new ViewModel();
            model.E = new Models.Empleado();
            model.Empleados = empList;

            //Obteniendo areas
            List<AreaViewModel> ma = new List<AreaViewModel>();           
            ma = dBempleado.ObtenerArea().ToList();

            //Obteniendo modos de pago
            List<MPagoViewModel> mPago = new List<MPagoViewModel>();
            mPago = dBempleado.ObtenerModoPago().ToList();

            //Pasando lista de pagos y areas a vista
            ViewBag.pagos = mPago;
            ViewBag.items = ma;

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
            bool imagenNula=false;
            try
            {
                if(objEmp.imagen.imageFile == null)
                {
                    imagenNula = false;
                }
            }
            catch (Exception e) { imagenNula = true; }

            if (ModelState.IsValid && imagenNula)
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
                        Models.datosUsuario d = new datosUsuario();
                        d.correo = objEmp.correo;
                        d.contra = dBempleado.crearContra(objEmp.nombre);
                        return RedirectToAction("datosUsuario",d);
                    }                    
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else if (ModelState.IsValid && objEmp.imagen.imageFile!=null)
            {
                //Rellenar datos de imagen
                Imagen i = objEmp.imagen;
                i.idDuenio = objEmp.idEmpleado.ToString();
                i.perteneceA = "Empleado";                
                //Crear nombre y ruta. Guardar en folder uploads
                string rootPath = hostEnvironment.WebRootPath;
                string fileName = objEmp.nombre;
                fileName = fileName.Replace(" ","");
                string extension = Path.GetExtension(i.imageFile.FileName);
                i.nombreImagen= fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(rootPath + "/uploads/", fileName);
                i.imagePath = path;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await i.imageFile.CopyToAsync(fileStream);                    
                }
       
                //Crear usuario
                var user = new IdentityUser { UserName = objEmp.correo, Email = objEmp.correo };
                var result = await userManager.CreateAsync(user, dBempleado.crearContra(objEmp.nombre));
                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, objEmp.idRol);
                    objEmp.idUsuario = user.Id;
                    if (result.Succeeded)
                    {
                        objEmp.imagen.nombreImagen = i.nombreImagen;
                        dBempleado.InsertarEmpleado(objEmp);                        
                        //Insertar detalles de imagen en tabla
                        dBempleado.InsertarImagen(i);
                        Models.datosUsuario d = new datosUsuario();
                        d.correo = objEmp.correo;
                        d.contra = dBempleado.crearContra(objEmp.nombre);
                        return RedirectToAction("datosUsuario", d);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
                return View(objEmp);            
        }

        public IActionResult datosUsuario(datosUsuario datos)
        {
            return View(datos);
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
            ViewBag.pagos = pagos;
            ViewBag.items = items;
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind] Models.Empleado objEmp)
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
            ViewBag.pagos = pagos;
            ViewBag.items = items;
            if (ModelState.IsValid)
            {
                try
                {
                    if(objEmp.imagen.imageFile == null)
                    {
                        dBempleado.ActualizarEmpleado(objEmp);
                        return RedirectToAction("Empleado");
                    }
                }
                catch (Exception eL)
                {

                }
                //Eliminar imagen anterior
                Models.Empleado e = dBempleado.BuscarEmpleado(objEmp.idEmpleado);
                Imagen i = new Imagen();
                i.imageFile = objEmp.imagen.imageFile;
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, "uploads", e.imagen.nombreImagen);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                //Crear nombre y ruta. Guardar en folder uploads
                string rootPath = hostEnvironment.WebRootPath;
                string fileName = objEmp.nombre;
                fileName = fileName.Replace(" ", "");
                string extension = Path.GetExtension(i.imageFile.FileName);
                i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(rootPath + "/uploads/", fileName);
                i.imagePath = path;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await i.imageFile.CopyToAsync(fileStream);
                }
                objEmp.imagen.nombreImagen = i.nombreImagen;
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
        public async Task<IActionResult> DeleteEmp([Bind]Models.Empleado emp)
        {
            if (emp != null)
            {
                emp = dBempleado.BuscarEmpleado(emp.idEmpleado);
                var user = await userManager.FindByIdAsync(emp.idUsuario);                
                var rolesForUser = await userManager.GetRolesAsync(user);
                if (rolesForUser.Count() > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        var result = await userManager.RemoveFromRoleAsync(user, item);
                    }
                }
                //Eliminar imagen
                try
                {
                    var imagePath = Path.Combine(hostEnvironment.WebRootPath, "uploads", emp.imagen.nombreImagen);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    dBempleado.EliminarEmpleado(emp.idEmpleado);
                    await userManager.DeleteAsync(user);
                }
                catch (ArgumentNullException aNull)
                {
                    dBempleado.EliminarEmpleado(emp.idEmpleado);
                    await userManager.DeleteAsync(user);
                }
                               
                                
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
                vm.E.nombre = "";
            }
            listEmpleado = dBempleado.BusquedaEmpleado(vm.E).ToList();
            vm.Empleados = listEmpleado;
            //Obteniendo areas
            List<AreaViewModel> ma = new List<AreaViewModel>();
            ma = dBempleado.ObtenerArea().ToList();

            //Obteniendo modos de pago
            List<MPagoViewModel> mPago = new List<MPagoViewModel>();
            mPago = dBempleado.ObtenerModoPago().ToList();

            //Pasando lista de pagos y areas a vista
            ViewBag.pagos = mPago;
            ViewBag.items = ma;
            return View("Empleado", vm);
        }
    }
    public class ViewModel
    {
        public IEnumerable<Models.Empleado> Empleados { get; set; }
        public Models.Empleado E { get; set; }
    }
}
