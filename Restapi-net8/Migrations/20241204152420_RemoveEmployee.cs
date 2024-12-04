using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_invoice_employee_EmployeeId",
                table: "invoice");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropIndex(
                name: "IX_invoice_EmployeeId",
                table: "invoice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    password = table.Column<string>(type: "varchar(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoice_EmployeeId",
                table: "invoice",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_invoice_employee_EmployeeId",
                table: "invoice",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
