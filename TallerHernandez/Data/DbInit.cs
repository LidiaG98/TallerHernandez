using Microsoft.AspNetCore.Identity;
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
           
            if (context.Area.Any())
            {
                return;
            }
            else
            {
                var area = new Area[]
                {
                    new Area{areaNom="Mecánica"},
                    new Area{areaNom="Administración"}                    
                };
                foreach (Area a in area)
                {
                    context.Add(a);
                }
                context.SaveChanges();
            }            
            if (context.Automovil.Any())
            {
                return;
            }
            if (context.Cliente.Any())
            {
                return;
            }
            if (context.Rol.Any())
            {
                return;
            }
            else
            {             
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
                    new ModoPago{tipo="PayPal"} 
                    };
                    foreach(ModoPago Mp in modoPago)
                {
                    context.Add(Mp);
                   
                }
                context.SaveChanges();                
            }           
            if (context.Procedimiento.Any())
            {
                return;
            }
            else
            {
                var procedimiento = new Procedimiento[]
                {
                    new Procedimiento{procedimiento="Procedimiento 1",areaID=1},
                    new Procedimiento{procedimiento="Procedimiento 2",areaID=2}
                };
                foreach (Procedimiento p in procedimiento)
                {
                    context.Add(p);
                }
                context.SaveChanges();
            }
            if (context.Empleado.Any())
            {
                return;
            }
            else
            {               
            }
        }
    }
}
