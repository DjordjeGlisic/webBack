﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace septmebarski2_2024.Migrations
{
    [DbContext(typeof(ProdavnicaContext))]
    partial class ProdavnicaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KorpaProizvod", b =>
                {
                    b.Property<int>("KorpeId")
                        .HasColumnType("int");

                    b.Property<int>("ProizvodiId")
                        .HasColumnType("int");

                    b.HasKey("KorpeId", "ProizvodiId");

                    b.HasIndex("ProizvodiId");

                    b.ToTable("KorpaProizvod");
                });

            modelBuilder.Entity("Models.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Kategorija");
                });

            modelBuilder.Entity("Models.Korpa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Korpa");
                });

            modelBuilder.Entity("Models.Proizvod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<int?>("KategorijaId")
                        .HasColumnType("int");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Opis")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaId");

                    b.ToTable("Proizvod");
                });

            modelBuilder.Entity("KorpaProizvod", b =>
                {
                    b.HasOne("Models.Korpa", null)
                        .WithMany()
                        .HasForeignKey("KorpeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Proizvod", null)
                        .WithMany()
                        .HasForeignKey("ProizvodiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.Proizvod", b =>
                {
                    b.HasOne("Models.Kategorija", "Kategorija")
                        .WithMany("Proizvodi")
                        .HasForeignKey("KategorijaId");

                    b.Navigation("Kategorija");
                });

            modelBuilder.Entity("Models.Kategorija", b =>
                {
                    b.Navigation("Proizvodi");
                });
#pragma warning restore 612, 618
        }
    }
}