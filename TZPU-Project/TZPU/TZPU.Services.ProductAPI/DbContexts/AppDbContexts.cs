using Microsoft.EntityFrameworkCore;
using TZPU.Services.ProductAPI.Models;

namespace TZPU.Services.ProductAPI.DbContexts
{
    public class AppDbContexts : DbContext
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = ".NET Course",
                Price = 15,
                Description = " Master the fundamentals of .NET programming and learn how to create .NET projects with this .NET Programming Certification Course. You’ll get introduced to the .NET space and coding with C#, including Visual Studio and WinForms, preparing you for a successful career.",
                ImageUrl = "https://dusanm.blob.core.windows.net/tzpu/11.jpg",
                CategoryName = ".NET"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Python Course",
                Price = 13.99,
                Description = " Learn to Program and Analyze Data with Python. Develop programs to gather, clean, analyze, and visualize data.",
                ImageUrl = "https://dusanm.blob.core.windows.net/tzpu/12.jpg",
                CategoryName = "Python"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "C++ Course",
                Price = 10.99,
                Description = " Learn C++ — a versatile programming language that’s important for developing software, games, databases, and more.",
                ImageUrl = "https://dusanm.blob.core.windows.net/tzpu/13.jpg",
                CategoryName = "C++"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "React Course",
                Price = 15,
                Description = "Learn basics of React.",
                ImageUrl = "https://dusanm.blob.core.windows.net/tzpu/14.jpg",
                CategoryName = "React"
            });
        }
    }
}
