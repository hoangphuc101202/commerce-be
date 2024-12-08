using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class upgradestatustableseeddatacancle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "shipping_status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[] { new Guid("c62e3c10-5e07-427e-a55a-45cd301b5397"), "Đơn hàng đã hủy", false, "Đã hủy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shipping_status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b5397"));
        }
    }
}
