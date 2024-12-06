using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class AddTableShippingStatusChangeForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"));

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"));

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4399"));

            migrationBuilder.AddColumn<Guid>(
                name: "ShippingStatusId",
                table: "invoice",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "shipping_status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "varchar(200)", nullable: false),
                    description = table.Column<string>(type: "varchar(300)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipping_status", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "shipping_status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"), "Đơn hàng đang được giao đi", false, "Đang giao hàng" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"), "Đơn hàng giao thành công", false, "Giao hàng thành công" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4399"), "Đơn hàng đã bị hủy", false, "Đã hủy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoice_detail_product_id",
                table: "invoice_detail",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_ShippingStatusId",
                table: "invoice",
                column: "ShippingStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_shipping_status_ShippingStatusId",
                table: "invoice",
                column: "ShippingStatusId",
                principalTable: "shipping_status",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_detail_product_product_id",
                table: "invoice_detail",
                column: "product_id",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_shipping_status_ShippingStatusId",
                table: "invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_detail_product_product_id",
                table: "invoice_detail");

            migrationBuilder.DropTable(
                name: "shipping_status");

            migrationBuilder.DropIndex(
                name: "IX_invoice_detail_product_id",
                table: "invoice_detail");

            migrationBuilder.DropIndex(
                name: "IX_invoice_ShippingStatusId",
                table: "invoice");

            migrationBuilder.DropColumn(
                name: "ShippingStatusId",
                table: "invoice");

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"), "Đơn hàng đang được giao đi", false, "Đang giao hàng" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"), "Đơn hàng giao thành công", false, "Giao hàng thành công" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4399"), "Đơn hàng đã bị hủy", false, "Đã hủy" }
                });
        }
    }
}
