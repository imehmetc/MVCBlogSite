using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class viewCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BirthDate", "CreatedDate" },
                values: new object[] { new DateTime(2024, 7, 28, 13, 22, 35, 897, DateTimeKind.Local).AddTicks(9747), new DateTime(2024, 7, 28, 13, 22, 35, 897, DateTimeKind.Local).AddTicks(9748) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Posts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BirthDate", "CreatedDate" },
                values: new object[] { new DateTime(2024, 7, 26, 15, 0, 17, 352, DateTimeKind.Local).AddTicks(2310), new DateTime(2024, 7, 26, 15, 0, 17, 352, DateTimeKind.Local).AddTicks(2314) });
        }
    }
}
