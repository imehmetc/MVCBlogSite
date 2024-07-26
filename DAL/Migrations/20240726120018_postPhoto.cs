using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class postPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Posts",
                newName: "PhotoUrl");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BirthDate", "CreatedDate" },
                values: new object[] { new DateTime(2024, 7, 26, 15, 0, 17, 352, DateTimeKind.Local).AddTicks(2310), new DateTime(2024, 7, 26, 15, 0, 17, 352, DateTimeKind.Local).AddTicks(2314) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Posts",
                newName: "Photo");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BirthDate", "CreatedDate" },
                values: new object[] { new DateTime(2024, 7, 26, 14, 45, 22, 262, DateTimeKind.Local).AddTicks(3537), new DateTime(2024, 7, 26, 14, 45, 22, 262, DateTimeKind.Local).AddTicks(3539) });
        }
    }
}
