using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerHernandez.Library
{
    public class LUserRoles
    {
        public List<SelectListItem> getRoles(RoleManager<IdentityRole> roleManager)
        {
            List<SelectListItem> selectLists = new List<SelectListItem>();
            var roles = roleManager.Roles.ToList();
            roles.ForEach(item =>
            {
                selectLists.Add(new SelectListItem
                {
                    Value = item.Id,
                    Text = item.Name
                });
            });
            return selectLists;
        }
    }
}
