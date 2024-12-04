using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbHasStatusAndUpgradeInvoiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_customer_CustomerId",
                table: "invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_status_StatusId",
                table: "invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_detail_invoice_InvoiceId",
                table: "invoice_detail");

            migrationBuilder.DropIndex(
                name: "IX_invoice_detail_InvoiceId",
                table: "invoice_detail");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "invoice_detail",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "InvoiceId",
                table: "invoice_detail",
                newName: "invoice_id");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "invoice",
                newName: "status_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "invoice",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_StatusId",
                table: "invoice",
                newName: "IX_invoice_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_CustomerId",
                table: "invoice",
                newName: "IX_invoice_customer_id");

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "Id", "description", "is_deleted", "name" },
                values: new object[,]
                {
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"), null, false, "Chờ xác nhận" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"), null, false, "Đơn hàng đang giao" },
                    { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"), null, false, "Đã giao hàng" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_customer_customer_id",
                table: "invoice",
                column: "customer_id",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_status_status_id",
                table: "invoice",
                column: "status_id",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_customer_customer_id",
                table: "invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_invoice_status_status_id",
                table: "invoice");

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"));

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4396"));

            migrationBuilder.DeleteData(
                table: "status",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4397"));

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "invoice_detail",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "invoice_id",
                table: "invoice_detail",
                newName: "InvoiceId");

            migrationBuilder.RenameColumn(
                name: "status_id",
                table: "invoice",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "invoice",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_status_id",
                table: "invoice",
                newName: "IX_invoice_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_customer_id",
                table: "invoice",
                newName: "IX_invoice_CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_invoice_detail_InvoiceId",
                table: "invoice_detail",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_customer_CustomerId",
                table: "invoice",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_status_StatusId",
                table: "invoice",
                column: "StatusId",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_detail_invoice_InvoiceId",
                table: "invoice_detail",
                column: "InvoiceId",
                principalTable: "invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
