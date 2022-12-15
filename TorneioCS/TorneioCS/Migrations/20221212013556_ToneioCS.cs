using Microsoft.EntityFrameworkCore.Migrations;

namespace TorneioCS.Migrations
{
    public partial class ToneioCS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Competidores",
                columns: table => new
                {
                    idCompetidor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    TotalKill = table.Column<int>(type: "int", nullable: false),
                    TotalPartidas = table.Column<int>(type: "int", nullable: false),
                    Vitorias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competidores", x => x.idCompetidor);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competidores");
        }
    }
}
