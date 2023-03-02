using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace A21API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmploiTemps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Annee_Scolaire = table.Column<string>(type: "longtext", nullable: true),
                    Nom_Ecole = table.Column<string>(type: "longtext", nullable: true),
                    Groupe = table.Column<int>(type: "int", nullable: true),
                    Locale = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploiTemps", x => x.ID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Enseignants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Prenom = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    NHeures = table.Column<int>(type: "int", nullable: false),
                    Cours = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignants", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CrenoHoraires",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    EnseignantID = table.Column<int>(type: "int", nullable: true),
                    Jours = table.Column<string>(type: "longtext", nullable: false),
                    Periode = table.Column<int>(type: "int", nullable: false),
                    EmploiTempsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrenoHoraires", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CrenoHoraires_EmploiTemps_EmploiTempsID",
                        column: x => x.EmploiTempsID,
                        principalTable: "EmploiTemps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrenoHoraires_Enseignants_EnseignantID",
                        column: x => x.EnseignantID,
                        principalTable: "Enseignants",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CrenoHoraires_EmploiTempsID",
                table: "CrenoHoraires",
                column: "EmploiTempsID");

            migrationBuilder.CreateIndex(
                name: "IX_CrenoHoraires_EnseignantID",
                table: "CrenoHoraires",
                column: "EnseignantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrenoHoraires");

            migrationBuilder.DropTable(
                name: "EmploiTemps");

            migrationBuilder.DropTable(
                name: "Enseignants");
        }
    }
}
