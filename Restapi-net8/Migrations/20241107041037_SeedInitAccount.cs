using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restapi_net8.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "customer",
                columns: new[] { "Id", "address", "birth_of_date", "email", "full_name", "gender", "image_url", "is_active", "is_deleted", "password", "phone", "role" },
                values: new object[] { new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"), null, null, "admin@gmail.com", "Admin", "male", null, true, false, "GaNodgXEDqXtPGbzdu6NxA==;u3PPeju79362KCDvOP9UVBN7DwrVjFb/xLsYlf+QVqU=", null, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "customer",
                keyColumn: "Id",
                keyValue: new Guid("c62e3c10-5e07-427e-a55a-45cd301b4395"));
        }
    }
}
