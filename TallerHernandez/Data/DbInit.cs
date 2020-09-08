using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Models;

namespace TallerHernandez.Data
{
    public class DbInit
    { 
        public static void Iniz(TallerHernandezContext context)
        {
            context.Database.EnsureCreated();

            if(context.Area.Any())
            {
                return;
            }
            else
            {

            }
            if (context.Automovil.Any())
            {
                return;
            }
            if (context.Cliente.Any())
            {
                return;
            }
            if (context.Empleado.Any())
            {
                return;
            }
            if (context.Rol.Any())
            {
                return;
            }
            else
            {
                var rol = new Rol[]
                {
                    new Rol{rolNom="Mecánico"},
                    new Rol{rolNom="Gerente"},
                    new Rol{rolNom="recepcionista"}
                };
                foreach(Rol r in rol)
                {
                    context.Add(r);
                }
                context.SaveChanges();
            }
            if (context.ModoPago.Any())
            {
                return;
            }
            else
            {
                var modoPago = new ModoPago[]
                    {
                    new ModoPago{tipo="Cheque"},
                    new ModoPago{tipo="Efectivo"},
                    new ModoPago{tipo="PayPal"} };
                    foreach(ModoPago Mp in modoPago)
                {
                    context.Add(Mp);
                   
                }
                context.SaveChanges();
            };
            
        }
    }
}
