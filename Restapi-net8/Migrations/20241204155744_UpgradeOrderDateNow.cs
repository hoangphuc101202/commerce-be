using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeOrderDateNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cancle_date",
                table: "invoice",
                newName: "cancel_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cancel_date",
                table: "invoice",
                newName: "cancle_date");
        }
    }
}
