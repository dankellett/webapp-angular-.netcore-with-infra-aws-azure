using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace hr_proto_vs.Migrations
{
    public partial class AddAlignmentEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alignment",
                columns: table => new
                {
                    AlignmentEntryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    AlignmentType = table.Column<int>(nullable: false),
                    X = table.Column<float>(nullable: false),
                    Y = table.Column<float>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alignment", x => x.AlignmentEntryID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alignment_UserId",
                table: "Alignment",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alignment");
        }
    }
}
