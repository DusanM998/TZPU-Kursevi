using Microsoft.EntityFrameworkCore;
using TZPU.Services.OrderAPI.Models;

namespace TZPU.Services.OrderAPI.DbContexts
{
    public class AppDbContexts : DbContext
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
        {
            
        }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
