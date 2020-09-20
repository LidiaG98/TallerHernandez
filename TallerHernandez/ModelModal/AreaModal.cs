using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerHernandez.Data;
using TallerHernandez.Models;

namespace TallerHernandez.ModelModal
{
    public class AreaModal
    {
        private TallerHernandezContext context; 
        public AreaModal(TallerHernandezContext context)
        {
            this.context = context;
        }
        public List<IdentityError> nuevoArea(string areaNom)
        {
            var errorList = new List<IdentityError>();
            
            var area = new Area
            {   AreaID = default,
                areaNom = areaNom
            };
            context.Add(area);
            context.SaveChangesAsync();
            errorList.Add(new IdentityError
            {
                Code = "Save",
                Description = "Se guardó, que bien"
            });
            return errorList;

        }
    }
}
