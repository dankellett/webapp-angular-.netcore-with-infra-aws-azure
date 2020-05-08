using app_template.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace app_template.Data
{
    public class AppDbContext: ApiAuthorizationDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        { }

        public DbSet<AlignmentEntry> Alignment { get; set; }
        public DbSet<UserOrgReport> UserOrgReport { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //Setup many-many FK relationships to support property mapping from Users to Roles
            //This functionality existed out of the box in previous versions of ASP.NET Core Identity
            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<AlignmentEntry>()
                .HasIndex(a => a.UserId);

            builder.Entity<UserOrgReport>()
                .HasIndex(u => new { u.UserId, u.ReportsToUserId })
                .IsUnique();
        }
    }
}
