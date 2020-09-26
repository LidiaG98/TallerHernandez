using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly TallerHernandezContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginModel(SignInManager<IdentityUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager, TallerHernandezContext context, RoleManager<IdentityRole> rolemanager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _roleManager = rolemanager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if(_context.Empleado.Any())
            {

            }
            else
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Superusuario",
                    NormalizedName = "Superusuario"
                };
                var result = await _roleManager.CreateAsync(role);
                var rol = new Rol[]
                {
                    new Rol{rolNom="Superusuario"},
                };
                foreach (Rol r in rol)
                {
                    _context.Add(r);
                }
                _context.SaveChanges();
                var empleado = new Empleado
                {
                    empleadoID = "000000000-0",
                    nombre = "Admin",
                    apellido = "Admin",
                    correo = "admin@mail.com",
                    telefono = "78945612",
                    salario = 0,
                    areaID = _context.Area.FirstOrDefault(r => r.areaNom == "Administración").AreaID,
                    rolID = _context.Rol.FirstOrDefault(r => r.rolNom == "Superusuario").rolID,
                    modopagoID = _context.ModoPago.FirstOrDefault(r => r.tipo == "Efectivo").modopagoID,
                    
                };
                _context.Empleado.Add(empleado);
                var usuario = new IdentityUser
                {
                    UserName = empleado.correo,
                    Email = empleado.correo
                };
                string pass = "Admin@123";
                result = await _userManager.CreateAsync(usuario, pass);
                usuario = await _userManager.FindByNameAsync(empleado.correo);
                await _userManager.AddToRoleAsync(usuario, "Superusuario");
            }
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
