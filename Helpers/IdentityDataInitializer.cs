using app_template.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_template
{
    public class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            SeedRoles(roleManager, configuration);
            SeedUsers(userManager, configuration);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            var gaUsername = configuration.GetValue<string>("ga_username");
            if (!String.IsNullOrEmpty(gaUsername)){
                EnsureUser(userManager,
                    gaUsername,
                    configuration.GetValue<string>("ga_pw"),
                    Constants.Roles.GlobalAdmin);
            }
        }

        public static void SeedRoles(RoleManager<ApplicationRole> roleManager, IConfiguration configuration)
        {
            var roles = GetRoleList();
            foreach (var role in roles)
            {
                EnsureRole(roleManager, role);
            }
        }

        /// <summary>
        /// Create a user if it doesn't already exists.
        /// NOTE: This isn't the most secure way to move passwords around
        /// </summary>
        /// <param name="userManager">The Core Identity user manager</param>
        /// <param name="userName">Username & email of user</param>
        /// <param name="password">The password for the user</param>
        /// <param name="role">The role name - pull from Constants.Roles</param>
        /// <returns></returns>
        private static bool EnsureUser(UserManager<ApplicationUser> userManager, string userName, string password, string role)
        {
            if (userManager.FindByNameAsync(userName).Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = userName;
                user.Email = userName;
                user.EmailConfirmed = true;
                IdentityResult result = userManager.CreateAsync(user, password).Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Retrive List of Roles using constant values
        /// </summary>
        /// <returns></returns>
        private static List<RoleListValue> GetRoleList()
        {
            var roleList = new List<RoleListValue>();
            roleList.Add(new RoleListValue()
            {
                RoleName = Constants.Roles.GlobalAdmin,
                RoleDescription = Constants.Roles.GlobalAdminDescription
            });
            roleList.Add(new RoleListValue()
            {
                RoleName = Constants.Roles.AccountAdmin,
                RoleDescription = Constants.Roles.AccountAdminDescription
            });
            roleList.Add(new RoleListValue()
            {
                RoleName = Constants.Roles.ReportViewer,
                RoleDescription = Constants.Roles.ReportViewerDescription
            });
            roleList.Add(new RoleListValue()
            {
                RoleName = Constants.Roles.Manager,
                RoleDescription = Constants.Roles.ManagerDescription
            });
            roleList.Add(new RoleListValue()
            {
                RoleName = Constants.Roles.Employee,
                RoleDescription = Constants.Roles.EmployeeDescription
            });
            return roleList;
        }

        /// <summary>
        /// Create a role if it does not already exist
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="roleValue"></param>
        /// <returns></returns>
        private static bool EnsureRole(RoleManager<ApplicationRole> roleManager, RoleListValue roleValue)
        {
            if (!roleManager.RoleExistsAsync(roleValue.RoleName).Result)
            {
                ApplicationRole role = new ApplicationRole();
                role.Name = roleValue.RoleName;
                role.Description = roleValue.RoleDescription;
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Helper class to build role list
        /// </summary>
        private class RoleListValue
        {
            public string RoleName { get; set; }
            public string RoleDescription { get; set; }
        }
    }
}
