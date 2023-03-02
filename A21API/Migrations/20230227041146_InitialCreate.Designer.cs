﻿// <auto-generated />
using System;
using A21API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace A21API.Migrations
{
    [DbContext(typeof(A21APIContext))]
    [Migration("20230227041146_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("A21API.Models.CrenoHoraire", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmploiTempsID")
                        .HasColumnType("int");

                    b.Property<int?>("EnseignantID")
                        .HasColumnType("int");

                    b.Property<string>("Jours")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Periode")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmploiTempsID");

                    b.HasIndex("EnseignantID");

                    b.ToTable("CrenoHoraires");
                });

            modelBuilder.Entity("A21API.Models.EmploiTemps", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Annee_Scolaire")
                        .HasColumnType("longtext");

                    b.Property<int?>("Groupe")
                        .HasColumnType("int");

                    b.Property<int?>("Locale")
                        .HasColumnType("int");

                    b.Property<string>("Nom_Ecole")
                        .HasColumnType("longtext");

                    b.HasKey("ID");

                    b.ToTable("EmploiTemps");
                });

            modelBuilder.Entity("A21API.Models.Enseignant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Cours")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("NHeures")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Enseignants");
                });

            modelBuilder.Entity("A21API.Models.CrenoHoraire", b =>
                {
                    b.HasOne("A21API.Models.EmploiTemps", "EmploiTemps")
                        .WithMany("CrenoHoraires")
                        .HasForeignKey("EmploiTempsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("A21API.Models.Enseignant", "Enseignant")
                        .WithMany("CrenoHoraires")
                        .HasForeignKey("EnseignantID");

                    b.Navigation("EmploiTemps");

                    b.Navigation("Enseignant");
                });

            modelBuilder.Entity("A21API.Models.EmploiTemps", b =>
                {
                    b.Navigation("CrenoHoraires");
                });

            modelBuilder.Entity("A21API.Models.Enseignant", b =>
                {
                    b.Navigation("CrenoHoraires");
                });
#pragma warning restore 612, 618
        }
    }
}
