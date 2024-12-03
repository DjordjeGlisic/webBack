using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aprilskiB2024.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maraton",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Lokacija = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DuzinaStazeM = table.Column<int>(type: "int", nullable: false),
                    VremePocetkaTrke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VremeKrajaTrke = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maraton", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trkac",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    JMBG = table.Column<long>(type: "bigint", nullable: false),
                    Nagrade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trkac", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojMaratonca = table.Column<int>(type: "int", nullable: false),
                    Pozicija = table.Column<int>(type: "int", nullable: false),
                    PredjenoMetra = table.Column<float>(type: "real", nullable: false),
                    Progres = table.Column<int>(type: "int", nullable: false),
                    BrzinaTrkaca = table.Column<float>(type: "real", nullable: false),
                    MaratonacId = table.Column<int>(type: "int", nullable: false),
                    TakmicenjeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trka", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trka_Maraton_TakmicenjeId",
                        column: x => x.TakmicenjeId,
                        principalTable: "Maraton",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trka_Trkac_MaratonacId",
                        column: x => x.MaratonacId,
                        principalTable: "Trkac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trka_MaratonacId",
                table: "Trka",
                column: "MaratonacId");

            migrationBuilder.CreateIndex(
                name: "IX_Trka_TakmicenjeId",
                table: "Trka",
                column: "TakmicenjeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trka");

            migrationBuilder.DropTable(
                name: "Maraton");

            migrationBuilder.DropTable(
                name: "Trkac");
        }
    }
}
