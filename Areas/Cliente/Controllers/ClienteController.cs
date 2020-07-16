using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TallerHernandez.Areas.Cliente.Models;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Cliente.Controllers
{    
    [Area("Cliente")]
    public class ClienteController : Controller
    {
        ControlDB clienteCRUD = new ControlDB();
        private readonly IWebHostEnvironment hostEnvironment;

        public ClienteController(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            List<Cliente.Models.Cliente> listCliente = new List<Cliente.Models.Cliente>();
            listCliente = clienteCRUD.ObtenerTodos().ToList();
            ViewModel model = new ViewModel();
            model.c = new Models.Cliente();
            model.Clientes = listCliente;
            return View(model);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insertar([Bind] Models.Cliente cliente)
        {
            bool imagenNula = false;
            try
            {
                if (cliente.image.imageFile == null)
                {
                    imagenNula = false;
                }
            }
            catch (Exception e) { imagenNula = true; }

            if (ModelState.IsValid && imagenNula)//no subieron foto
            {
                cliente.image = new Imagen();
                cliente.image.nombreImagen = "";
                clienteCRUD.InsertarCliente(cliente);
                return RedirectToAction("Index");
            }
            else if(ModelState.IsValid && cliente.image.imageFile != null)
            {
                Imagen i = cliente.image;
                string rootPath = hostEnvironment.WebRootPath;
                string fileName = cliente.nombre;
                fileName = fileName.Replace(" ", "");
                string extension = Path.GetExtension(i.imageFile.FileName);
                i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(rootPath + "/uploads/", fileName);
                i.imagePath = path;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await i.imageFile.CopyToAsync(fileStream);
                }               
                cliente.image.nombreImagen = i.nombreImagen;
                clienteCRUD.InsertarCliente(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);  
        }
       
        public IActionResult Actualizar(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Models.Cliente cliente = clienteCRUD.ObtenerCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar([Bind] Models.Cliente c)
        {               
            try
            {                              
                if (ModelState.IsValid && c.image.imageFile == null)//La persona no cambió foto
                {                   
                    clienteCRUD.ActualizarCliente(c);
                    return RedirectToAction("Index");
                }
                else if (ModelState.IsValid && c.image.imageFile != null) //La persona cambió la foto, pero ya tenía foto
                {
                    var imagePath = Path.Combine(hostEnvironment.WebRootPath, "uploads", c.image.nombreImagen);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    Imagen i = c.image;
                    string rootPath = hostEnvironment.WebRootPath;
                    string fileName = c.nombre;
                    fileName = fileName.Replace(" ", "");
                    string extension = Path.GetExtension(i.imageFile.FileName);
                    i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(rootPath + "/uploads/", fileName);
                    i.imagePath = path;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await i.imageFile.CopyToAsync(fileStream);
                    }
                    c.image.nombreImagen = i.nombreImagen;
                    clienteCRUD.ActualizarCliente(c);
                    return RedirectToAction("Index");
                }                               
            }
            catch(SqlException n)//para personas que no tienen foto y no cambian la foto
            {
                c.image.nombreImagen = "";
                clienteCRUD.ActualizarCliente(c);   
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException e)//para personas que no tienen foto y cambian la foto
            {
                bool imagenNula = false;
                try
                {
                    if (c.image.imageFile == null)
                    {
                        imagenNula = false;
                    }
                }
                catch (Exception l) { imagenNula = true; }

                if (ModelState.IsValid && imagenNula)
                {
                    clienteCRUD.ActualizarCliente(c);
                    return RedirectToAction("Index");
                }
                else if (ModelState.IsValid && c.image.imageFile != null)
                {
                    Imagen i = c.image;
                    string rootPath = hostEnvironment.WebRootPath;
                    string fileName = c.nombre;
                    fileName = fileName.Replace(" ", "");
                    string extension = Path.GetExtension(i.imageFile.FileName);
                    i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(rootPath + "/uploads/", fileName);
                    i.imagePath = path;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await i.imageFile.CopyToAsync(fileStream);
                    }
                    c.image.nombreImagen = i.nombreImagen;
                    clienteCRUD.ActualizarCliente(c);
                    return RedirectToAction("Index");
                }
            }
            return View(c);
        }

        public IActionResult Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Cliente cliente = clienteCRUD.ObtenerCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar([Bind] Models.Cliente c)
        {           
            try
            {
                //Eliminar imagen
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, "uploads", c.image.nombreImagen);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }                             
                clienteCRUD.EliminarCliente(c.ID);
                return RedirectToAction("Index");
            }
            catch(SqlException e)
            {
                return View("EliminarTodo", c);    
            }
            catch(ArgumentNullException e)
            {
                clienteCRUD.EliminarCliente(c.ID);
                return RedirectToAction("Index");
            }
        }

        public IActionResult EliminarTodo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Models.Cliente cliente = clienteCRUD.ObtenerCliente(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarTodo([Bind] Models.Cliente c)
        {
            try
            {
                var imagePath = Path.Combine(hostEnvironment.WebRootPath, "uploads", c.image.nombreImagen);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                clienteCRUD.EliminarClienteVehiculo(c.ID);
                return RedirectToAction("Index");
            }
            catch(SqlException e)
            {
                clienteCRUD.EliminarClienteVehiculo(c.ID);
                return RedirectToAction("Index");
            }
            catch (ArgumentNullException e)
            {
                clienteCRUD.EliminarClienteVehiculo(c.ID);
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Busqueda([Bind] ViewModel vm)
        {
            if(vm.c.nombre == null)
            {
                vm.c.nombre = "";
            }
            List<Cliente.Models.Cliente> listCliente = new List<Cliente.Models.Cliente>();
            listCliente = clienteCRUD.BusquedaCliente(vm.c).ToList();
            vm.Clientes = listCliente;
            return View("Index", vm);
        }
    }

    public class ViewModel
    {
        public IEnumerable<Models.Cliente> Clientes { get; set; }
        public Models.Cliente c { get; set; }
    }
}
