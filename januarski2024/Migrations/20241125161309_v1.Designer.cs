﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace januarski2024.Migrations
{
    [DbContext(typeof(CetContext))]
    [Migration("20241125161309_v1")]
    partial class v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Cet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("KorisnikID")
                        .HasColumnType("int");

                    b.Property<string>("Nadimak")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SobaID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikID");

                    b.HasIndex("SobaID");

                    b.ToTable("Cet");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Boja")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Korisncko")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sifra")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("ID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Models.Soba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("MaxBrojClanova")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("ID");

                    b.ToTable("Soba");
                });

            modelBuilder.Entity("Models.Cet", b =>
                {
                    b.HasOne("Models.Korisnik", "Korisnik")
                        .WithMany("KorisniciSaNadimkom")
                        .HasForeignKey("KorisnikID");

                    b.HasOne("Models.Soba", "Soba")
                        .WithMany("KorisniciSaNadimkom")
                        .HasForeignKey("SobaID");

                    b.Navigation("Korisnik");

                    b.Navigation("Soba");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Navigation("KorisniciSaNadimkom");
                });

            modelBuilder.Entity("Models.Soba", b =>
                {
                    b.Navigation("KorisniciSaNadimkom");
                });
#pragma warning restore 612, 618
        }
    }
}
