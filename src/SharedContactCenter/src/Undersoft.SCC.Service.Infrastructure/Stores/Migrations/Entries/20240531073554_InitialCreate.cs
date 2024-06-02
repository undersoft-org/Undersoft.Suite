﻿using System;
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
                name: "relations");

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguages",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
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
                    table.PrimaryKey("PK_CountryLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
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
                name: "Groups",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    GroupImage = table.Column<string>(type: "text", nullable: true),
                    GroupImageData = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactAddresses",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    Building = table.Column<string>(type: "text", nullable: true),
                    Apartment = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactAddresses_Contacts_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactOrganizations",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    OrganizationIndustry = table.Column<string>(type: "text", nullable: true),
                    OrganizationName = table.Column<string>(type: "text", nullable: true),
                    OrganizationFullName = table.Column<string>(type: "text", nullable: true),
                    PositionInOrganization = table.Column<string>(type: "text", nullable: true),
                    OrganizationWebsites = table.Column<string>(type: "text", nullable: true),
                    OrganizationSize = table.Column<int>(type: "integer", nullable: false),
                    OrganizationImage = table.Column<string>(type: "text", nullable: true),
                    OrganizationImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactOrganizations_Contacts_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactPersonals",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
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
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    PersonalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPersonals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPersonals_Contacts_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactProfessionals",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ProfessionIndustry = table.Column<string>(type: "text", nullable: true),
                    Profession = table.Column<string>(type: "text", nullable: true),
                    ProfessionalEmail = table.Column<string>(type: "text", nullable: true),
                    ProfessionalPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    ProfessionalSocialMedia = table.Column<string>(type: "text", nullable: true),
                    ProfessionalWebsites = table.Column<string>(type: "text", nullable: true),
                    ProfessionalExperience = table.Column<float>(type: "real", nullable: true),
                    ContactId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionalId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactProfessionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactProfessionals_Contacts_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalSchema: "domain",
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
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
                        name: "FK_Countries_CountryLanguages_LanguageId",
                        column: x => x.LanguageId,
                        principalSchema: "domain",
                        principalTable: "CountryLanguages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Countries_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalSchema: "domain",
                        principalTable: "Currencies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContactsToGroups",
                schema: "relations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
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
                name: "CountryStates",
                schema: "domain",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_AddressId",
                schema: "domain",
                table: "ContactAddresses",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddresses_Index",
                schema: "domain",
                table: "ContactAddresses",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ContactOrganizations_Index",
                schema: "domain",
                table: "ContactOrganizations",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ContactOrganizations_OrganizationId",
                schema: "domain",
                table: "ContactOrganizations",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonals_Index",
                schema: "domain",
                table: "ContactPersonals",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonals_PersonalId",
                schema: "domain",
                table: "ContactPersonals",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactProfessionals_Index",
                schema: "domain",
                table: "ContactProfessionals",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ContactProfessionals_ProfessionalId",
                schema: "domain",
                table: "ContactProfessionals",
                column: "ProfessionalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Index",
                schema: "domain",
                table: "Contacts",
                column: "Index");

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
                name: "IX_CountryLanguages_Index",
                schema: "domain",
                table: "CountryLanguages",
                column: "Index");

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
                name: "IX_Groups_Index",
                schema: "domain",
                table: "Groups",
                column: "Index");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactAddresses",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ContactOrganizations",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ContactPersonals",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ContactProfessionals",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "ContactsToGroups",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "CountryStates",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "CountryLanguages",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "domain");
        }
    }
}