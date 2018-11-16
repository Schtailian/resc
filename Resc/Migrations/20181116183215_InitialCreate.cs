using Microsoft.EntityFrameworkCore.Migrations;

namespace Resc.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirstResponder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstResponder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivePositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstResponderId = table.Column<int>(nullable: false),
                    Lat = table.Column<decimal>(nullable: false),
                    Lng = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivePositions_FirstResponder_FirstResponderId",
                        column: x => x.FirstResponderId,
                        principalTable: "FirstResponder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivePositions_FirstResponderId",
                table: "ActivePositions",
                column: "FirstResponderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivePositions");

            migrationBuilder.DropTable(
                name: "FirstResponder");
        }
    }
}
