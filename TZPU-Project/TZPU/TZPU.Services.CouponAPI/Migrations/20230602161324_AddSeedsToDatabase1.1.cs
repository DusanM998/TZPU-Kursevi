using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TZPU.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedsToDatabase11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 3,
                columns: new[] { "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { "EXTRA_OFFER_90OFF", 20.0, 40 });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { 4, "30OFF", 10.0, 20 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 3,
                columns: new[] { "CouponCode", "DiscountAmount", "MinAmount" },
                values: new object[] { "EXTRA_OFFER_100OFF", 10.0, 20 });
        }
    }
}
