using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerHernandez.Areas.Cliente.Models;

namespace TallerHernandez.Areas.Cliente.Controllers
{    
    [Area("Cliente")]
    public class ClienteController : Controller
    {
        ClienteCRUD clienteCRUD = new ClienteCRUD();
        public IActionResult Index()
        {
            List<Cliente.Models.Cliente> listCliente = new List<Cliente.Models.Cliente>();
            listCliente = clienteCRUD.ObtenerTodos().ToList();
            return View(listCliente);
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
    }
}
