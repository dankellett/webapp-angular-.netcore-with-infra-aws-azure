using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
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
        private readonly AppDbContext _dbContext;
        private string _userId;
        private readonly ClaimsPrincipal _claimedUser;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(
            AppDbContext dbContext,
            IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
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
                    ManagerId = "test"
                });
            }

            return userDtoList;
        }

        [HttpPost()]
        [Route("/api/user")]
        public async Task<ActionResult<ApplicationUserDto>> UpdateUser([FromBody] ApplicationUserDto userDto)
        {
            var userEntity = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserName);
            var userOrgReportEntity = await _dbContext.UserOrgReport.FirstOrDefaultAsync(u => u.UserId == userEntity.Id);

            if (userOrgReportEntity != null)
            {
                userOrgReportEntity.ReportsToUserId = userDto.ManagerId;
            } 
            else
            {
                userOrgReportEntity = new UserOrgReport
                {
                    ReportsToUserId = userDto.ManagerId,
                    UserId = userEntity.Id
                };
                await _dbContext.AddAsync<UserOrgReport>(userOrgReportEntity);
                await _dbContext.SaveChangesAsync();
            }

            return userDto;
        }


        public class ApplicationUserDto
        {
            public string UserName { get; set; }
            public string ManagerId { get; set; }
            public List<string> Roles { get; set; }
        }
    }
}