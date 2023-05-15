﻿// <auto-generated />
using System;
using Chat.EntityFrameworkCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chat.EntityFrameworkCore.Migrations
{
    [DbContext(typeof(EFDBContext))]
    [Migration("20230515013041_addDictionary")]
    partial class addDictionary
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Chat.Domain.Entities.Dictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("createdby");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<string>("Key")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("key");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("ParentKey")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("parentkey");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("remark");

                    b.Property<int?>("Slot")
                        .HasColumnType("int")
                        .HasColumnName("slot");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("updatedby");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("value");

                    b.HasKey("Id");

                    b.ToTable("dictionary", "dbo");
                });

            modelBuilder.Entity("Chat.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("account");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("createdby");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<string>("DeviceNo")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("deviceno");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<string>("OpenId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("openid");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<string>("UnionId")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("unionid");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("updatedby");

                    b.HasKey("Id");

                    b.ToTable("user", "dbo");
                });
#pragma warning restore 612, 618
        }
    }
}
