using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace store.api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "persone",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codice_fiscale = table.Column<string>(type: "varchar(16)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cognome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sesso = table.Column<string>(type: "varchar(1)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_nascita = table.Column<DateOnly>(type: "date", nullable: false),
                    citta = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    provincia = table.Column<string>(type: "varchar(5)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codice_postale = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    indirizzo = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero_contatto = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persone", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "prodotti",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sku = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    qnt = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prodotti", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clienti",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    person_id = table.Column<int>(type: "int", nullable: false),
                    codice_cliente = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_fidelizzato = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_iscritto_newsletter = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clienti", x => x.id);
                    table.ForeignKey(
                        name: "FK_clienti_persone_person_id",
                        column: x => x.person_id,
                        principalTable: "persone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dipendenti",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    person_id = table.Column<int>(type: "int", nullable: false),
                    codice_meccanografico = table.Column<string>(type: "varchar(10)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email_aziendale = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(24)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ruolo = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dipendenti", x => x.id);
                    table.ForeignKey(
                        name: "FK_dipendenti_persone_person_id",
                        column: x => x.person_id,
                        principalTable: "persone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "persone",
                columns: new[] { "id", "citta", "codice_fiscale", "codice_postale", "cognome", "created_at", "data_nascita", "email", "indirizzo", "is_deleted", "modified_at", "nome", "numero_contatto", "provincia", "sesso" },
                values: new object[,]
                {
                    { 1, "Roma", "0000000000000001", "00100", "Cognome1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 1, 1), "persona1@example.com", "Via delle Prove 1", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome1", "3330000001", "RM", "F" },
                    { 2, "Roma", "0000000000000002", "00100", "Cognome2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 2, 1), "persona2@example.com", "Via delle Prove 2", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome2", "3330000002", "RM", "M" },
                    { 3, "Roma", "0000000000000003", "00100", "Cognome3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 3, 1), "persona3@example.com", "Via delle Prove 3", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome3", "3330000003", "RM", "F" },
                    { 4, "Roma", "0000000000000004", "00100", "Cognome4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 4, 1), "persona4@example.com", "Via delle Prove 4", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome4", "3330000004", "RM", "M" },
                    { 5, "Roma", "0000000000000005", "00100", "Cognome5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 5, 1), "persona5@example.com", "Via delle Prove 5", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome5", "3330000005", "RM", "F" },
                    { 6, "Roma", "0000000000000006", "00100", "Cognome6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 6, 1), "persona6@example.com", "Via delle Prove 6", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome6", "3330000006", "RM", "M" },
                    { 7, "Roma", "0000000000000007", "00100", "Cognome7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 7, 1), "persona7@example.com", "Via delle Prove 7", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome7", "3330000007", "RM", "F" },
                    { 8, "Roma", "0000000000000008", "00100", "Cognome8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 8, 1), "persona8@example.com", "Via delle Prove 8", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome8", "3330000008", "RM", "M" },
                    { 9, "Roma", "0000000000000009", "00100", "Cognome9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 9, 1), "persona9@example.com", "Via delle Prove 9", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome9", "3330000009", "RM", "F" },
                    { 10, "Roma", "0000000000000010", "00100", "Cognome10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 10, 1), "persona10@example.com", "Via delle Prove 10", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome10", "3330000010", "RM", "M" },
                    { 11, "Roma", "0000000000000011", "00100", "Cognome11", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 11, 1), "persona11@example.com", "Via delle Prove 11", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome11", "3330000011", "RM", "F" },
                    { 12, "Roma", "0000000000000012", "00100", "Cognome12", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1980, 12, 1), "persona12@example.com", "Via delle Prove 12", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome12", "3330000012", "RM", "M" },
                    { 13, "Roma", "0000000000000013", "00100", "Cognome13", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 1, 1), "persona13@example.com", "Via delle Prove 13", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome13", "3330000013", "RM", "F" },
                    { 14, "Roma", "0000000000000014", "00100", "Cognome14", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 2, 1), "persona14@example.com", "Via delle Prove 14", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome14", "3330000014", "RM", "M" },
                    { 15, "Roma", "0000000000000015", "00100", "Cognome15", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 3, 1), "persona15@example.com", "Via delle Prove 15", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome15", "3330000015", "RM", "F" },
                    { 16, "Roma", "0000000000000016", "00100", "Cognome16", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 4, 1), "persona16@example.com", "Via delle Prove 16", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome16", "3330000016", "RM", "M" },
                    { 17, "Roma", "0000000000000017", "00100", "Cognome17", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 5, 1), "persona17@example.com", "Via delle Prove 17", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome17", "3330000017", "RM", "F" },
                    { 18, "Roma", "0000000000000018", "00100", "Cognome18", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 6, 1), "persona18@example.com", "Via delle Prove 18", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome18", "3330000018", "RM", "M" },
                    { 19, "Roma", "0000000000000019", "00100", "Cognome19", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 7, 1), "persona19@example.com", "Via delle Prove 19", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome19", "3330000019", "RM", "F" },
                    { 20, "Roma", "0000000000000020", "00100", "Cognome20", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 8, 1), "persona20@example.com", "Via delle Prove 20", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome20", "3330000020", "RM", "M" },
                    { 21, "Roma", "0000000000000021", "00100", "Cognome21", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 9, 1), "persona21@example.com", "Via delle Prove 21", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome21", "3330000021", "RM", "F" },
                    { 22, "Roma", "0000000000000022", "00100", "Cognome22", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 10, 1), "persona22@example.com", "Via delle Prove 22", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome22", "3330000022", "RM", "M" },
                    { 23, "Roma", "0000000000000023", "00100", "Cognome23", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 11, 1), "persona23@example.com", "Via delle Prove 23", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome23", "3330000023", "RM", "F" },
                    { 24, "Roma", "0000000000000024", "00100", "Cognome24", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1981, 12, 1), "persona24@example.com", "Via delle Prove 24", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome24", "3330000024", "RM", "M" },
                    { 25, "Roma", "0000000000000025", "00100", "Cognome25", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1982, 1, 1), "persona25@example.com", "Via delle Prove 25", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome25", "3330000025", "RM", "F" },
                    { 26, "Roma", "0000000000000026", "00100", "Cognome26", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1982, 2, 1), "persona26@example.com", "Via delle Prove 26", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome26", "3330000026", "RM", "M" },
                    { 27, "Roma", "0000000000000027", "00100", "Cognome27", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1982, 3, 1), "persona27@example.com", "Via delle Prove 27", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome27", "3330000027", "RM", "F" },
                    { 28, "Roma", "0000000000000028", "00100", "Cognome28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1982, 4, 1), "persona28@example.com", "Via delle Prove 28", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome28", "3330000028", "RM", "M" },
                    { 29, "Roma", "0000000000000029", "00100", "Cognome29", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1982, 5, 1), "persona29@example.com", "Via delle Prove 29", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome29", "3330000029", "RM", "F" },
                    { 30, "Roma", "0000000000000030", "00100", "Cognome30", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(1982, 6, 1), "persona30@example.com", "Via delle Prove 30", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nome30", "3330000030", "RM", "M" }
                });

            migrationBuilder.InsertData(
                table: "prodotti",
                columns: new[] { "id", "created_at", "is_deleted", "modified_at", "nome", "qnt", "sku" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 1", 10, "SKU-00001" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 2", 20, "SKU-00002" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 3", 30, "SKU-00003" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 4", 40, "SKU-00004" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 5", 50, "SKU-00005" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 6", 60, "SKU-00006" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 7", 70, "SKU-00007" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 8", 80, "SKU-00008" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 9", 90, "SKU-00009" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 10", 100, "SKU-00010" },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 11", 110, "SKU-00011" },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 12", 120, "SKU-00012" },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 13", 130, "SKU-00013" },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 14", 140, "SKU-00014" },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 15", 150, "SKU-00015" },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 16", 160, "SKU-00016" },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 17", 170, "SKU-00017" },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 18", 180, "SKU-00018" },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 19", 190, "SKU-00019" },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 20", 200, "SKU-00020" }
                });

            migrationBuilder.InsertData(
                table: "clienti",
                columns: new[] { "id", "codice_cliente", "created_at", "is_deleted", "is_fidelizzato", "is_iscritto_newsletter", "modified_at", "person_id" },
                values: new object[,]
                {
                    { 1, "00000000000000000001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11 },
                    { 2, "00000000000000000002", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12 },
                    { 3, "00000000000000000003", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13 },
                    { 4, "00000000000000000004", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14 },
                    { 5, "00000000000000000005", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15 },
                    { 6, "00000000000000000006", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16 },
                    { 7, "00000000000000000007", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17 },
                    { 8, "00000000000000000008", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18 },
                    { 9, "00000000000000000009", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19 },
                    { 10, "00000000000000000010", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20 }
                });

            migrationBuilder.InsertData(
                table: "dipendenti",
                columns: new[] { "id", "codice_meccanografico", "created_at", "email_aziendale", "is_deleted", "modified_at", "password", "person_id", "ruolo", "salario" },
                values: new object[,]
                {
                    { 1, "0000000001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 1, "User", 1600.00m },
                    { 2, "0000000002", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 2, "Admin", 1700.00m },
                    { 3, "0000000003", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 3, "User", 1800.00m },
                    { 4, "0000000004", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 4, "Admin", 1900.00m },
                    { 5, "0000000005", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 5, "User", 2000.00m },
                    { 6, "0000000006", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 6, "Admin", 2100.00m },
                    { 7, "0000000007", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 7, "User", 2200.00m },
                    { 8, "0000000008", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 8, "Admin", 2300.00m },
                    { 9, "0000000009", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 9, "User", 2400.00m },
                    { 10, "0000000010", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email@email.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 10, "Admin", 2500.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_clienti_codice_cliente",
                table: "clienti",
                column: "codice_cliente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clienti_person_id",
                table: "clienti",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dipendenti_codice_meccanografico",
                table: "dipendenti",
                column: "codice_meccanografico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dipendenti_person_id",
                table: "dipendenti",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_persone_codice_fiscale",
                table: "persone",
                column: "codice_fiscale",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_persone_email",
                table: "persone",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_prodotti_sku",
                table: "prodotti",
                column: "sku",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clienti");

            migrationBuilder.DropTable(
                name: "dipendenti");

            migrationBuilder.DropTable(
                name: "prodotti");

            migrationBuilder.DropTable(
                name: "persone");
        }
    }
}
