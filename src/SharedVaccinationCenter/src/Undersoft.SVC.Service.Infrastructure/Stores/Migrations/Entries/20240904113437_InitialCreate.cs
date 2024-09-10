using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Undersoft.SVC.Service.Infrastructure.Stores.Migrations.Entries
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
                name: "Campaigns",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PriceId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Costs",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: true),
                    Tax = table.Column<double>(type: "double precision", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: true),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    TrafficId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ManufacturerImage = table.Column<string>(type: "text", nullable: true),
                    ManufacturerImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Number = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: true),
                    Tax = table.Column<double>(type: "double precision", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: true),
                    CertificateId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personals",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IdentifierType = table.Column<int>(type: "integer", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: true),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: true),
                    CertificateId = table.Column<long>(type: "bigint", nullable: true),
                    PostSymptomId = table.Column<long>(type: "bigint", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    SecondName = table.Column<string>(type: "text", nullable: true),
                    SocialMedia = table.Column<string>(type: "text", nullable: true),
                    Websites = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Safety",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExpirationDays = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Temperature = table.Column<float>(type: "real", nullable: true),
                    VaccineId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Safety", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Interval = table.Column<TimeSpan>(type: "interval", nullable: true),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    VaccineImage = table.Column<string>(type: "text", nullable: true),
                    VaccineImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Virus = table.Column<string>(type: "text", nullable: true),
                    Dose = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    VaccineId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    AddresslId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionalId = table.Column<long>(type: "bigint", nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vaccinations",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Dose = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Interval = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Expiration = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    PostSymptomId = table.Column<long>(type: "bigint", nullable: true),
                    CertificateId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<double>(type: "double precision", nullable: true),
                    Tax = table.Column<double>(type: "double precision", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: true),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    CampaignId = table.Column<long>(type: "bigint", nullable: true),
                    TrafficId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalSchema: "domain",
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<int>(type: "integer", nullable: false),
                    OfficeId = table.Column<long>(type: "bigint", nullable: true),
                    PersonalId = table.Column<long>(type: "bigint", nullable: true),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    CampaignId = table.Column<long>(type: "bigint", nullable: true),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalSchema: "domain",
                        principalTable: "Campaigns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalSchema: "domain",
                        principalTable: "Offices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Personals_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "domain",
                        principalTable: "Personals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointments_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "domain",
                        principalTable: "Schedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vaccines",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ManufacturerId = table.Column<long>(type: "bigint", nullable: true),
                    SafetyId = table.Column<long>(type: "bigint", nullable: true),
                    SpecificationId = table.Column<long>(type: "bigint", nullable: true),
                    StockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vaccines_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalSchema: "domain",
                        principalTable: "Manufacturers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vaccines_Safety_SafetyId",
                        column: x => x.SafetyId,
                        principalSchema: "domain",
                        principalTable: "Safety",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Vaccines_Specifications_SpecificationId",
                        column: x => x.SpecificationId,
                        principalSchema: "domain",
                        principalTable: "Specifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    AddressType = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    Building = table.Column<string>(type: "text", nullable: true),
                    Apartment = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "domain",
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    OrganizationIdentifierType = table.Column<int>(type: "integer", nullable: false),
                    OrganizationIdentifier = table.Column<string>(type: "text", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationImage = table.Column<string>(type: "text", nullable: true),
                    OrganizationIndustry = table.Column<string>(type: "text", nullable: true),
                    OrganizationName = table.Column<string>(type: "text", nullable: true),
                    OrganizationFullName = table.Column<string>(type: "text", nullable: true),
                    PositionInOrganization = table.Column<string>(type: "text", nullable: true),
                    OrganizationWebsites = table.Column<string>(type: "text", nullable: true),
                    OrganizationImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    OrganizationSize = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "domain",
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Professionals",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ProfessionalImage = table.Column<string>(type: "text", nullable: true),
                    ProfessionalManager = table.Column<string>(type: "text", nullable: true),
                    ProfessionalName = table.Column<string>(type: "text", nullable: true),
                    ProfessionalPosition = table.Column<string>(type: "text", nullable: true),
                    ProfessionalImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    SupplierId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionIndustry = table.Column<string>(type: "text", nullable: true),
                    Profession = table.Column<string>(type: "text", nullable: true),
                    ProfessionalEmail = table.Column<string>(type: "text", nullable: true),
                    ProfessionalPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    ProfessionalSocialMedia = table.Column<string>(type: "text", nullable: true),
                    ProfessionalWebsites = table.Column<string>(type: "text", nullable: true),
                    ProfessionalExperience = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professionals_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "domain",
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CampaignsToVaccines",
                schema: "relations",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_CampaignsToVaccines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampaignsToVaccines_Campaigns_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignsToVaccines_Vaccines_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: true),
                    VaccineId = table.Column<long>(type: "bigint", nullable: true),
                    TermId = table.Column<long>(type: "bigint", nullable: true),
                    CostId = table.Column<long>(type: "bigint", nullable: true),
                    PriceId = table.Column<long>(type: "bigint", nullable: true),
                    PostSymptomId = table.Column<long>(type: "bigint", nullable: true),
                    CertificateId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedures_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "domain",
                        principalTable: "Appointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Procedures_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "domain",
                        principalTable: "Costs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Procedures_Prices_PriceId",
                        column: x => x.PriceId,
                        principalSchema: "domain",
                        principalTable: "Prices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Procedures_Vaccinations_TermId",
                        column: x => x.TermId,
                        principalSchema: "domain",
                        principalTable: "Vaccinations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Procedures_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalSchema: "domain",
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    VaccineId = table.Column<long>(type: "bigint", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalSchema: "domain",
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Certificates",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    PersonalId = table.Column<long>(type: "bigint", nullable: true),
                    VaccineId = table.Column<long>(type: "bigint", nullable: true),
                    TermId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Certificates_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "domain",
                        principalTable: "Payments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Certificates_Personals_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "domain",
                        principalTable: "Personals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Certificates_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalSchema: "domain",
                        principalTable: "Procedures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Certificates_Vaccinations_TermId",
                        column: x => x.TermId,
                        principalSchema: "domain",
                        principalTable: "Vaccinations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Certificates_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalSchema: "domain",
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostSymptoms",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    PersonalId = table.Column<long>(type: "bigint", nullable: true),
                    VaccineId = table.Column<long>(type: "bigint", nullable: true),
                    TermId = table.Column<long>(type: "bigint", nullable: true),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSymptoms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostSymptoms_Personals_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "domain",
                        principalTable: "Personals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostSymptoms_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalSchema: "domain",
                        principalTable: "Procedures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostSymptoms_Vaccinations_TermId",
                        column: x => x.TermId,
                        principalSchema: "domain",
                        principalTable: "Vaccinations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostSymptoms_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalSchema: "domain",
                        principalTable: "Vaccines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    StockId = table.Column<long>(type: "bigint", nullable: true),
                    Quentity = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Stocks_StockId",
                        column: x => x.StockId,
                        principalSchema: "domain",
                        principalTable: "Stocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Traffics",
                schema: "domain",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    OriginId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Modified = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Modifier = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Creator = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CostId = table.Column<long>(type: "bigint", nullable: true),
                    PriceId = table.Column<long>(type: "bigint", nullable: true),
                    Quantity = table.Column<float>(type: "real", nullable: true),
                    StockId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traffics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traffics_Costs_CostId",
                        column: x => x.CostId,
                        principalSchema: "domain",
                        principalTable: "Costs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Traffics_Prices_PriceId",
                        column: x => x.PriceId,
                        principalSchema: "domain",
                        principalTable: "Prices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Traffics_Stocks_StockId",
                        column: x => x.StockId,
                        principalSchema: "domain",
                        principalTable: "Stocks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Index",
                schema: "domain",
                table: "Addresses",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_SupplierId",
                schema: "domain",
                table: "Addresses",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CampaignId",
                schema: "domain",
                table: "Appointments",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Index",
                schema: "domain",
                table: "Appointments",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_OfficeId",
                schema: "domain",
                table: "Appointments",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PersonalId",
                schema: "domain",
                table: "Appointments",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ScheduleId",
                schema: "domain",
                table: "Appointments",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_Index",
                schema: "domain",
                table: "Campaigns",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignsToVaccines_LeftEntityId",
                schema: "relations",
                table: "CampaignsToVaccines",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignsToVaccines_RightEntityId",
                schema: "relations",
                table: "CampaignsToVaccines",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_Index",
                table: "Certificates",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_PaymentId",
                table: "Certificates",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_PersonalId",
                table: "Certificates",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_ProcedureId",
                table: "Certificates",
                column: "ProcedureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_TermId",
                table: "Certificates",
                column: "TermId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Certificates_VaccineId",
                table: "Certificates",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Costs_Index",
                schema: "domain",
                table: "Costs",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_Index",
                schema: "domain",
                table: "Manufacturers",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_Index",
                schema: "domain",
                table: "Offices",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Index",
                schema: "domain",
                table: "Organizations",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_SupplierId",
                schema: "domain",
                table: "Organizations",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Index",
                schema: "domain",
                table: "Payments",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Personals_Index",
                schema: "domain",
                table: "Personals",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_PostSymptoms_Index",
                schema: "domain",
                table: "PostSymptoms",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_PostSymptoms_PersonalId",
                schema: "domain",
                table: "PostSymptoms",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostSymptoms_ProcedureId",
                schema: "domain",
                table: "PostSymptoms",
                column: "ProcedureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostSymptoms_TermId",
                schema: "domain",
                table: "PostSymptoms",
                column: "TermId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostSymptoms_VaccineId",
                schema: "domain",
                table: "PostSymptoms",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_CampaignId",
                schema: "domain",
                table: "Prices",
                column: "CampaignId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prices_Index",
                schema: "domain",
                table: "Prices",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_AppointmentId",
                schema: "domain",
                table: "Procedures",
                column: "AppointmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_CostId",
                schema: "domain",
                table: "Procedures",
                column: "CostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_Index",
                schema: "domain",
                table: "Procedures",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_PriceId",
                schema: "domain",
                table: "Procedures",
                column: "PriceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_TermId",
                schema: "domain",
                table: "Procedures",
                column: "TermId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_VaccineId",
                schema: "domain",
                table: "Procedures",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_Index",
                schema: "domain",
                table: "Professionals",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Professionals_SupplierId",
                schema: "domain",
                table: "Professionals",
                column: "SupplierId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_Index",
                schema: "domain",
                table: "Requests",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StockId",
                schema: "domain",
                table: "Requests",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Safety_Index",
                schema: "domain",
                table: "Safety",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Index",
                schema: "domain",
                table: "Schedules",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_Index",
                schema: "domain",
                table: "Specifications",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_Index",
                schema: "domain",
                table: "Stocks",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_VaccineId",
                schema: "domain",
                table: "Stocks",
                column: "VaccineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Index",
                schema: "domain",
                table: "Suppliers",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Traffics_CostId",
                schema: "domain",
                table: "Traffics",
                column: "CostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Traffics_Index",
                schema: "domain",
                table: "Traffics",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Traffics_PriceId",
                schema: "domain",
                table: "Traffics",
                column: "PriceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Traffics_StockId",
                schema: "domain",
                table: "Traffics",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_Index",
                schema: "domain",
                table: "Vaccinations",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_Index",
                schema: "domain",
                table: "Vaccines",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_ManufacturerId",
                schema: "domain",
                table: "Vaccines",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_SafetyId",
                schema: "domain",
                table: "Vaccines",
                column: "SafetyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vaccines_SpecificationId",
                schema: "domain",
                table: "Vaccines",
                column: "SpecificationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "CampaignsToVaccines",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "Certificates");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "PostSymptoms",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Professionals",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Requests",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Traffics",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Procedures",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Suppliers",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Costs",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Prices",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Vaccinations",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Vaccines",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Offices",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Personals",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Campaigns",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Manufacturers",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Safety",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Specifications",
                schema: "domain");
        }
    }
}
