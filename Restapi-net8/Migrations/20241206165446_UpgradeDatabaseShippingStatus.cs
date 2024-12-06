using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeDatabaseShippingStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_shipping_status_ShippingStatusId",
                table: "invoice");

            migrationBuilder.DropColumn(
                name: "shipping_status",
                table: "invoice");

            migrationBuilder.RenameColumn(
                name: "ShippingStatusId",
                table: "invoice",
                newName: "shipping_status_id");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_ShippingStatusId",
                table: "invoice",
                newName: "IX_invoice_shipping_status_id");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "shipping_status",
                type: "nvarchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "shipping_status",
                type: "nvarchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(300)");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_shipping_status_shipping_status_id",
                table: "invoice",
                column: "shipping_status_id",
                principalTable: "shipping_status",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_shipping_status_shipping_status_id",
                table: "invoice");

            migrationBuilder.RenameColumn(
                name: "shipping_status_id",
                table: "invoice",
                newName: "ShippingStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_invoice_shipping_status_id",
                table: "invoice",
                newName: "IX_invoice_ShippingStatusId");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "shipping_status",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "shipping_status",
                type: "varchar(300)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)");

            migrationBuilder.AddColumn<Guid>(
                name: "shipping_status",
                table: "invoice",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_shipping_status_ShippingStatusId",
                table: "invoice",
                column: "ShippingStatusId",
                principalTable: "shipping_status",
                principalColumn: "Id");
        }
    }
}
