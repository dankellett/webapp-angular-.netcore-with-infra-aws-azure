using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace app_template.Migrations.ApplicationDb
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alignment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    AlignmentType = table.Column<int>(nullable: false),
                    X = table.Column<float>(nullable: false),
                    Y = table.Column<float>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alignment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOrgReport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    ReportsToUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrgReport", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alignment_UserId",
                table: "Alignment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrgReport_UserId_ReportsToUserId",
                table: "UserOrgReport",
                columns: new[] { "UserId", "ReportsToUserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL AND [ReportsToUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alignment");

            migrationBuilder.DropTable(
                name: "UserOrgReport");
        }
    }
}
