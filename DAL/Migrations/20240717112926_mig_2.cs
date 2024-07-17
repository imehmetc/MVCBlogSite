using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "CreatedDate", "DeletedDate", "FirstName", "IsAdmin", "IsDeleted", "LastName", "ModifiedDate", "Password", "Photo", "UserName" },
                values: new object[] { 10, new DateTime(2024, 7, 17, 14, 29, 25, 568, DateTimeKind.Local).AddTicks(4231), new DateTime(2024, 7, 17, 14, 29, 25, 568, DateTimeKind.Local).AddTicks(4233), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ali", true, false, "Veli", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "123", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
