using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.ModelModal;
using TallerHernandez.Models;


namespace TallerHernandez.Controllers
{
    public class ClientesController : Controller
    {   private readonly IWebHostEnvironment hostEnvironment;
        private readonly TallerHernandezContext _context;
        private ClienteModal clienteModal;
        

        public ClientesController(TallerHernandezContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            clienteModal = new ClienteModal(context);
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Clientes
        public async Task<IActionResult> Index(string OrdenA,string Buscar)
        { 
          ViewData["OrdenNom"] = String.IsNullOrEmpty(OrdenA) ? "nom_desc" : "";
          ViewData["OrdenAp"] = OrdenA == "ap_asc" ? "ap_desc": "ap_asc";
            ViewData["Filtro"] = Buscar;
            var cliente = from s in _context.Cliente select s;
            if(!String.IsNullOrEmpty(Buscar))
            {
                cliente = cliente.Where(s => s.clienteID.Contains(Buscar) || s.nombre.Contains(Buscar) || s.apellido.Contains(Buscar));
            }
            switch(OrdenA)
            {
                case "nom_desc":
                    cliente = cliente.OrderByDescending(s => s.nombre);
                    break;
                case "ap_asc":
                    cliente = cliente.OrderBy(s => s.apellido);
                    break;
                case "ap_desc":
                    cliente = cliente.OrderByDescending(s => s.apellido);
                    break;
                default:
                    cliente = cliente.OrderBy(s => s.nombre);
                    break;
            }
            
            return View( await cliente.AsNoTracking().ToListAsync());

          //  return View(await _context.Cliente.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.clienteID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }
        public  List<IdentityError> nuevoCliente(string clienteID, string nombre, string apellido, string correo, string telefono, string puntos)
        {
           return clienteModal.nuevoCliente(clienteID, nombre, apellido, correo, telefono, puntos); 
           
        }

            // GET: Clientes/Create
            public IActionResult Create()
        {
            ViewData["id"] = "falso";
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("clienteID,nombre,apellido,correo,telefono,imagen,puntos")] Cliente cliente)
        {
            ViewData["id"] = "falso";
            if (!(ClienteExists(cliente.clienteID)))
            {
                


                bool imagenNula = false;

                try
                {
                    if (cliente.imagen == null)
                    {
                        imagenNula = true;
                    }
                }
                catch (Exception e) { Console.WriteLine(e); }  //Verifica si ha subido o no una imagen a la hora de crear


                if (ModelState.IsValid && imagenNula)
                {
                    cliente.imagenN = "logoTaller.png";
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else if (ModelState.IsValid && cliente.imagen != null) //Modelo valido y si subio una imagen
                {
                    Imagen i = cliente.imagen;
                    string rootPath = hostEnvironment.WebRootPath;
                    string fileName = cliente.nombre;
                    fileName = fileName.Replace(" ", "");
                    string extension = Path.GetExtension(i.imageFile.FileName);
                    i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(rootPath + "/uploads/", fileName);
                    //cliente.imagen.imagePath = path;
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await i.imageFile.CopyToAsync(fileStream);
                    }
                    cliente.imagenN = fileName;
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewData["id"] = "verdad";
            }
                return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("clienteID,nombre,apellido,correo,telefono,imagen,imagenN,puntos")] Cliente cliente)
        {

            bool imagenNula = false;
            try
            {
                if (cliente.imagen == null)
                {
                    imagenNula = true;
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
            if (id != cliente.clienteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imagenNula)
                    {
                        _context.Update(cliente);
                        await _context.SaveChangesAsync();

                    }
                    else if (cliente.imagen != null)
                    {
                        Imagen i = cliente.imagen;
                        string rootPath = hostEnvironment.WebRootPath;
                        string fileName = cliente.clienteID;
                        fileName = fileName.Replace(" ", "");
                        string extension = Path.GetExtension(i.imageFile.FileName);
                        i.nombreImagen = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(rootPath + "/uploads/", fileName);
                        //cliente.imagen.imagePath = path;
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await i.imageFile.CopyToAsync(fileStream);
                        }
                        cliente.imagenN = fileName;

                        _context.Update(cliente);
                        await _context.SaveChangesAsync();

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.clienteID))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.clienteID == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(string id)
        {
            return _context.Cliente.Any(e => e.clienteID == id);
        }
        public async Task<List<Cliente>> VeniteCliente(string id)
        {
            var bombolbi = from owo in _context.Cliente select owo;
            bombolbi = bombolbi.Where(owo => owo.clienteID == id);
            return await bombolbi.ToListAsync();
        }
        public async Task<String> EliminarCliente(string id)
        {

            var respuesta = "";
            try
            {
                var bombolbi = await _context.Cliente.FindAsync(id);
                _context.Cliente.Remove(bombolbi);
                await _context.SaveChangesAsync();
                respuesta = "Delete";
            }
            catch
            {
                respuesta = "NoDelete";
            }
            return respuesta;
        }
    }
}
