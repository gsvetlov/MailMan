﻿// <auto-generated />
using System;
using MailMan.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MailMan.Migrations
{
    [DbContext(typeof(MailManDB))]
    [Migration("20210706161251_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MailMan.Models.MailingList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("MailingLists");
                });

            modelBuilder.Entity("MailMan.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Text")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MailMan.Models.Recipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("MailingListId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("MailingListId");

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("MailMan.Models.Sender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int?>("MailingListId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("MailingListId");

                    b.ToTable("Senders");
                });

            modelBuilder.Entity("MailMan.Models.Server", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Login")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int?>("MailingListId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Password")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<bool>("UseSSL")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MailingListId");

                    b.ToTable("Servers");
                });

            modelBuilder.Entity("MailMan.Models.MailingList", b =>
                {
                    b.HasOne("MailMan.Models.Message", "Message")
                        .WithMany()
                        .HasForeignKey("MessageId");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("MailMan.Models.Recipient", b =>
                {
                    b.HasOne("MailMan.Models.MailingList", null)
                        .WithMany("Recipients")
                        .HasForeignKey("MailingListId");
                });

            modelBuilder.Entity("MailMan.Models.Sender", b =>
                {
                    b.HasOne("MailMan.Models.MailingList", null)
                        .WithMany("Senders")
                        .HasForeignKey("MailingListId");
                });

            modelBuilder.Entity("MailMan.Models.Server", b =>
                {
                    b.HasOne("MailMan.Models.MailingList", null)
                        .WithMany("Servers")
                        .HasForeignKey("MailingListId");
                });

            modelBuilder.Entity("MailMan.Models.MailingList", b =>
                {
                    b.Navigation("Recipients");

                    b.Navigation("Senders");

                    b.Navigation("Servers");
                });
#pragma warning restore 612, 618
        }
    }
}
