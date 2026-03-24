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
                    sku = table.Column<string>(type: "varchar(20)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    prezzo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    quantita = table.Column<int>(type: "int", nullable: false),
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
                name: "scontrini",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    data_emissione = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    totale_definitivo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    metodo_pagamento = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scontrini", x => x.id);
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
                    codice_meccanografico = table.Column<string>(type: "varchar(20)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "scontrini_dettagli",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ricevuta_id = table.Column<int>(type: "int", nullable: false),
                    prodotto_id = table.Column<int>(type: "int", nullable: false),
                    quantita = table.Column<int>(type: "int", nullable: false),
                    prezzo_totale = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    modified_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scontrini_dettagli", x => x.id);
                    table.ForeignKey(
                        name: "FK_scontrini_dettagli_prodotti_prodotto_id",
                        column: x => x.prodotto_id,
                        principalTable: "prodotti",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scontrini_dettagli_scontrini_ricevuta_id",
                        column: x => x.ricevuta_id,
                        principalTable: "scontrini",
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
                columns: new[] { "id", "created_at", "is_deleted", "modified_at", "nome", "prezzo", "quantita", "sku" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 1", 1m, 10, "00000000000000000001" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 2", 2m, 20, "00000000000000000002" },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 3", 3m, 30, "00000000000000000003" },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 4", 4m, 40, "00000000000000000004" },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 5", 5m, 50, "00000000000000000005" },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 6", 6m, 60, "00000000000000000006" },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 7", 7m, 70, "00000000000000000007" },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 8", 8m, 80, "00000000000000000008" },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 9", 9m, 90, "00000000000000000009" },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 10", 10m, 100, "00000000000000000010" },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 11", 11m, 110, "00000000000000000011" },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 12", 12m, 120, "00000000000000000012" },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 13", 13m, 130, "00000000000000000013" },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 14", 14m, 140, "00000000000000000014" },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 15", 15m, 150, "00000000000000000015" },
                    { 16, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 16", 16m, 160, "00000000000000000016" },
                    { 17, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 17", 17m, 170, "00000000000000000017" },
                    { 18, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 18", 18m, 180, "00000000000000000018" },
                    { 19, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 19", 19m, 190, "00000000000000000019" },
                    { 20, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Prodotto Esempio 20", 20m, 200, "00000000000000000020" }
                });

            migrationBuilder.InsertData(
                table: "scontrini",
                columns: new[] { "id", "created_at", "data_emissione", "is_deleted", "metodo_pagamento", "modified_at", "totale_definitivo" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 16, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 17, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 18, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 19, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carta di Credito", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m },
                    { 20, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Contanti", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0m }
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
                    { 1, "00000000000000000001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email1@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 1, "User", 1600.00m },
                    { 2, "00000000000000000002", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email2@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 2, "Admin", 1700.00m },
                    { 3, "00000000000000000003", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email3@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 3, "User", 1800.00m },
                    { 4, "00000000000000000004", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email4@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 4, "Admin", 1900.00m },
                    { 5, "00000000000000000005", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email5@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 5, "User", 2000.00m },
                    { 6, "00000000000000000006", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email6@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 6, "Admin", 2100.00m },
                    { 7, "00000000000000000007", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email7@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 7, "User", 2200.00m },
                    { 8, "00000000000000000008", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email8@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 8, "Admin", 2300.00m },
                    { 9, "00000000000000000009", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email9@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 9, "User", 2400.00m },
                    { 10, "00000000000000000010", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "email10@gmail.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Password123!", 10, "Admin", 2500.00m }
                });

            migrationBuilder.InsertData(
                table: "scontrini_dettagli",
                columns: new[] { "id", "created_at", "is_deleted", "modified_at", "prezzo_totale", "prodotto_id", "quantita", "ricevuta_id" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1m, 1, 1, 1 },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4m, 2, 2, 1 },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2m, 2, 1, 2 },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6m, 3, 2, 2 },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3m, 3, 1, 3 },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8m, 4, 2, 3 },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4m, 4, 1, 4 },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, 5, 2, 4 },
                    { 9, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5m, 5, 1, 5 },
                    { 10, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12m, 6, 2, 5 },
                    { 11, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6m, 6, 1, 6 },
                    { 12, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14m, 7, 2, 6 },
                    { 13, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7m, 7, 1, 7 },
                    { 14, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16m, 8, 2, 7 },
                    { 15, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8m, 8, 1, 8 },
                    { 16, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18m, 9, 2, 8 },
                    { 17, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9m, 9, 1, 9 },
                    { 18, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20m, 10, 2, 9 },
                    { 19, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, 10, 1, 10 },
                    { 20, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 22m, 11, 2, 10 },
                    { 21, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 11m, 11, 1, 11 },
                    { 22, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 24m, 12, 2, 11 },
                    { 23, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12m, 12, 1, 12 },
                    { 24, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 26m, 13, 2, 12 },
                    { 25, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 13m, 13, 1, 13 },
                    { 26, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 28m, 14, 2, 13 },
                    { 27, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14m, 14, 1, 14 },
                    { 28, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30m, 15, 2, 14 },
                    { 29, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 15m, 15, 1, 15 },
                    { 30, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 32m, 16, 2, 15 },
                    { 31, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 16m, 16, 1, 16 },
                    { 32, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34m, 17, 2, 16 },
                    { 33, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 17m, 17, 1, 17 },
                    { 34, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 36m, 18, 2, 17 },
                    { 35, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 18m, 18, 1, 18 },
                    { 36, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 38m, 19, 2, 18 },
                    { 37, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 19m, 19, 1, 19 },
                    { 38, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 40m, 20, 2, 19 },
                    { 39, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 20m, 20, 1, 20 },
                    { 40, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2m, 1, 2, 20 }
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
                name: "IX_dipendenti_email_aziendale",
                table: "dipendenti",
                column: "email_aziendale",
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
                name: "IX_prodotti_sku",
                table: "prodotti",
                column: "sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_scontrini_dettagli_prodotto_id",
                table: "scontrini_dettagli",
                column: "prodotto_id");

            migrationBuilder.CreateIndex(
                name: "IX_scontrini_dettagli_ricevuta_id",
                table: "scontrini_dettagli",
                column: "ricevuta_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clienti");

            migrationBuilder.DropTable(
                name: "dipendenti");

            migrationBuilder.DropTable(
                name: "scontrini_dettagli");

            migrationBuilder.DropTable(
                name: "persone");

            migrationBuilder.DropTable(
                name: "prodotti");

            migrationBuilder.DropTable(
                name: "scontrini");
        }
    }
}
