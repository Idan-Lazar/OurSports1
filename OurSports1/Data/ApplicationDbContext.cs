using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OurSports1.Models;

namespace OurSports1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OurSports1.Models.Article> Article { get; set; }

        public DbSet<OurSports1.Models.Author> Author { get; set; }

        public DbSet<OurSports1.Models.Comment> Comment { get; set; }

        public DbSet<OurSports1.Models.Category> Category { get; set; }
        public DbSet<OurSports1.Models.Stadiums> Stadiums { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
