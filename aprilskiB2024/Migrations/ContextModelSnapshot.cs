﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace aprilskiB2024.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Maraton", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DuzinaStazeM")
                        .HasColumnType("int");

                    b.Property<string>("Lokacija")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("VremeKrajaTrke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VremePocetkaTrke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Maraton");
                });

            modelBuilder.Entity("Models.Trka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrojMaratonca")
                        .HasColumnType("int");

                    b.Property<float>("BrzinaTrkaca")
                        .HasColumnType("real");

                    b.Property<int>("MaratonacId")
                        .HasColumnType("int");

                    b.Property<int>("Pozicija")
                        .HasColumnType("int");

                    b.Property<float>("PredjenoMetra")
                        .HasColumnType("real");

                    b.Property<int>("Progres")
                        .HasColumnType("int");

                    b.Property<int>("TakmicenjeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaratonacId");

                    b.HasIndex("TakmicenjeId");

                    b.ToTable("Trka");
                });

            modelBuilder.Entity("Models.Trkac", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("JMBG")
                        .HasColumnType("bigint");

                    b.Property<int>("Nagrade")
                        .HasColumnType("int");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Trkac");
                });

            modelBuilder.Entity("Models.Trka", b =>
                {
                    b.HasOne("Models.Trkac", "Maratonac")
                        .WithMany("Maratoni")
                        .HasForeignKey("MaratonacId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Maraton", "Takmicenje")
                        .WithMany("Ucensici")
                        .HasForeignKey("TakmicenjeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maratonac");

                    b.Navigation("Takmicenje");
                });

            modelBuilder.Entity("Models.Maraton", b =>
                {
                    b.Navigation("Ucensici");
                });

            modelBuilder.Entity("Models.Trkac", b =>
                {
                    b.Navigation("Maratoni");
                });
#pragma warning restore 612, 618
        }
    }
}