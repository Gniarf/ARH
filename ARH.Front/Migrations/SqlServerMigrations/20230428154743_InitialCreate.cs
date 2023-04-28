using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ARH.Front.Migrations.SqlServerMigrations
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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OnSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtHome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PayedVacation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnpayedVacation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sickness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
