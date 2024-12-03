using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace septembarski2024.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stan",
                columns: table => new
                {
                    BrojStana = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeVlasnika = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Povrsina = table.Column<float>(type: "real", maxLength: 100, nullable: false),
                    BrojClanova = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stan", x => x.BrojStana);
                });

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojStana = table.Column<int>(type: "int", nullable: true),
                    MesecIzdavanja = table.Column<int>(type: "int", nullable: false),
                    Struja = table.Column<float>(type: "real", nullable: false),
                    Voda = table.Column<float>(type: "real", nullable: false),
                    Komunalije = table.Column<float>(type: "real", nullable: false),
                    Placen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racun", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Racun_Stan_BrojStana",
                        column: x => x.BrojStana,
                        principalTable: "Stan",
                        principalColumn: "BrojStana");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Racun_BrojStana",
                table: "Racun",
                column: "BrojStana");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Racun");

            migrationBuilder.DropTable(
                name: "Stan");
        }
    }
}
