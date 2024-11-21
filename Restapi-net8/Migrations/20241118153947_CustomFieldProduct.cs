using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class CustomFieldProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_detail_Products_ProductId",
                table: "invoice_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_category_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_supplier_SupplierId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "product");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SupplierId",
                table: "product",
                newName: "IX_product_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "product",
                newName: "IX_product_CategoryId");

            migrationBuilder.AlterColumn<long>(
                name: "views",
                table: "product",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "product",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "product",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "product",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_product",
                table: "product",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_detail_product_ProductId",
                table: "invoice_detail",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_detail_product_ProductId",
                table: "invoice_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_product_category_CategoryId",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_product_supplier_SupplierId",
                table: "product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_product",
                table: "product");

            migrationBuilder.RenameTable(
                name: "product",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_product_SupplierId",
                table: "Products",
                newName: "IX_Products_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_product_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AlterColumn<long>(
                name: "views",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SupplierId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_detail_Products_ProductId",
                table: "invoice_detail",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_supplier_SupplierId",
                table: "Products",
                column: "SupplierId",
                principalTable: "supplier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
