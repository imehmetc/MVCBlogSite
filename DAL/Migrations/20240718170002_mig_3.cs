using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostLikes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BirthDate", "CreatedDate" },
                values: new object[] { new DateTime(2024, 7, 18, 20, 0, 1, 663, DateTimeKind.Local).AddTicks(2242), new DateTime(2024, 7, 18, 20, 0, 1, 663, DateTimeKind.Local).AddTicks(2244) });

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_PostId",
                table: "PostLikes",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostLikes_UserId",
                table: "PostLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostLikes");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "BirthDate", "CreatedDate" },
                values: new object[] { new DateTime(2024, 7, 17, 14, 29, 25, 568, DateTimeKind.Local).AddTicks(4231), new DateTime(2024, 7, 17, 14, 29, 25, 568, DateTimeKind.Local).AddTicks(4233) });
        }
    }
}
