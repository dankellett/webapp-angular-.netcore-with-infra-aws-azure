using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace hr_proto_vs.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<AlignmentEntry> Alignment { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<AlignmentEntry>()
            //    .Property(b => b.Url)
            //    .IsRequired();

            modelBuilder.Entity<AlignmentEntry>()
                .HasIndex(a => a.UserId);

            modelBuilder.Entity<UserOrgReport>()
                .HasIndex(u => new { u.UserId, u.ReportsToUserId })
                .IsUnique();
        }
        #endregion
    }

    public class AlignmentEntry
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public int AlignmentType { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }

    public class UserOrgReport
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ReportsToUserId { get; set; }
    }
}
