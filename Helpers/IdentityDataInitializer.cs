using app_template.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_template
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager)
        {
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
        }

        public static void SeedRoles(RoleManager<ApplicationUser> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                //MyIdentityRole role = new MyIdentityRole();
                //role.Name = "NormalUser";
                //role.Description = "Perform normal operations.";
                //IdentityResult roleResult = roleManager.
                //CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
        ("Administrator").Result)
            {
                //MyIdentityRole role = new MyIdentityRole();
                //role.Name = "Administrator";
                //role.Description = "Perform all the operations.";
                //IdentityResult roleResult = roleManager.
                //CreateAsync(role).Result;
            }
        }
    }
}
