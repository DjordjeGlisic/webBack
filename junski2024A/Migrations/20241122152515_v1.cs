using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace junski2024A.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Jmbg = table.Column<long>(type: "bigint", nullable: false),
                    BrojVozacke = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivModela = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Automobil",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Godiste = table.Column<int>(type: "int", nullable: false),
                    Kilometraza = table.Column<long>(type: "bigint", nullable: false),
                    BrojSedista = table.Column<int>(type: "int", nullable: false),
                    CenaPoDanu = table.Column<float>(type: "real", nullable: false),
                    ModelAutomobilaID = table.Column<int>(type: "int", nullable: true),
                    KorisnikAutomobilaID = table.Column<int>(type: "int", nullable: true),
                    BrojDana = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobil", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Automobil_Korisnik_KorisnikAutomobilaID",
                        column: x => x.KorisnikAutomobilaID,
                        principalTable: "Korisnik",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Automobil_Model_ModelAutomobilaID",
                        column: x => x.ModelAutomobilaID,
                        principalTable: "Model",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobil_KorisnikAutomobilaID",
                table: "Automobil",
                column: "KorisnikAutomobilaID");

            migrationBuilder.CreateIndex(
                name: "IX_Automobil_ModelAutomobilaID",
                table: "Automobil",
                column: "ModelAutomobilaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobil");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Model");
        }
    }
}
