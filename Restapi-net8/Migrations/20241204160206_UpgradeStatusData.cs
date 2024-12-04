using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeStatusData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"),
                column: "description",
                value: "Đơn hàng đang chờ xác nhận");

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"),
                column: "description",
                value: "Đơn hàng đang được giao");

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"),
                column: "description",
                value: "Đơn hàng đã được giao");

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[] { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"), "Đơn hàng đã bị hủy", false, "Đã hủy" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"));

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"),
                column: "description",
                value: null);

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"),
                column: "description",
                value: null);

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"),
                column: "description",
                value: null);
        }
    }
}
