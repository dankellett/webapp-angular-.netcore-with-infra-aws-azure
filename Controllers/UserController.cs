using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using app_template.Data;
using app_template.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app_template.Controllers
{
    [Authorize(Roles = Constants.Roles.GlobalAdmin)]
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ApplicationUserContext _userDbContext;
        private string _userId;
        private readonly ClaimsPrincipal _claimedUser;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext dbContext,
            ApplicationUserContext userDbContext,
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userDbContext = userDbContext;
            _claimedUser = contextAccessor.HttpContext.User;
            _userManager = userManager;
            _userId = userManager.GetUserId(_claimedUser);
        }

        [HttpGet()]
        [Route("/api/users")]
        public async Task<ActionResult<List<ApplicationUserDto>>> GetUsers()
        {
            var users  = await _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
            var userDtoList = new List<ApplicationUserDto>();
            foreach (var user in users)
            {
                userDtoList.Add(new ApplicationUserDto
                {
                    Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
                    UserName = user.UserName,
                    Manager = "test"
                });
            }

            //var userList = await _userDbContext.Users.Select(async u => new ApplicationUserDto
            //{
            //    UserName = u.UserName,
            //    Roles = u.roles (await _userManager.GetRolesAsync(u)).ToList()
            //}).ToListAsync();

            return userDtoList;
        }

        public class ApplicationUserDto
        {
            public string UserName { get; set; }
            public string Manager { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}