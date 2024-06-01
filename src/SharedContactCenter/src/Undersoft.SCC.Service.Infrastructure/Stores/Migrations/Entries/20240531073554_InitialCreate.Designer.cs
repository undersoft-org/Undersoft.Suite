﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Undersoft.SCC.Service.Infrastructure.Stores;

#nullable disable

namespace Undersoft.SCC.Service.Infrastructure.Stores.Migrations.Entries
{
    [DbContext(typeof(EntryStore))]
    [Migration("20240531073554_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contact", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<long?>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<long?>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PersonalId")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProfessionalId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.ToTable("Contacts", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactAddress", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<long?>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<string>("Apartment")
                        .HasColumnType("text");

                    b.Property<string>("Building")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<long?>("ContactId")
                        .HasColumnType("bigint");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<string>("Postcode")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("Index");

                    b.ToTable("ContactAddresses", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactOrganization", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<long?>("ContactId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("OrganizationFullName")
                        .HasColumnType("text");

                    b.Property<long?>("OrganizationId")
                        .HasColumnType("bigint");

                    b.Property<string>("OrganizationImage")
                        .HasColumnType("text");

                    b.Property<byte[]>("OrganizationImageData")
                        .HasColumnType("bytea");

                    b.Property<string>("OrganizationIndustry")
                        .HasColumnType("text");

                    b.Property<string>("OrganizationName")
                        .HasColumnType("text");

                    b.Property<int>("OrganizationSize")
                        .HasColumnType("integer");

                    b.Property<string>("OrganizationWebsites")
                        .HasColumnType("text");

                    b.Property<string>("PositionInOrganization")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.HasIndex("OrganizationId")
                        .IsUnique();

                    b.ToTable("ContactOrganizations", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactPersonal", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<long?>("ContactId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<long?>("PersonalId")
                        .HasColumnType("bigint");

                    b.Property<string>("PersonalImage")
                        .HasColumnType("text");

                    b.Property<byte[]>("PersonalImageData")
                        .HasColumnType("bytea");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.HasIndex("PersonalId")
                        .IsUnique();

                    b.ToTable("ContactPersonals", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactProfessional", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<long?>("ContactId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Profession")
                        .HasColumnType("text");

                    b.Property<string>("ProfessionIndustry")
                        .HasColumnType("text");

                    b.Property<string>("ProfessionalEmail")
                        .HasColumnType("text");

                    b.Property<float?>("ProfessionalExperience")
                        .HasColumnType("real");

                    b.Property<long?>("ProfessionalId")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfessionalPhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("ProfessionalSocialMedia")
                        .HasColumnType("text");

                    b.Property<string>("ProfessionalWebsites")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.HasIndex("ProfessionalId")
                        .IsUnique();

                    b.ToTable("ContactProfessionals", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Countries.CountryLanguage", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<string>("LanguageCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.ToTable("CountryLanguages", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Countries.CountryState", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<long?>("CountryId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("StateCode")
                        .HasColumnType("text");

                    b.Property<string>("TimeZone")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("Index");

                    b.ToTable("CountryStates", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Countries.Currency", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<string>("CurrencyCode")
                        .HasColumnType("text");

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.ToTable("Currencies", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Country", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<string>("Continent")
                        .HasColumnType("text");

                    b.Property<string>("CountryCode")
                        .HasColumnType("text");

                    b.Property<string>("CountryImage")
                        .HasColumnType("text");

                    b.Property<byte[]>("CountryImageData")
                        .HasColumnType("bytea");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<long?>("CurrencyId")
                        .HasColumnType("bigint");

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<long?>("LanguageId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("TimeZone")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("Index");

                    b.HasIndex("LanguageId");

                    b.ToTable("Countries", "domain");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Group", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<string>("GroupImage")
                        .HasColumnType("text");

                    b.Property<byte[]>("GroupImageData")
                        .HasColumnType("bytea");

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.ToTable("Groups", "domain");
                });

            modelBuilder.Entity("Undersoft.SDK.Service.Infrastructure.Database.Relation.RelatedLink<Undersoft.SCC.Domain.Entities.Contact, Undersoft.SCC.Domain.Entities.Group>", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(4);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(10);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Index"));

                    b.Property<string>("Label")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(11);

                    b.Property<long?>("LeftEntityId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(6);

                    b.Property<string>("Modifier")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(7);

                    b.Property<long?>("RightEntityId")
                        .HasColumnType("bigint");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("LeftEntityId");

                    b.HasIndex("RightEntityId");

                    b.ToTable("ContactsToGroups", "relations");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactAddress", b =>
                {
                    b.HasOne("Undersoft.SCC.Domain.Entities.Contact", "Contact")
                        .WithOne("Address")
                        .HasForeignKey("Undersoft.SCC.Domain.Entities.Contacts.ContactAddress", "AddressId");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactOrganization", b =>
                {
                    b.HasOne("Undersoft.SCC.Domain.Entities.Contact", "Contact")
                        .WithOne("Organization")
                        .HasForeignKey("Undersoft.SCC.Domain.Entities.Contacts.ContactOrganization", "OrganizationId");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactPersonal", b =>
                {
                    b.HasOne("Undersoft.SCC.Domain.Entities.Contact", "Contact")
                        .WithOne("Personal")
                        .HasForeignKey("Undersoft.SCC.Domain.Entities.Contacts.ContactPersonal", "PersonalId");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contacts.ContactProfessional", b =>
                {
                    b.HasOne("Undersoft.SCC.Domain.Entities.Contact", "Contact")
                        .WithOne("Professional")
                        .HasForeignKey("Undersoft.SCC.Domain.Entities.Contacts.ContactProfessional", "ProfessionalId");

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Countries.CountryState", b =>
                {
                    b.HasOne("Undersoft.SCC.Domain.Entities.Country", "Country")
                        .WithMany("States")
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Country", b =>
                {
                    b.HasOne("Undersoft.SCC.Domain.Entities.Countries.Currency", "Currency")
                        .WithMany("Countries")
                        .HasForeignKey("CurrencyId");

                    b.HasOne("Undersoft.SCC.Domain.Entities.Countries.CountryLanguage", "Language")
                        .WithMany("Countries")
                        .HasForeignKey("LanguageId");

                    b.Navigation("Currency");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("Undersoft.SDK.Service.Infrastructure.Database.Relation.RelatedLink<Undersoft.SCC.Domain.Entities.Contact, Undersoft.SCC.Domain.Entities.Group>", b =>
                {
                    b.HasOne("Undersoft.SCC.Domain.Entities.Contact", "LeftEntity")
                        .WithMany()
                        .HasForeignKey("LeftEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Undersoft.SCC.Domain.Entities.Group", "RightEntity")
                        .WithMany()
                        .HasForeignKey("RightEntityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("LeftEntity");

                    b.Navigation("RightEntity");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Contact", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Organization");

                    b.Navigation("Personal");

                    b.Navigation("Professional");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Countries.CountryLanguage", b =>
                {
                    b.Navigation("Countries");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Countries.Currency", b =>
                {
                    b.Navigation("Countries");
                });

            modelBuilder.Entity("Undersoft.SCC.Domain.Entities.Country", b =>
                {
                    b.Navigation("States");
                });
#pragma warning restore 612, 618
        }
    }
}
