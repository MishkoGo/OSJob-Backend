using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace aspnetserver.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    From = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    whom = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Text = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "PostId", "Date", "From", "Text", "" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7236), "From AI", "This is task 1 and do it, please.", "Manager" },
                    { 2, new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7266), "From AI", "This is task 2 and do it, please.", "Manager" },
                    { 3, new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7268), "From AI", "This is task 3 and do it, please.", "Manager" },
                    { 4, new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7270), "From AI", "This is task 4 and do it, please.", "Manager" },
                    { 5, new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7272), "From AI", "This is task 5 and do it, please.", "Manager" },
                    { 6, new DateTime(2023, 2, 28, 14, 49, 5, 944, DateTimeKind.Local).AddTicks(7273), "From AI", "This is task 6 and do it, please.", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
