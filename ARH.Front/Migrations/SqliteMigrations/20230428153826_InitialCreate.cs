using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARH.Front.Migrations.SqliteMigrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyRecordCollection",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Day = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OnSite = table.Column<string>(type: "TEXT", nullable: false),
                    AtHome = table.Column<string>(type: "TEXT", nullable: false),
                    PayedVacation = table.Column<string>(type: "TEXT", nullable: false),
                    UnpayedVacation = table.Column<string>(type: "TEXT", nullable: false),
                    Sickness = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyRecordCollection", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyRecordCollection");
        }
    }
}
