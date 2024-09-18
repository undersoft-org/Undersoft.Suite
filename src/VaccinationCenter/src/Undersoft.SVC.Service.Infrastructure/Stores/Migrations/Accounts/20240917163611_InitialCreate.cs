using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Undersoft.SVC.Service.Infrastructure.Stores.Migrations.Accounts
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.CreateTable(
                name: "AccountUsers",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RegistrationCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsLockedOut = table.Column<bool>(type: "boolean", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consents",
                schema: "Accounts",
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
                    TermsText = table.Column<string>(type: "text", nullable: true),
                    TermsConsent = table.Column<bool>(type: "boolean", nullable: false),
                    PersonalDataText = table.Column<string>(type: "text", nullable: true),
                    PersonalDataConsent = table.Column<bool>(type: "boolean", nullable: false),
                    MarketingText = table.Column<string>(type: "text", nullable: true),
                    MarketingConsent = table.Column<bool>(type: "boolean", nullable: false),
                    ThirdPartyText = table.Column<string>(type: "text", nullable: true),
                    ThirdPartyConsent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Credentials",
                schema: "Accounts",
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
                    Site = table.Column<int>(type: "integer", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    OldPassword = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    RegistrationCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    SessionToken = table.Column<string>(type: "text", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    EmailConfirmationToken = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmationToken = table.Column<string>(type: "text", nullable: true),
                    RegistrationCompleteToken = table.Column<string>(type: "text", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    SaveAccountInCookies = table.Column<bool>(type: "boolean", nullable: false),
                    Authenticated = table.Column<bool>(type: "boolean", nullable: false),
                    IsLockedOut = table.Column<bool>(type: "boolean", nullable: false),
                    ReturnPath = table.Column<string>(type: "text", nullable: true),
                    RetypedPassword = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    TermsConsent = table.Column<bool>(type: "boolean", nullable: false),
                    CookiesConsent = table.Column<bool>(type: "boolean", nullable: false),
                    OptionalConsent = table.Column<bool>(type: "boolean", nullable: false),
                    NewPassword = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationNotes",
                schema: "Accounts",
                columns: table => new
                {
                    CodeNo = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    TypeName = table.Column<string>(type: "character varying(768)", maxLength: 768, nullable: true),
                    Errors = table.Column<string>(type: "text", nullable: true),
                    Success = table.Column<string>(type: "text", nullable: true),
                    Info = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "Accounts",
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
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "Accounts",
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
                    CardTitle = table.Column<string>(type: "text", nullable: true),
                    CardNumber = table.Column<string>(type: "text", nullable: true),
                    CardType = table.Column<string>(type: "text", nullable: true),
                    CardExpirationDate = table.Column<string>(type: "text", nullable: true),
                    CardCSV = table.Column<string>(type: "text", nullable: true),
                    PaymentFirstName = table.Column<string>(type: "text", nullable: true),
                    PaymentLastName = table.Column<string>(type: "text", nullable: true),
                    PaymentTermsConsent = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentType = table.Column<string>(type: "text", nullable: true),
                    PaymentPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PaymentImage = table.Column<string>(type: "text", nullable: true),
                    PaymentImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    PaymentStatus = table.Column<string>(type: "text", nullable: true),
                    PaymentProvider = table.Column<string>(type: "text", nullable: true),
                    PaymentWebsites = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "Accounts",
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
                    SubscriptionName = table.Column<string>(type: "text", nullable: true),
                    SubscriptionDescription = table.Column<string>(type: "text", nullable: true),
                    SubscriptionPeriod = table.Column<double>(type: "double precision", nullable: false),
                    SubscriptionExpireDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SubscriptionQuantity = table.Column<double>(type: "double precision", nullable: false),
                    SubscriptionValue = table.Column<double>(type: "double precision", nullable: false),
                    SubscriptionCurrency = table.Column<string>(type: "text", nullable: true),
                    SubscriptionStatus = table.Column<string>(type: "text", nullable: true),
                    SubscriptionToken = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                schema: "Accounts",
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
                    TenantName = table.Column<string>(type: "text", nullable: true),
                    TenantUrl = table.Column<string>(type: "text", nullable: true),
                    TenantPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountClaims",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountClaims_AccountUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Accounts",
                        principalTable: "AccountUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountLogins",
                schema: "Accounts",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AccountLogins_AccountUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Accounts",
                        principalTable: "AccountUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                schema: "Accounts",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRoles_AccountUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Accounts",
                        principalTable: "AccountUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Accounts",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    AccountRoleId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_AccountRoleId",
                        column: x => x.AccountRoleId,
                        principalSchema: "Accounts",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Accounts",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountAddreses",
                schema: "Accounts",
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
                    Country = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    Building = table.Column<string>(type: "text", nullable: true),
                    Apartment = table.Column<string>(type: "text", nullable: true),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountAddreses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountConsents",
                schema: "Accounts",
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
                    ConsentId = table.Column<long>(type: "bigint", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    TermsText = table.Column<string>(type: "text", nullable: true),
                    TermsConsent = table.Column<bool>(type: "boolean", nullable: false),
                    PersonalDataText = table.Column<string>(type: "text", nullable: true),
                    PersonalDataConsent = table.Column<bool>(type: "boolean", nullable: false),
                    MarketingText = table.Column<string>(type: "text", nullable: true),
                    MarketingConsent = table.Column<bool>(type: "boolean", nullable: false),
                    ThirdPartyText = table.Column<string>(type: "text", nullable: true),
                    ThirdPartyConsent = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConsents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountConsents_Consents_ConsentId",
                        column: x => x.ConsentId,
                        principalSchema: "Accounts",
                        principalTable: "Consents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountOrganizations",
                schema: "Accounts",
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
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_AccountOrganizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountOrganizations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "Accounts",
                        principalTable: "Organizations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountPayments",
                schema: "Accounts",
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
                    PaymentId = table.Column<long>(type: "bigint", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    CardTitle = table.Column<string>(type: "text", nullable: true),
                    CardNumber = table.Column<string>(type: "text", nullable: true),
                    CardType = table.Column<string>(type: "text", nullable: true),
                    CardExpirationDate = table.Column<string>(type: "text", nullable: true),
                    CardCSV = table.Column<string>(type: "text", nullable: true),
                    PaymentFirstName = table.Column<string>(type: "text", nullable: true),
                    PaymentLastName = table.Column<string>(type: "text", nullable: true),
                    PaymentTermsConsent = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentType = table.Column<string>(type: "text", nullable: true),
                    PaymentPhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PaymentImage = table.Column<string>(type: "text", nullable: true),
                    PaymentImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    PaymentStatus = table.Column<string>(type: "text", nullable: true),
                    PaymentProvider = table.Column<string>(type: "text", nullable: true),
                    PaymentWebsites = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountPayments_Payment_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Accounts",
                        principalTable: "Payment",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountPersonals",
                schema: "Accounts",
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
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_AccountPersonals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountProffesionals",
                schema: "Accounts",
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
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_AccountProffesionals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                schema: "Accounts",
                columns: table => new
                {
                    AccountsId = table.Column<long>(type: "bigint", nullable: false),
                    RolesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_AccountRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "Accounts",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Accounts",
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
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    PersonalId = table.Column<long>(type: "bigint", nullable: true),
                    AddressId = table.Column<long>(type: "bigint", nullable: true),
                    ProfessionalId = table.Column<long>(type: "bigint", nullable: true),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    ConsentId = table.Column<long>(type: "bigint", nullable: true),
                    SubscriptionId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    CredentialsId = table.Column<long>(type: "bigint", nullable: true),
                    NotesId = table.Column<long>(type: "bigint", nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    Authenticated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountAddreses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Accounts",
                        principalTable: "AccountAddreses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_AccountConsents_ConsentId",
                        column: x => x.ConsentId,
                        principalSchema: "Accounts",
                        principalTable: "AccountConsents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_AccountOrganizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "Accounts",
                        principalTable: "AccountOrganizations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_AccountPayments_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Accounts",
                        principalTable: "AccountPayments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_AccountPersonals_PersonalId",
                        column: x => x.PersonalId,
                        principalSchema: "Accounts",
                        principalTable: "AccountPersonals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_AccountProffesionals_ProfessionalId",
                        column: x => x.ProfessionalId,
                        principalSchema: "Accounts",
                        principalTable: "AccountProffesionals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_Credentials_CredentialsId",
                        column: x => x.CredentialsId,
                        principalSchema: "Accounts",
                        principalTable: "Credentials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_OperationNotes_NotesId",
                        column: x => x.NotesId,
                        principalSchema: "Accounts",
                        principalTable: "OperationNotes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountSubscriptions",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SubscriptionId = table.Column<long>(type: "bigint", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountSubscriptions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountSubscriptions_Subscriptions_Id",
                        column: x => x.Id,
                        principalSchema: "Accounts",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountSubscriptions_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "Accounts",
                        principalTable: "Subscriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountTenants",
                schema: "Accounts",
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
                    TenantId = table.Column<long>(type: "bigint", nullable: true),
                    AccountId = table.Column<long>(type: "bigint", nullable: true),
                    TenantName = table.Column<string>(type: "text", nullable: true),
                    TenantUrl = table.Column<string>(type: "text", nullable: true),
                    TenantPath = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTenants_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "Accounts",
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountTokens",
                schema: "Accounts",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AccountTokens_AccountUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Accounts",
                        principalTable: "AccountUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountTokens_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountAddreses_AccountId",
                schema: "Accounts",
                table: "AccountAddreses",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountClaims_UserId",
                schema: "Accounts",
                table: "AccountClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConsents_AccountId",
                schema: "Accounts",
                table: "AccountConsents",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountConsents_ConsentId",
                schema: "Accounts",
                table: "AccountConsents",
                column: "ConsentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountLogins_UserId",
                schema: "Accounts",
                table: "AccountLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountOrganizations_AccountId",
                schema: "Accounts",
                table: "AccountOrganizations",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountOrganizations_OrganizationId",
                schema: "Accounts",
                table: "AccountOrganizations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPayments_AccountId",
                schema: "Accounts",
                table: "AccountPayments",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountPayments_PaymentId",
                schema: "Accounts",
                table: "AccountPayments",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPersonals_AccountId",
                schema: "Accounts",
                table: "AccountPersonals",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountProffesionals_AccountId",
                schema: "Accounts",
                table: "AccountProffesionals",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RolesId",
                schema: "Accounts",
                table: "AccountRole",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_RoleId",
                schema: "Accounts",
                table: "AccountRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AddressId",
                schema: "Accounts",
                table: "Accounts",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ConsentId",
                schema: "Accounts",
                table: "Accounts",
                column: "ConsentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CredentialsId",
                schema: "Accounts",
                table: "Accounts",
                column: "CredentialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_NotesId",
                schema: "Accounts",
                table: "Accounts",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OrganizationId",
                schema: "Accounts",
                table: "Accounts",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PaymentId",
                schema: "Accounts",
                table: "Accounts",
                column: "PaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PersonalId",
                schema: "Accounts",
                table: "Accounts",
                column: "PersonalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ProfessionalId",
                schema: "Accounts",
                table: "Accounts",
                column: "ProfessionalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_SubscriptionId",
                schema: "Accounts",
                table: "Accounts",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenantId",
                schema: "Accounts",
                table: "Accounts",
                column: "TenantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubscriptions_AccountId",
                schema: "Accounts",
                table: "AccountSubscriptions",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountSubscriptions_SubscriptionId",
                schema: "Accounts",
                table: "AccountSubscriptions",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTenants_AccountId",
                schema: "Accounts",
                table: "AccountTenants",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTenants_TenantId",
                schema: "Accounts",
                table: "AccountTenants",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTokens_AccountId",
                schema: "Accounts",
                table: "AccountTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Accounts",
                table: "AccountUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Accounts",
                table: "AccountUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_AccountRoleId",
                schema: "Accounts",
                table: "RoleClaims",
                column: "AccountRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Accounts",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Accounts",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountAddreses_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountAddreses",
                column: "AccountId",
                principalSchema: "Accounts",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountConsents_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountConsents",
                column: "AccountId",
                principalSchema: "Accounts",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountOrganizations_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountOrganizations",
                column: "AccountId",
                principalSchema: "Accounts",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPayments_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountPayments",
                column: "AccountId",
                principalSchema: "Accounts",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountPersonals_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountPersonals",
                column: "AccountId",
                principalSchema: "Accounts",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountProffesionals_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountProffesionals",
                column: "AccountId",
                principalSchema: "Accounts",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRole_Accounts_AccountsId",
                schema: "Accounts",
                table: "AccountRole",
                column: "AccountsId",
                principalSchema: "Accounts",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountSubscriptions_SubscriptionId",
                schema: "Accounts",
                table: "Accounts",
                column: "SubscriptionId",
                principalSchema: "Accounts",
                principalTable: "AccountSubscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountTenants_TenantId",
                schema: "Accounts",
                table: "Accounts",
                column: "TenantId",
                principalSchema: "Accounts",
                principalTable: "AccountTenants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountAddreses_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountAddreses");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountConsents_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountConsents");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountOrganizations_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountOrganizations");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountPayments_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountPersonals_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountPersonals");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountProffesionals_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountProffesionals");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountSubscriptions_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountSubscriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountTenants_Accounts_AccountId",
                schema: "Accounts",
                table: "AccountTenants");

            migrationBuilder.DropTable(
                name: "AccountClaims",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountLogins",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountRole",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountRoles",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTokens",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountUsers",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountAddreses",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountConsents",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountOrganizations",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountPayments",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountPersonals",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountProffesionals",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountSubscriptions",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTenants",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Credentials",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "OperationNotes",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Consents",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Tenants",
                schema: "Accounts");
        }
    }
}
