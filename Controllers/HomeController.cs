﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TallerHernandez.Models;

namespace TallerHernandez.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //IServiceProvider _serviceProvider;
        //public HomeController (IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}

        public IActionResult Index()
        {            
            return View();
        }

        //public async Task<IActionResult> Index()
        //{
        //    await CreateRolesAsync(_serviceProvider);
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Gerente", "Mecánico", "Administracion" };
            foreach (var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(item));
                }
            }
        }
    }
}
