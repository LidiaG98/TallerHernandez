using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.ModelModal
{
    public class ProcedimientoModels
    {
        private TallerHernandezContext context;
        public ProcedimientoModels(TallerHernandezContext context)
        {
            this.context = context;
        }
        public List<IdentityError> agregarProcedimiento(string procedimiento, string precio, string areaID)
        {
            var errorList = new List<IdentityError>();
            var procedimeinto = new Procedimiento
            {
                procedimiento = procedimiento,
                precio = float.Parse(precio, CultureInfo.InvariantCulture),
                areaID = Convert.ToInt32(areaID)
            };
            context.Procedimiento.Add(procedimeinto);
            //context.Mantenimiento.FromSqlRaw("INSERT INTO MANTENIMIENTO ");
            context.SaveChanges();
            errorList.Add(new IdentityError
            {
                Code = "SaveProc",
                Description = "Save"
            });
            return errorList;
        }
    }
}

