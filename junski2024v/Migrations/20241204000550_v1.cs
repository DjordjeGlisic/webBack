using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace New_folder.Migrations
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BrojLicneKarte = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresotaloMesta = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Znamenitost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeZnamenitosti = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Cena = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Znamenitost", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisnikTura",
                columns: table => new
                {
                    RezervacijeId = table.Column<int>(type: "int", nullable: false),
                    RezervisaniId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisnikTura", x => new { x.RezervacijeId, x.RezervisaniId });
                    table.ForeignKey(
                        name: "FK_KorisnikTura_Korisnik_RezervisaniId",
                        column: x => x.RezervisaniId,
                        principalTable: "Korisnik",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisnikTura_Tura_RezervacijeId",
                        column: x => x.RezervacijeId,
                        principalTable: "Tura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TuraZnamenitost",
                columns: table => new
                {
                    PripadaTuramaId = table.Column<int>(type: "int", nullable: false),
                    ZnamenitostiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuraZnamenitost", x => new { x.PripadaTuramaId, x.ZnamenitostiId });
                    table.ForeignKey(
                        name: "FK_TuraZnamenitost_Tura_PripadaTuramaId",
                        column: x => x.PripadaTuramaId,
                        principalTable: "Tura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TuraZnamenitost_Znamenitost_ZnamenitostiId",
                        column: x => x.ZnamenitostiId,
                        principalTable: "Znamenitost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisnikTura_RezervisaniId",
                table: "KorisnikTura",
                column: "RezervisaniId");

            migrationBuilder.CreateIndex(
                name: "IX_TuraZnamenitost_ZnamenitostiId",
                table: "TuraZnamenitost",
                column: "ZnamenitostiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisnikTura");

            migrationBuilder.DropTable(
                name: "TuraZnamenitost");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Tura");

            migrationBuilder.DropTable(
                name: "Znamenitost");
        }
    }
}
