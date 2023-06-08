using Microsoft.EntityFrameworkCore;
using TZPU.Services.ShoppingCardAPI.Models;

namespace TZPU.Services.ShoppingCardAPI.DbContexts
{
    public class AppDbContexts : DbContext
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
        {
            
        }

        public DbSet<CardHeader> CardHeaders { get; set; }
        public DbSet<CardDetails> CardDetails { get; set; }
    }
}
