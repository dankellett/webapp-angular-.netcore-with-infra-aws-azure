﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app_template.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;
using app_template.Models;

namespace app_template.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AlignmentEntryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private string _userId;
        private readonly ClaimsPrincipal _claimedUser;
        private readonly UserManager<ApplicationUser> _userManager;

        public AlignmentEntryController(ApplicationDbContext context, IHttpContextAccessor contextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _claimedUser = contextAccessor.HttpContext.User;            
            _userManager = userManager;
            _userId = userManager.GetUserId(_claimedUser);
        }

        // GET: api/AlignmentEntry
        [HttpGet()]
        public async Task<ActionResult<AlignmentEntry>> GetAlignmentEntry()
        {
            //Example of testing for user role inside action, this can also be done via the Auth attribute.
            var user = await _userManager.GetUserAsync(_claimedUser);
            var isAdmin = await _userManager.IsInRoleAsync(user, Constants.Roles.GlobalAdmin);

            var alignmentEntry = await _context.Alignment.Where(a => a.UserId == this._userId).OrderByDescending(b=>b.Id).FirstAsync();

            if (alignmentEntry == null)
            {
                return NotFound();
            }

            return alignmentEntry;
        }

        // PUT: api/AlignmentEntry/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlignmentEntry(int id, AlignmentEntry alignmentEntry)
        {
            if (id != alignmentEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(alignmentEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlignmentEntryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AlignmentEntry
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AlignmentEntry>> PostAlignmentEntry([FromBody] AlignmentEntryDto alignmentEntryDto)
        {
            var alignmentEntry = new AlignmentEntry()
            {
                UserId = _userId,
                X = alignmentEntryDto.x,
                Y = alignmentEntryDto.y,
                AlignmentType = 0
            };

            _context.Alignment.Add(alignmentEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlignmentEntry", new { id = alignmentEntry.Id }, alignmentEntry);
        }

        private bool AlignmentEntryExists(int id)
        {
            return _context.Alignment.Any(e => e.Id == id);
        }

        public class AlignmentEntryDto
        {
            public float x { get; set; }
            public float y { get; set; }
        }
    }
}
