using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TZPU.Services.AuthAPI.Models;

namespace TZPU.Services.AuthAPI.Data
{
    public class AppDbContexts : IdentityDbContext<ApplicationUser>
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
