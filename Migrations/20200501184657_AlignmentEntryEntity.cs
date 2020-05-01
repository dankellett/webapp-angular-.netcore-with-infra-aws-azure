using Microsoft.EntityFrameworkCore.Migrations;

namespace hr_proto_vs.Migrations
{
    public partial class AlignmentEntryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Alignment",
                table: "Alignment");

            migrationBuilder.DropColumn(
                name: "AlignmentEntryID",
                table: "Alignment");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Alignment",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alignment",
                table: "Alignment",
                column: "Id");

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
                name: "IX_UserOrgReport_UserId_ReportsToUserId",
                table: "UserOrgReport",
                columns: new[] { "UserId", "ReportsToUserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL AND [ReportsToUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOrgReport");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alignment",
                table: "Alignment");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Alignment");

            migrationBuilder.AddColumn<int>(
                name: "AlignmentEntryID",
                table: "Alignment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alignment",
                table: "Alignment",
                column: "AlignmentEntryID");
        }
    }
}
