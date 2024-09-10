﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Undersoft.SVC.Service.Infrastructure.Stores;

#nullable disable

namespace Undersoft.SVC.Service.Infrastructure.Stores.Migrations.Events
{
    [DbContext(typeof(EventStore))]
    [Migration("20240904113532_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Undersoft.SDK.Service.Data.Event.Event", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.Property<string>("CodeNo")
                        .IsConcurrencyToken()
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp")
                        .HasColumnOrder(8);

                    b.Property<string>("Creator")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnOrder(9);

                    b.Property<byte[]>("Data")
                        .HasColumnType("bytea");

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("EntityTypeName")
                        .HasColumnType("text");

                    b.Property<string>("EventType")
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

                    b.Property<long>("OriginId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(3);

                    b.Property<int>("PublishStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PublishTime")
                        .HasColumnType("timestamp");

                    b.Property<long>("TypeId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(2);

                    b.Property<string>("TypeName")
                        .HasMaxLength(768)
                        .HasColumnType("character varying(768)")
                        .HasColumnOrder(5);

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Index");

                    b.ToTable("Events", "domain");
                });
#pragma warning restore 612, 618
        }
    }
}
