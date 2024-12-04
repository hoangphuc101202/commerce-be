using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class AddTablePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    payment_method = table.Column<string>(type: "varchar(200)", nullable: true),
                    status = table.Column<string>(type: "varchar(200)", nullable: true),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    invoice_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoice_detail_invoice_id",
                table: "invoice_detail",
                column: "invoice_id");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_detail_invoice_invoice_id",
                table: "invoice_detail",
                column: "invoice_id",
                principalTable: "invoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_detail_invoice_invoice_id",
                table: "invoice_detail");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropIndex(
                name: "IX_invoice_detail_invoice_id",
                table: "invoice_detail");
        }
    }
}
