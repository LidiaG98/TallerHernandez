using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TallerHernandez.Models;

namespace TallerHernandez.Data
{
    public class TallerHernandezContext : IdentityDbContext
    {
        public TallerHernandezContext (DbContextOptions<TallerHernandezContext> options)
            : base(options)
        {
        }

      public DbSet<TallerHernandez.Models.Area> Area { get; set; }

        public DbSet<TallerHernandez.Models.Automovil> Automovil { get; set; }

        public DbSet<TallerHernandez.Models.Cliente> Cliente { get; set; }

        public DbSet<TallerHernandez.Models.Empleado> Empleado { get; set; }

        public DbSet<TallerHernandez.Models.ModoPago> ModoPago { get; set; }

        public DbSet<TallerHernandez.Models.Rol> Rol { get; set; }
        public DbSet<TallerHernandez.Models.Recepcion> Recepcion { get; set; }
        public DbSet<TallerHernandez.Models.Procedimiento> Procedimiento { get; set; }
        public DbSet<TallerHernandez.Models.Mantenimiento> Mantenimiento { get; set; }
        public DbSet<TallerHernandez.Models.AsignacionTarea> AsignacionTarea { get; set; }
        public DbSet<TallerHernandez.Models.Repuesto> Repuesto { get; set; }
    }
}
