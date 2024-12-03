﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace junski2024A.Migrations
{
    [DbContext(typeof(IznajmContext))]
    partial class IznajmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Automobil", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrojDana")
                        .HasColumnType("int");

                    b.Property<int>("BrojSedista")
                        .HasColumnType("int");

                    b.Property<float>("CenaPoDanu")
                        .HasColumnType("real");

                    b.Property<int>("Godiste")
                        .HasColumnType("int");

                    b.Property<long>("Kilometraza")
                        .HasColumnType("bigint");

                    b.Property<int?>("KorisnikAutomobilaID")
                        .HasColumnType("int");

                    b.Property<int?>("ModelAutomobilaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KorisnikAutomobilaID");

                    b.HasIndex("ModelAutomobilaID");

                    b.ToTable("Automobil");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<long>("BrojVozacke")
                        .HasColumnType("bigint");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("Jmbg")
                        .HasColumnType("bigint");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Korisnik");
                });

            modelBuilder.Entity("Models.Model", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("NazivModela")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Model");
                });

            modelBuilder.Entity("Models.Automobil", b =>
                {
                    b.HasOne("Models.Korisnik", "KorisnikAutomobila")
                        .WithMany("TrenutnoIznajmljeni")
                        .HasForeignKey("KorisnikAutomobilaID");

                    b.HasOne("Models.Model", "ModelAutomobila")
                        .WithMany("ListaAutomobila")
                        .HasForeignKey("ModelAutomobilaID");

                    b.Navigation("KorisnikAutomobila");

                    b.Navigation("ModelAutomobila");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Navigation("TrenutnoIznajmljeni");
                });

            modelBuilder.Entity("Models.Model", b =>
                {
                    b.Navigation("ListaAutomobila");
                });
#pragma warning restore 612, 618
        }
    }
}
