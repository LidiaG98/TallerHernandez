using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TallerHernandez.Data;
using TallerHernandez.ViewModels;

namespace TallerHernandez.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TallerHernandezContext _context;

        public UsuariosController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, TallerHernandezContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new List<UsuarioTrabajadorViewModel>();            
            var users = _userManager.Users.ToArray();
            var empleados = _context.Empleado.ToArray();
            for(int i = 0; i < users.Length; i++)
            {
                var m = new UsuarioTrabajadorViewModel
                {
                    user = users[i],
                    empleado = empleados[i]
                };
                model.Add(m);
            }            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                ViewBag.ErrorMessage =  "No se encontró el usuario";
                return NotFound();
            }

            var rols = await _userManager.GetRolesAsync(user);

            var model = new EditUsuarioViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = rols
            };



            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUsuarioViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;                

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

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

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string? userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"No se encontró el usuario con el id {userId}";
                return NotFound();
            }
            var model = new List<UserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.IsSelected = true;
                }
                else
                {
                    userRolesViewModel.IsSelected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);                
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string? userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user,
                model.Where(x => x.IsSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("Edit", new { Id = userId });
        }

    }
}
