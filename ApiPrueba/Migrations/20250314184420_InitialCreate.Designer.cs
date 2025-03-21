﻿// <auto-generated />
using System;
using ApiPrueba.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiPrueba.Migrations
{
    [DbContext(typeof(BdRegistroEscolarContext))]
    [Migration("20250314184420_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiPrueba.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Nacimiento")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sexo")
                        .HasColumnType("bit");

                    b.Property<string>("TelefonoPadres")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("ApiPrueba.Models.Asignatura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaestroId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaestroId");

                    b.ToTable("Asignaturas");
                });

            modelBuilder.Entity("ApiPrueba.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadAlumnos")
                        .HasColumnType("int");

                    b.Property<int>("EncargadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("ApiPrueba.Models.Maestro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Maestros");
                });

            modelBuilder.Entity("ApiPrueba.Models.Alumno", b =>
                {
                    b.HasOne("ApiPrueba.Models.Curso", "Curso")
                        .WithMany("Alumnos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("ApiPrueba.Models.Asignatura", b =>
                {
                    b.HasOne("ApiPrueba.Models.Maestro", "Maestro")
                        .WithMany("Asignaturas")
                        .HasForeignKey("MaestroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Maestro");
                });

            modelBuilder.Entity("ApiPrueba.Models.Maestro", b =>
                {
                    b.HasOne("ApiPrueba.Models.Curso", "Curso")
                        .WithMany("Maestros")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("ApiPrueba.Models.Curso", b =>
                {
                    b.Navigation("Alumnos");

                    b.Navigation("Maestros");
                });

            modelBuilder.Entity("ApiPrueba.Models.Maestro", b =>
                {
                    b.Navigation("Asignaturas");
                });
#pragma warning restore 612, 618
        }
    }
}
