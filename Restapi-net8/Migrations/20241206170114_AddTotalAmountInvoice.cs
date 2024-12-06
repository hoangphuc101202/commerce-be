using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalAmountInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "total_amount",
                table: "invoice",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_amount",
                table: "invoice");
        }
    }
}
