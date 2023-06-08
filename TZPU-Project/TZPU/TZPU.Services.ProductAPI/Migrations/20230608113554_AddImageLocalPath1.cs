using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZPU.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddImageLocalPath1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLocalPathUrl",
                table: "Products",
                newName: "ImageLocalPath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLocalPath",
                table: "Products",
                newName: "ImageLocalPathUrl");
        }
    }
}
