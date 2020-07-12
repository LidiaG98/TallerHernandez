using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerHernandez.Areas.Empleado.Models;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Empleado.Controllers
{
    [Area("Empleado")]
    public class EmpleadoController : Controller
    {
        ControlDB dBempleado = new ControlDB();
        public IActionResult Empleado()
        {
            List<Models.Empleado> empList = new List<Models.Empleado>();
            empList = dBempleado.GetAllEmployee().ToList();
            return View(empList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Models.Empleado objEmp)
        {
            if (ModelState.IsValid)
            {
                dBempleado.InsertarEmpleado(objEmp);
                return RedirectToAction("Empleado");
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
    }
}
