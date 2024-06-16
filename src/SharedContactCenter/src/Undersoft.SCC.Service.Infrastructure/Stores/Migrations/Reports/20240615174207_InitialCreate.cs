using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Undersoft.SCC.Service.Infrastructure.Stores.Migrations.Reports
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identifiers");

            migrationBuilder.EnsureSchema(
                name: "domain");

            migrationBuilder.EnsureSchema(
                name: "relations");

            migrationBuilder.CreateTable(
                name: "Details",
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
                    table.PrimaryKey("PK_Details", x => x.Id);
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
                    Name = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
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
                        name: "FK_DetailIdentifiers_Details_ObjectId",
                        column: x => x.ObjectId,
                        principalSchema: "domain",
                        principalTable: "Details",
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
                        name: "FK_DetailToDetail_Details_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailToDetail_Details_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Details",
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
                        name: "FK_MembersToDetails_Details_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Details",
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
                name: "MemberToMember",
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
                    table.PrimaryKey("PK_MemberToMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberToMember_Members_LeftEntityId",
                        column: x => x.LeftEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberToMember_Members_RightEntityId",
                        column: x => x.RightEntityId,
                        principalSchema: "domain",
                        principalTable: "Members",
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
                name: "IX_Details_Index",
                schema: "domain",
                table: "Details",
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
                name: "IX_MemberToMember_LeftEntityId",
                schema: "relations",
                table: "MemberToMember",
                column: "LeftEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberToMember_RightEntityId",
                schema: "relations",
                table: "MemberToMember",
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
                name: "DetailIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "DetailToDetail",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "GroupToGroup",
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
                name: "MembersToSettings",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "MemberToMember",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "SettingIdentifiers",
                schema: "identifiers");

            migrationBuilder.DropTable(
                name: "SettingToSetting",
                schema: "relations");

            migrationBuilder.DropTable(
                name: "Details",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "domain");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "domain");
        }
    }
}
