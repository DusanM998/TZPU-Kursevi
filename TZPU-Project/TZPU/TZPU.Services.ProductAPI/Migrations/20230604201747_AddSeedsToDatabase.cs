using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TZPU.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedsToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, ".NET", " Master the fundamentals of .NET programming and learn how to create .NET projects with this .NET Programming Certification Course. You’ll get introduced to the .NET space and coding with C#, including Visual Studio and WinForms, preparing you for a successful career.", "https://placehold.co/603x403", ".NET Course", 15.0 },
                    { 2, "Python", " Learn to Program and Analyze Data with Python. Develop programs to gather, clean, analyze, and visualize data.", "https://placehold.co/602x402", "Python Course", 13.99 },
                    { 3, "C++", " Learn C++ — a versatile programming language that’s important for developing software, games, databases, and more.", "https://placehold.co/601x401", "C++ Course", 10.99 },
                    { 4, "React", "Learn basics of React.", "https://placehold.co/600x400", "React Course", 15.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
