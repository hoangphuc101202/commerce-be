using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingStatusInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_status_status_id",
                table: "invoice");

            migrationBuilder.AlterColumn<Guid>(
                name: "status_id",
                table: "invoice",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "shipping_status",
                table: "invoice",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_status_status_id",
                table: "invoice",
                column: "status_id",
                principalTable: "status",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_status_status_id",
                table: "invoice");

            migrationBuilder.DropColumn(
                name: "shipping_status",
                table: "invoice");

            migrationBuilder.AlterColumn<Guid>(
                name: "status_id",
                table: "invoice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_status_status_id",
                table: "invoice",
                column: "status_id",
                principalTable: "status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
