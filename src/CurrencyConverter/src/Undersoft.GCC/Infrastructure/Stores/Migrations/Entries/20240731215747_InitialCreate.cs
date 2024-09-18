using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Undersoft.GCC.Infrastructure.Stores.Migrations.Entries
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    TypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CodeNo = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Label = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    CurrencyCode = table.Column<string>(type: "TEXT", nullable: true),
                    Symbol = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IsDecimal = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyProviders",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    TypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CodeNo = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Label = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseCurrencyId = table.Column<long>(type: "INTEGER", nullable: true),
                    BaseUri = table.Column<string>(type: "TEXT", nullable: true),
                    UpdateHour = table.Column<int>(type: "INTEGER", nullable: false),
                    UpdateMinute = table.Column<int>(type: "INTEGER", nullable: false),
                    HistorySince = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyProviders_Currencies_BaseCurrencyId",
                        column: x => x.BaseCurrencyId,
                        principalSchema: "domain",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRateTable",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    TypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CodeNo = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Label = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ProviderId = table.Column<long>(type: "INTEGER", nullable: true),
                    SourceCurrencyId = table.Column<long>(type: "INTEGER", nullable: true),
                    SourceRate = table.Column<double>(type: "REAL", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Decimals = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRateTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRateTable_Currencies_SourceCurrencyId",
                        column: x => x.SourceCurrencyId,
                        principalSchema: "domain",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrencyRateTable_CurrencyProviders_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "domain",
                        principalTable: "CurrencyProviders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    TypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CodeNo = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Label = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ProviderId = table.Column<long>(type: "INTEGER", nullable: true),
                    TableId = table.Column<long>(type: "INTEGER", nullable: true),
                    SourceCurrencyId = table.Column<long>(type: "INTEGER", nullable: true),
                    SourceRate = table.Column<double>(type: "REAL", nullable: false),
                    TargetCurrencyId = table.Column<long>(type: "INTEGER", nullable: true),
                    TargetRate = table.Column<double>(type: "REAL", nullable: false),
                    Rate = table.Column<double>(type: "REAL", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Decimals = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_SourceCurrencyId",
                        column: x => x.SourceCurrencyId,
                        principalSchema: "domain",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_TargetCurrencyId",
                        column: x => x.TargetCurrencyId,
                        principalSchema: "domain",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrencyRates_CurrencyProviders_ProviderId",
                        column: x => x.ProviderId,
                        principalSchema: "domain",
                        principalTable: "CurrencyProviders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CurrencyRates_CurrencyRateTable_TableId",
                        column: x => x.TableId,
                        principalTable: "CurrencyRateTable",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Index",
                schema: "domain",
                table: "Currencies",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyProviders_BaseCurrencyId",
                schema: "domain",
                table: "CurrencyProviders",
                column: "BaseCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyProviders_Index",
                schema: "domain",
                table: "CurrencyProviders",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_Index",
                schema: "domain",
                table: "CurrencyRates",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_ProviderId",
                schema: "domain",
                table: "CurrencyRates",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_SourceCurrencyId",
                schema: "domain",
                table: "CurrencyRates",
                column: "SourceCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_TableId",
                schema: "domain",
                table: "CurrencyRates",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_TargetCurrencyId",
                schema: "domain",
                table: "CurrencyRates",
                column: "TargetCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRateTable_Index",
                table: "CurrencyRateTable",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRateTable_ProviderId",
                table: "CurrencyRateTable",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRateTable_SourceCurrencyId",
                table: "CurrencyRateTable",
                column: "SourceCurrencyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRates",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "CurrencyRateTable");

            migrationBuilder.DropTable(
                name: "CurrencyProviders",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "domain");
        }
    }
}
