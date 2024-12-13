using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_payment_invoice_invoice_id",
                table: "payment");

            migrationBuilder.DropIndex(
                name: "IX_payment_invoice_id",
                table: "payment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_payment_invoice_id",
                table: "payment",
                column: "invoice_id");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_invoice_invoice_id",
                table: "payment",
                column: "invoice_id",
                principalTable: "invoice",
                principalColumn: "Id");
        }
    }
}
