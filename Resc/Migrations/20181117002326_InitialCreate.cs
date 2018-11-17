using Microsoft.EntityFrameworkCore.Migrations;

namespace Resc.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirstResponders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PushEndpoint = table.Column<string>(nullable: true),
                    PushAuth = table.Column<string>(nullable: true),
                    PushKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstResponders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interventions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Lat = table.Column<double>(nullable: false),
                    Lng = table.Column<double>(nullable: false),
                    Overview = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interventions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<int>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Lng = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivePositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstResponderId = table.Column<int>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Lng = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivePositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivePositions_FirstResponders_FirstResponderId",
                        column: x => x.FirstResponderId,
                        principalTable: "FirstResponders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FirstResponderInterventions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstResponderId = table.Column<int>(nullable: false),
                    InterventionId = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstResponderInterventions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirstResponderInterventions_FirstResponders_FirstResponderId",
                        column: x => x.FirstResponderId,
                        principalTable: "FirstResponders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FirstResponderInterventions_Interventions_InterventionId",
                        column: x => x.InterventionId,
                        principalTable: "Interventions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivePositions_FirstResponderId",
                table: "ActivePositions",
                column: "FirstResponderId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstResponderInterventions_FirstResponderId",
                table: "FirstResponderInterventions",
                column: "FirstResponderId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstResponderInterventions_InterventionId",
                table: "FirstResponderInterventions",
                column: "InterventionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivePositions");

            migrationBuilder.DropTable(
                name: "FirstResponderInterventions");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "FirstResponders");

            migrationBuilder.DropTable(
                name: "Interventions");
        }
    }
}
