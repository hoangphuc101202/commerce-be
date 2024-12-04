using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDiscountInvoiceDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_detail_product_ProductId",
                table: "invoice_detail");

            migrationBuilder.DropIndex(
                name: "IX_invoice_detail_ProductId",
                table: "invoice_detail");

            migrationBuilder.DropColumn(
                name: "discount",
                table: "invoice_detail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "discount",
                table: "invoice_detail",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_invoice_detail_ProductId",
                table: "invoice_detail",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_detail_product_ProductId",
                table: "invoice_detail",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
