using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TallerHernandez.Data;
using TallerHernandez.Models;
using TallerHernandez.ViewModels;

namespace TallerHernandez.Controllers
{
    public class RolsController : Controller
    {
        private readonly TallerHernandezContext _context;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolsController(TallerHernandezContext context, 
                              RoleManager<IdentityRole> rolemanager, 
                              UserManager<IdentityUser> userManager)
        {
            _context = context;
            _rolemanager = rolemanager;
            _userManager = userManager;
        }

        // GET: Rols
        public async Task<IActionResult> Index(string OrdenA,string Buscar)
        {            
            ViewData["OrdenNom"] = String.IsNullOrEmpty(OrdenA) ? "nom_desc" : "";

            ViewData["Filtro"] = Buscar;
            var rol = from s in _context.Roles select s;
            if (!String.IsNullOrEmpty(Buscar))
            {
                rol = rol.Where(s => s.Name.Contains(Buscar));
            }
            switch (OrdenA)
            {
                case "nom_desc":
                    rol = rol.OrderByDescending(s => s.Name);
                    break;


                default:
                    rol = rol.OrderBy(s => s.Name);
                    break;
            }

            return View(await rol.AsNoTracking().ToListAsync());
        }

        // GET: Rols/Details/5
        public async Task<IActionResult> Details(int? id)
        {            
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Rol
                .FirstOrDefaultAsync(m => m.rolID == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // GET: Rols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("rolID,rolNom")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = rol.rolNom
                };
                _context.Add(rol);
                IdentityResult result = await _rolemanager.CreateAsync(identityRole);
                //Recordatorio: podría poner los errores de crear roles con un foreach
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: Rols/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _rolemanager.FindByIdAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            var model = new EditRoleViewModel
            {
                Id = rol.Id,
                Name = rol.Name,
                
            };

            foreach(var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, rol.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);
        }

        // POST: Rols/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRoleViewModel rol)
        {
            var role = await _rolemanager.FindByIdAsync(rol.Id);
            if(role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = rol.Name;
                if (ModelState.IsValid)
                {
                    var result = await _rolemanager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }            
            return View(rol);
        }

        //// GET: Rols/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var rol = await _context.Rol
        //        .FirstOrDefaultAsync(m => m.rolID == id);
        //    if (rol == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(rol);
        //}

        //// POST: Rols/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var rol = await _context.Rol.FindAsync(id);
        //    _context.Rol.Remove(rol);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {
            var role = await _rolemanager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _rolemanager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("Index");
            }
        }

        private bool RolExists(int id)
        {
            return _context.Rol.Any(e => e.rolID == id);
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string rolId)
        {
            ViewBag.rolId = rolId;

            var rol = await _rolemanager.FindByIdAsync(rolId);

            if(rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con Id = {rolId} no existe";
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();
            foreach(var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserID = user.Id,
                    UserName = user.UserName
                };
                if(await _userManager.IsInRoleAsync(user, rol.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string rolId)
        {
            var rol = await _rolemanager.FindByIdAsync(rolId);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con Id = {rolId} no existe";
                return NotFound();
            }
            for(int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserID);
                IdentityResult result = null;

                if(model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, rol.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, rol.Name);
                }
                else if(!model[i].IsSelected && await _userManager.IsInRoleAsync(user, rol.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, rol.Name);
                }
                else
                {
                    continue;
                }

                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Edit", new { Id = rolId });
                }
            }

            return RedirectToAction("Edit", new { Id = rolId });

        }
    }
}
