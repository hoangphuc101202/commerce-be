using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeDatabaseShippingStatusSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shipping_status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"));

            migrationBuilder.DeleteData(
                table: "shipping_status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4399"));

            migrationBuilder.UpdateData(
                table: "shipping_status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đã giao hàng", "Đã giao hàng" });

            migrationBuilder.InsertData(
                table: "shipping_status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"), "Đơn hàng đang chờ xác nhận", false, "Chờ xác nhận" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"), "Đơn hàng đang giao hàng", false, "Đang giao hàng" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shipping_status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"));

            migrationBuilder.DeleteData(
                table: "shipping_status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"));

            migrationBuilder.UpdateData(
                table: "shipping_status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đang được giao đi", "Đang giao hàng" });

            migrationBuilder.InsertData(
                table: "shipping_status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"), "Đơn hàng giao thành công", false, "Giao hàng thành công" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4399"), "Đơn hàng đã bị hủy", false, "Đã hủy" }
                });
        }
    }
}
