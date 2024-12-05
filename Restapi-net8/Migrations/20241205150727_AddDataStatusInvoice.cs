using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class AddDataStatusInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_CategoryId",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_supplier_SupplierId",
                table: "product");

            migrationBuilder.DropColumn(
                name: "status",
                table: "payment");

            migrationBuilder.RenameColumn(
                name: "SupplierId",
                table: "product",
                newName: "supplier_id");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "product",
                newName: "category_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_SupplierId",
                table: "product",
                newName: "IX_product_supplier_id");

            migrationBuilder.RenameIndex(
                name: "IX_product_CategoryId",
                table: "product",
                newName: "IX_product_category_id");

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đang chờ thanh toán", "Chờ thanh toán" });

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đã thanh toán", "Đơn hàng đã thanh toán" });

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đang được giao đi", "Đang giao hàng" });

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng giao thành công", "Giao hàng thành công" });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[] { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4399"), "Đơn hàng đã bị hủy", false, "Đã hủy" });

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_category_id",
                table: "product",
                column: "category_id",
                principalTable: "category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_supplier_supplier_id",
                table: "product",
                column: "supplier_id",
                principalTable: "supplier",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_product_category_category_id",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_supplier_supplier_id",
                table: "product");

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4399"));

            migrationBuilder.RenameColumn(
                name: "supplier_id",
                table: "product",
                newName: "SupplierId");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "product",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_product_supplier_id",
                table: "product",
                newName: "IX_product_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_product_category_id",
                table: "product",
                newName: "IX_product_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "payment",
                type: "varchar(200)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đang chờ xác nhận", "Chờ xác nhận" });

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đang được giao", "Đơn hàng đang giao" });

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đã được giao", "Đã giao hàng" });

            migrationBuilder.UpdateData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4398"),
                columns: new[] { "description", "name" },
                values: new object[] { "Đơn hàng đã bị hủy", "Đã hủy" });

            migrationBuilder.AddForeignKey(
                name: "FK_product_category_CategoryId",
                table: "product",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_supplier_SupplierId",
                table: "product",
                column: "SupplierId",
                principalTable: "supplier",
                principalColumn: "Id");
        }
    }
}
