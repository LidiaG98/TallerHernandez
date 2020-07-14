using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerHernandez.Areas.Cliente.Models;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Vehiculo.Controllers
{
    [Area("Vehiculo")]
    public class VehiculoController : Controller
    {
        ControlDB vehiculoCRUD = new ControlDB();
        public IActionResult Index()
        {
            List<Models.Vehiculo> listVehiculo = new List<Models.Vehiculo>();
            listVehiculo = vehiculoCRUD.ObtenerTodosVehiculos().ToList();
            List<Cliente.Models.Cliente> listClientes = vehiculoCRUD.ObtenerTodos().ToList();           
            ViewModel model = new ViewModel();
            model.v = new Models.Vehiculo();
            model.Vehiculos = listVehiculo;
            model.cliente = listClientes;
            return View(model);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InsertarVehiculo([Bind] AsignarClienteModel vm)
        {
                
                vehiculoCRUD.InsertarVehiculo(vm.v);
                return RedirectToAction("Index");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AsignarCliente([Bind] Models.Vehiculo v)
        {
            AsignarClienteModel model = new AsignarClienteModel();
            if (ModelState.IsValid)
            {
                model.c = new Cliente.Models.Cliente();
                model.v = v;
                model.cliente = vehiculoCRUD.ObtenerTodos().ToList();
                return View(model);
            }
            return View(v);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AsignarClienteActualizar([Bind] Models.Vehiculo v)
        {
            AsignarClienteModel model = new AsignarClienteModel();
            if (ModelState.IsValid)
            {
                model.c = new Cliente.Models.Cliente();
                model.v = v;
                model.cliente = vehiculoCRUD.ObtenerTodos().ToList();
                return View(model);
            }
            return View(v);
        }

        public IActionResult Actualizar(string? placa)
        {
            if (placa == null)
            {
                return NotFound();
            }
            Models.Vehiculo vehiculo = vehiculoCRUD.BusquedaVehiculo(placa);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarVehiculo([Bind] AsignarClienteModel vm)
        {            
                vehiculoCRUD.ActualizarVehiculo(vm.v);
                return RedirectToAction("Index");            
        }

        public IActionResult Eliminar(string? placa)
        {
            if (placa == null)
            {
                return NotFound();
            }
            Models.Vehiculo vehiculo = vehiculoCRUD.BusquedaVehiculo(placa);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar([Bind] Models.Vehiculo v)
        {
            vehiculoCRUD.EliminarVehiculo(v.placa);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Busqueda([Bind] ViewModel vm)
        {
            if (vm.v.placa == null)
            {
                vm.v.placa = "";
            }
            List<Models.Vehiculo> listVehiculo = new List<Models.Vehiculo>();
            listVehiculo = vehiculoCRUD.ObtenerVehiculo(vm.v).ToList();
            vm.Vehiculos = listVehiculo;
            vm.cliente = vehiculoCRUD.ObtenerTodos().ToList();
            return View("Index", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BusquedaAsignar([Bind] AsignarClienteModel vm)
        {
            if (vm.c.nombre == null)
            {
                vm.c.nombre = "";
            }            
            vm.cliente = vehiculoCRUD.BusquedaCliente(vm.c).ToList();
            return View("AsignarCliente", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BusquedaAsignarActualizar([Bind] AsignarClienteModel vm)
        {
            if (vm.c.nombre == null)
            {
                vm.c.nombre = "";
            }
            vm.cliente = vehiculoCRUD.BusquedaCliente(vm.c).ToList();
            return View("AsignarClienteActualizar", vm);
        }
    }

    public class ViewModel
    {
        public IEnumerable<Models.Vehiculo> Vehiculos { get; set; }
        public IEnumerable<Cliente.Models.Cliente> cliente { get; set; }
        public Models.Vehiculo v { get; set; }
        public Cliente.Models.Cliente c { get; set; }
    }

    public class AsignarClienteModel
    {        
        public Models.Vehiculo v { get; set; }
        public IEnumerable<Cliente.Models.Cliente> cliente { get; set; }
        public Cliente.Models.Cliente c { get; set; }
    }
}
