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
    [Migration("20230519091723_addClineId")]
    partial class addClineId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Chat.Domain.Entities.ChatMes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ChatSessionID")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("chatSessionID");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("createdby");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<string>("EmojiSymbols")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("emojiSymbols");

                    b.Property<string>("MediaFiles")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("mediaFiles");

                    b.Property<string>("MessageID")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("messageID");

                    b.Property<string>("MessageStatus")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("messageStatus");

                    b.Property<string>("Receiver")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("receiver");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("remark");

                    b.Property<string>("Sender")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sender");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2")
                        .HasColumnName("timestamp");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2")
                        .HasColumnName("updated");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("updatedby");

                    b.HasKey("Id");

                    b.ToTable("chatMes", "dbo");
                });

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
