using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Undersoft.SCC.Service.Infrastructure.Stores.Migrations.Entries
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.EnsureSchema(
                name: "identifiers");

            migrationBuilder.EnsureSchema(
                name: "relations");

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CurrencyCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailSet",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true),
                    Document = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Kind = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    GroupImage = table.Column<string>(type: "text", nullable: true),
                    GroupImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Industries",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    LanguageCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SettingSet",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Data = table.Column<byte[]>(type: "bytea", nullable: true),
                    Document = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Kind = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetailIdentifiers",
                schema: "identifiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailIdentifiers_DetailSet_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetailToDetail",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightEntityId = table.Column<long>(type: "bigint", nullable: true),
                    LeftEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailToDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailToDetail_DetailSet_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailToDetail_DetailSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupToGroup",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightEntityId = table.Column<long>(type: "bigint", nullable: true),
                    LeftEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupToGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupToGroup_Groups_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupToGroup_Groups_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    OrganizationIndustry = table.Column<string>(type: "text", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationName = table.Column<string>(type: "text", nullable: true),
                    OrganizationFullName = table.Column<string>(type: "text", nullable: true),
                    PositionInOrganization = table.Column<string>(type: "text", nullable: true),
                    OrganizationWebsites = table.Column<string>(type: "text", nullable: true),
                    OrganizationSize = table.Column<int>(type: "integer", nullable: false),
                    OrganizationImage = table.Column<string>(type: "text", nullable: true),
                    OrganizationImageData = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalSchema: "domain",
                        principalTable: "Industries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    Continent = table.Column<string>(type: "text", nullable: true),
                    TimeZone = table.Column<string>(type: "text", nullable: true),
                    CountryImage = table.Column<string>(type: "text", nullable: true),
                    CountryImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    LanguageId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "domain",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "domain",
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SettingIdentifiers",
                schema: "identifiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingIdentifiers_SettingSet_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "SettingSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SettingToSetting",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightEntityId = table.Column<long>(type: "bigint", nullable: true),
                    LeftEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SettingToSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SettingToSetting_SettingSet_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "SettingSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingToSetting_SettingSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "SettingSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PersonalId = table.Column<long>(type: "bigint", nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionalId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "domain",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryStates",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    StateCode = table.Column<string>(type: "text", nullable: true),
                    TimeZone = table.Column<string>(type: "text", nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryStates_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "domain",
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactIdentifiers",
                schema: "identifiers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    Key = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactIdentifiers_Contacts_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContactsToDetails",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightEntityId = table.Column<long>(type: "bigint", nullable: true),
                    LeftEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsToDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsToDetails_Contacts_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactsToDetails_DetailSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactsToGroups",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightEntityId = table.Column<long>(type: "bigint", nullable: true),
                    LeftEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsToGroups_Contacts_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactsToGroups_Groups_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactsToSettings",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightEntityId = table.Column<long>(type: "bigint", nullable: true),
                    LeftEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsToSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsToSettings_Contacts_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactsToSettings_SettingSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "SettingSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactToContact",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    RightEntityId = table.Column<long>(type: "bigint", nullable: true),
                    LeftEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactToContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactToContact_Contacts_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactToContact_Contacts_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personals",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PersonalImage = table.Column<string>(type: "text", nullable: true),
                    PersonalImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personals_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Professionals",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ProfessionIndustry = table.Column<string>(type: "text", nullable: true),
                    IndustryId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionName = table.Column<string>(type: "text", nullable: true),
                    ProfessionId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionalEmail = table.Column<string>(type: "text", nullable: true),
                    ProfessionalPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    ProfessionalSocialMedia = table.Column<string>(type: "text", nullable: true),
                    ProfessionalWebsites = table.Column<string>(type: "text", nullable: true),
                    ProfessionalExperience = table.Column<float>(type: "real", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professionals_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Professionals_Industries_IndustryId",
                        column: x => x.IndustryId,
                        principalSchema: "domain",
                        principalTable: "Industries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Professionals_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalSchema: "domain",
                        principalTable: "Professions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CountryId = table.Column<long>(type: "bigint", nullable: true),
                    CountryName = table.Column<string>(type: "text", nullable: true),
                    CountryStateId = table.Column<long>(type: "bigint", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    Building = table.Column<string>(type: "text", nullable: true),
                    Apartment = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalSchema: "domain",
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_CountryStates_CountryStateId",
                        column: x => x.CountryStateId,
                        principalSchema: "domain",
                        principalTable: "CountryStates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ContactId",
                schema: "domain",
                table: "Addresses",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                schema: "domain",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryStateId",
                schema: "domain",
                table: "Addresses",
                column: "CountryStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Index",
                schema: "domain",
                table: "Addresses",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ContactIdentifiers_Key",
                schema: "identifiers",
                table: "ContactIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ContactIdentifiers_ObjectId",
                schema: "identifiers",
                table: "ContactIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Index",
                schema: "domain",
                table: "Contacts",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_OrganizationId",
                schema: "domain",
                table: "Contacts",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsToDetails_LeftEntityId",
                schema: "relations",
                table: "ContactsToDetails",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsToDetails_RightEntityId",
                schema: "relations",
                table: "ContactsToDetails",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsToGroups_LeftEntityId",
                schema: "relations",
                table: "ContactsToGroups",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsToGroups_RightEntityId",
                schema: "relations",
                table: "ContactsToGroups",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsToSettings_LeftEntityId",
                schema: "relations",
                table: "ContactsToSettings",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsToSettings_RightEntityId",
                schema: "relations",
                table: "ContactsToSettings",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactToContact_LeftEntityId",
                schema: "relations",
                table: "ContactToContact",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactToContact_RightEntityId",
                schema: "relations",
                table: "ContactToContact",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CurrencyId",
                schema: "domain",
                table: "Countries",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Index",
                schema: "domain",
                table: "Countries",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_LanguageId",
                schema: "domain",
                table: "Countries",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryStates_CountryId",
                schema: "domain",
                table: "CountryStates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryStates_Index",
                schema: "domain",
                table: "CountryStates",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Index",
                schema: "domain",
                table: "Currencies",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_DetailIdentifiers_Key",
                schema: "identifiers",
                table: "DetailIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_DetailIdentifiers_ObjectId",
                schema: "identifiers",
                table: "DetailIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailSet_Index",
                schema: "domain",
                table: "DetailSet",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_DetailToDetail_LeftEntityId",
                schema: "relations",
                table: "DetailToDetail",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailToDetail_RightEntityId",
                schema: "relations",
                table: "DetailToDetail",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Index",
                schema: "domain",
                table: "Groups",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_GroupToGroup_LeftEntityId",
                schema: "relations",
                table: "GroupToGroup",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupToGroup_RightEntityId",
                schema: "relations",
                table: "GroupToGroup",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Industries_Index",
                schema: "domain",
                table: "Industries",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Index",
                schema: "domain",
                table: "Languages",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Index",
                schema: "domain",
                table: "Organizations",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_IndustryId",
                schema: "domain",
                table: "Organizations",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Personals_ContactId",
                schema: "domain",
                table: "Personals",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personals_Index",
                schema: "domain",
                table: "Personals",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_ContactId",
                schema: "domain",
                table: "Professionals",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_Index",
                schema: "domain",
                table: "Professionals",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_IndustryId",
                schema: "domain",
                table: "Professionals",
                column: "IndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_ProfessionId",
                schema: "domain",
                table: "Professionals",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Professions_Index",
                schema: "domain",
                table: "Professions",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_SettingIdentifiers_Key",
                schema: "identifiers",
                table: "SettingIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_SettingIdentifiers_ObjectId",
                schema: "identifiers",
                table: "SettingIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingSet_Index",
                schema: "domain",
                table: "SettingSet",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_SettingToSetting_LeftEntityId",
                schema: "relations",
                table: "SettingToSetting",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SettingToSetting_RightEntityId",
                schema: "relations",
                table: "SettingToSetting",
                column: "RightEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ContactIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "ContactsToDetails",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ContactsToGroups",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ContactsToSettings",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ContactToContact",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "DetailIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "DetailToDetail",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "GroupToGroup",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "Personals",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Professionals",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "SettingIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "SettingToSetting",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "CountryStates",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "DetailSet",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Professions",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "SettingSet",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Languages",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Industries",
                schema: "domain");
        }
    }
}
