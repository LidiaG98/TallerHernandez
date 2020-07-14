using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerHernandez.Areas.Cliente.Models;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Cliente.Controllers
{    
    [Area("Cliente")]
    public class ClienteController : Controller
    {
        ControlDB clienteCRUD = new ControlDB();
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
        public IActionResult Insertar([Bind] Models.Cliente cliente)
        {
            if(ModelState.IsValid)
            {
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
        public IActionResult Actualizar([Bind] Models.Cliente c)
        {                     
            if(ModelState.IsValid)
            {
                clienteCRUD.ActualizarCliente(c);
                return RedirectToAction("Index");
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
            clienteCRUD.EliminarCliente(c.ID);
            return RedirectToAction("Index");                     
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
