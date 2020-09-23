using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.ModelModal
{
    public class MantenimientoModels
    {
        private TallerHernandezContext context;
        public MantenimientoModels(TallerHernandezContext context)
        {
            this.context = context;
        }
        public List<IdentityError> agregarMantenimiento (string nombre, string precio, string areaID)
        {
            var errorList = new List<IdentityError>();            
            var mantenimiento = new Mantenimiento {
                nombre = nombre,
                precio = float.Parse(precio,CultureInfo.InvariantCulture),
                areaID = Convert.ToInt32(areaID)
            };
            context.Mantenimiento.Add(mantenimiento);
            //context.Mantenimiento.FromSqlRaw("INSERT INTO MANTENIMIENTO ");
            context.SaveChanges();            
            errorList.Add(new IdentityError {
                Code = "Save",
                Description = "Save"
            });
            return errorList;
        }
    }
}
