using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Undersoft.GDC.Service.Infrastructure.Stores.Migrations.Reports
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

            migrationBuilder.EnsureSchema(
                name: "identifiers");

            migrationBuilder.CreateTable(
                name: "Activities",
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
                    LocationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
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
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
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
                    LocationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
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
                    LocationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
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
                    LocationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
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
                    LocationId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
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
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityIdentifiers",
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
                    table.PrimaryKey("PK_ActivityIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityIdentifiers_Activities_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivitiesToDetails",
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
                    table.PrimaryKey("PK_ActivitiesToDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitiesToDetails_Activities_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitiesToDetails_DetailSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ActivitiesToGroups",
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
                    table.PrimaryKey("PK_ActivitiesToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitiesToGroups_Activities_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitiesToGroups_Groups_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
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
                name: "FromMemberToMember",
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
                    table.PrimaryKey("PK_FromMemberToMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FromMemberToMember_Members_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FromMemberToMember_Members_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberIdentifiers",
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
                    table.PrimaryKey("PK_MemberIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberIdentifiers_Members_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembersToDetails",
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
                    table.PrimaryKey("PK_MembersToDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembersToDetails_DetailSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersToDetails_Members_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembersToGroups",
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
                    table.PrimaryKey("PK_MembersToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembersToGroups_Groups_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersToGroups_Members_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourceIdentifiers",
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
                    table.PrimaryKey("PK_ResourceIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceIdentifiers_Resources_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesToDetails",
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
                    table.PrimaryKey("PK_ResourcesToDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourcesToDetails_DetailSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourcesToDetails_Resources_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesToGroups",
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
                    table.PrimaryKey("PK_ResourcesToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourcesToGroups_Groups_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourcesToGroups_Resources_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleIdentifiers",
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
                    table.PrimaryKey("PK_ScheduleIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleIdentifiers_Schedules_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SchedulesToDetails",
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
                    table.PrimaryKey("PK_SchedulesToDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulesToDetails_DetailSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulesToDetails_Schedules_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchedulesToGroups",
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
                    table.PrimaryKey("PK_SchedulesToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulesToGroups_Groups_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulesToGroups_Schedules_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
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
                    LocaleType = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PhoneType = table.Column<int>(type: "integer", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Notices = table.Column<string>(type: "text", nullable: true),
                    MemberId = table.Column<long>(type: "bigint", nullable: true),
                    ActivityId = table.Column<long>(type: "bigint", nullable: true),
                    ResourceId = table.Column<long>(type: "bigint", nullable: true),
                    ScheduleId = table.Column<long>(type: "bigint", nullable: true),
                    ServiceId = table.Column<long>(type: "bigint", nullable: true),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalSchema: "domain",
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalSchema: "domain",
                        principalTable: "Resources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalSchema: "domain",
                        principalTable: "Schedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Locations_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MembersToServices",
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
                    table.PrimaryKey("PK_MembersToServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembersToServices_Members_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersToServices_Services_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceIdentifiers",
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
                    table.PrimaryKey("PK_ServiceIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceIdentifiers_Services_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServicesToActivities",
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
                    table.PrimaryKey("PK_ServicesToActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesToActivities_Activities_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesToActivities_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesToDetails",
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
                    table.PrimaryKey("PK_ServicesToDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesToDetails_DetailSet_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "DetailSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesToDetails_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesToGroups",
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
                    table.PrimaryKey("PK_ServicesToGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesToGroups_Groups_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesToGroups_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesToResources",
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
                    table.PrimaryKey("PK_ServicesToResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesToResources_Resources_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesToResources_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesToSchedules",
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
                    table.PrimaryKey("PK_ServicesToSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesToSchedules_Schedules_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesToSchedules_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivitiesToSettings",
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
                    table.PrimaryKey("PK_ActivitiesToSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivitiesToSettings_Activities_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitiesToSettings_Settings_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembersToSettings",
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
                    table.PrimaryKey("PK_MembersToSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembersToSettings_Members_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersToSettings_Settings_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesToSettings",
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
                    table.PrimaryKey("PK_ResourcesToSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourcesToSettings_Resources_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourcesToSettings_Settings_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchedulesToSettings",
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
                    table.PrimaryKey("PK_SchedulesToSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchedulesToSettings_Schedules_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SchedulesToSettings_Settings_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicesToSettings",
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
                    table.PrimaryKey("PK_ServicesToSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesToSettings_Services_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicesToSettings_Settings_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_SettingIdentifiers_Settings_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Settings",
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
                        name: "FK_SettingToSetting_Settings_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SettingToSetting_Settings_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Settings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationsToAddresses",
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
                    table.PrimaryKey("PK_LocationsToAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationsToAddresses_Addresses_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationsToAddresses_Locations_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Places",
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
                    LocationId = table.Column<long>(type: "bigint", nullable: true),
                    Kind = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Height = table.Column<int>(type: "integer", nullable: true),
                    Width = table.Column<int>(type: "integer", nullable: true),
                    Length = table.Column<int>(type: "integer", nullable: true),
                    X = table.Column<int>(type: "integer", nullable: true),
                    Y = table.Column<int>(type: "integer", nullable: true),
                    Z = table.Column<int>(type: "integer", nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: true),
                    Unit = table.Column<string>(type: "text", nullable: true),
                    Latitue = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    Altitude = table.Column<double>(type: "double precision", nullable: true),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    Block = table.Column<int>(type: "integer", nullable: false),
                    Sector = table.Column<int>(type: "integer", nullable: false),
                    Cluster = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Places_Locations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "domain",
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Endpoints",
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
                    PlaceId = table.Column<long>(type: "bigint", nullable: true),
                    Host = table.Column<string>(type: "text", nullable: true),
                    IP = table.Column<string>(type: "text", nullable: true),
                    Port = table.Column<int>(type: "integer", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: true),
                    BaseUrl = table.Column<string>(type: "text", nullable: true),
                    OS = table.Column<string>(type: "text", nullable: true),
                    Protocol = table.Column<string>(type: "text", nullable: true),
                    Method = table.Column<string>(type: "text", nullable: true),
                    Parameters = table.Column<string[]>(type: "text[]", nullable: true),
                    ReturnUrl = table.Column<string>(type: "text", nullable: true),
                    StateUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endpoints_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalSchema: "domain",
                        principalTable: "Places",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Index",
                schema: "domain",
                table: "Activities",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesToDetails_LeftEntityId",
                schema: "relations",
                table: "ActivitiesToDetails",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesToDetails_RightEntityId",
                schema: "relations",
                table: "ActivitiesToDetails",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesToGroups_LeftEntityId",
                schema: "relations",
                table: "ActivitiesToGroups",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesToGroups_RightEntityId",
                schema: "relations",
                table: "ActivitiesToGroups",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesToSettings_LeftEntityId",
                schema: "relations",
                table: "ActivitiesToSettings",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesToSettings_RightEntityId",
                schema: "relations",
                table: "ActivitiesToSettings",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityIdentifiers_Key",
                schema: "identifiers",
                table: "ActivityIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityIdentifiers_ObjectId",
                schema: "identifiers",
                table: "ActivityIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Index",
                schema: "domain",
                table: "Addresses",
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
                name: "IX_Endpoints_Index",
                schema: "domain",
                table: "Endpoints",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Endpoints_PlaceId",
                schema: "domain",
                table: "Endpoints",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_FromMemberToMember_LeftEntityId",
                schema: "relations",
                table: "FromMemberToMember",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_FromMemberToMember_RightEntityId",
                schema: "relations",
                table: "FromMemberToMember",
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
                name: "IX_Locations_ActivityId",
                schema: "domain",
                table: "Locations",
                column: "ActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Index",
                schema: "domain",
                table: "Locations",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_MemberId",
                schema: "domain",
                table: "Locations",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ResourceId",
                schema: "domain",
                table: "Locations",
                column: "ResourceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ScheduleId",
                schema: "domain",
                table: "Locations",
                column: "ScheduleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ServiceId",
                schema: "domain",
                table: "Locations",
                column: "ServiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocationsToAddresses_LeftEntityId",
                schema: "relations",
                table: "LocationsToAddresses",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsToAddresses_RightEntityId",
                schema: "relations",
                table: "LocationsToAddresses",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberIdentifiers_Key",
                schema: "identifiers",
                table: "MemberIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_MemberIdentifiers_ObjectId",
                schema: "identifiers",
                table: "MemberIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Index",
                schema: "domain",
                table: "Members",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToDetails_LeftEntityId",
                schema: "relations",
                table: "MembersToDetails",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToDetails_RightEntityId",
                schema: "relations",
                table: "MembersToDetails",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToGroups_LeftEntityId",
                schema: "relations",
                table: "MembersToGroups",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToGroups_RightEntityId",
                schema: "relations",
                table: "MembersToGroups",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToServices_LeftEntityId",
                schema: "relations",
                table: "MembersToServices",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToServices_RightEntityId",
                schema: "relations",
                table: "MembersToServices",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToSettings_LeftEntityId",
                schema: "relations",
                table: "MembersToSettings",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersToSettings_RightEntityId",
                schema: "relations",
                table: "MembersToSettings",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Places_Index",
                schema: "domain",
                table: "Places",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_Places_LocationId",
                schema: "domain",
                table: "Places",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceIdentifiers_Key",
                schema: "identifiers",
                table: "ResourceIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceIdentifiers_ObjectId",
                schema: "identifiers",
                table: "ResourceIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Index",
                schema: "domain",
                table: "Resources",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesToDetails_LeftEntityId",
                schema: "relations",
                table: "ResourcesToDetails",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesToDetails_RightEntityId",
                schema: "relations",
                table: "ResourcesToDetails",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesToGroups_LeftEntityId",
                schema: "relations",
                table: "ResourcesToGroups",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesToGroups_RightEntityId",
                schema: "relations",
                table: "ResourcesToGroups",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesToSettings_LeftEntityId",
                schema: "relations",
                table: "ResourcesToSettings",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourcesToSettings_RightEntityId",
                schema: "relations",
                table: "ResourcesToSettings",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleIdentifiers_Key",
                schema: "identifiers",
                table: "ScheduleIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleIdentifiers_ObjectId",
                schema: "identifiers",
                table: "ScheduleIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_Index",
                schema: "domain",
                table: "Schedules",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesToDetails_LeftEntityId",
                schema: "relations",
                table: "SchedulesToDetails",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesToDetails_RightEntityId",
                schema: "relations",
                table: "SchedulesToDetails",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesToGroups_LeftEntityId",
                schema: "relations",
                table: "SchedulesToGroups",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesToGroups_RightEntityId",
                schema: "relations",
                table: "SchedulesToGroups",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesToSettings_LeftEntityId",
                schema: "relations",
                table: "SchedulesToSettings",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulesToSettings_RightEntityId",
                schema: "relations",
                table: "SchedulesToSettings",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceIdentifiers_Key",
                schema: "identifiers",
                table: "ServiceIdentifiers",
                column: "Key");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceIdentifiers_ObjectId",
                schema: "identifiers",
                table: "ServiceIdentifiers",
                column: "ObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Index",
                schema: "domain",
                table: "Services",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToActivities_LeftEntityId",
                schema: "relations",
                table: "ServicesToActivities",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToActivities_RightEntityId",
                schema: "relations",
                table: "ServicesToActivities",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToDetails_LeftEntityId",
                schema: "relations",
                table: "ServicesToDetails",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToDetails_RightEntityId",
                schema: "relations",
                table: "ServicesToDetails",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToGroups_LeftEntityId",
                schema: "relations",
                table: "ServicesToGroups",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToGroups_RightEntityId",
                schema: "relations",
                table: "ServicesToGroups",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToResources_LeftEntityId",
                schema: "relations",
                table: "ServicesToResources",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToResources_RightEntityId",
                schema: "relations",
                table: "ServicesToResources",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToSchedules_LeftEntityId",
                schema: "relations",
                table: "ServicesToSchedules",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToSchedules_RightEntityId",
                schema: "relations",
                table: "ServicesToSchedules",
                column: "RightEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToSettings_LeftEntityId",
                schema: "relations",
                table: "ServicesToSettings",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesToSettings_RightEntityId",
                schema: "relations",
                table: "ServicesToSettings",
                column: "RightEntityId");

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
                name: "IX_Settings_Index",
                schema: "domain",
                table: "Settings",
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
                name: "ActivitiesToDetails",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ActivitiesToGroups",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ActivitiesToSettings",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ActivityIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "DetailIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "DetailToDetail",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "Endpoints",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "FromMemberToMember",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "GroupToGroup",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "LocationsToAddresses",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "MemberIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "MembersToDetails",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "MembersToGroups",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "MembersToServices",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "MembersToSettings",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ResourceIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "ResourcesToDetails",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ResourcesToGroups",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ResourcesToSettings",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ScheduleIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "SchedulesToDetails",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "SchedulesToGroups",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "SchedulesToSettings",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ServiceIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "ServicesToActivities",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ServicesToDetails",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ServicesToGroups",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ServicesToResources",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ServicesToSchedules",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "ServicesToSettings",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "SettingIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "SettingToSetting",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "Places",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "DetailSet",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "domain");
        }
    }
}
