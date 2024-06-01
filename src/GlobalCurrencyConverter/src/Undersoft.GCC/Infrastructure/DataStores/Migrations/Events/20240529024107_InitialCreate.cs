using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Undersoft.GCC.Infrastructure.DataStores.Migrations.Events
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
                name: "Events",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    TypeId = table.Column<long>(type: "INTEGER", nullable: false),
                    CodeNo = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "TEXT", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "INTEGER", nullable: false),
                    Label = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Version = table.Column<uint>(type: "INTEGER", nullable: false),
                    EventType = table.Column<string>(type: "TEXT", nullable: true),
                    EntityId = table.Column<long>(type: "INTEGER", nullable: false),
                    EntityTypeName = table.Column<string>(type: "TEXT", nullable: true),
                    Data = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PublishTime = table.Column<DateTime>(type: "timestamp", nullable: false),
                    PublishStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_Index",
                schema: "domain",
                table: "Events",
                column: "Index");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events",
                schema: "domain");
        }
    }
}
