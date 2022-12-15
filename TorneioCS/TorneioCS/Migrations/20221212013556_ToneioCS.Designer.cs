﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TorneioCS.Context;

namespace TorneioCS.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20221212013556_ToneioCS")]
    partial class ToneioCS
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TorneioCS.Models.Competidor", b =>
                {
                    b.Property<int>("idCompetidor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("TotalKill")
                        .HasColumnType("int");

                    b.Property<int>("TotalPartidas")
                        .HasColumnType("int");

                    b.Property<int>("Vitorias")
                        .HasColumnType("int");

                    b.HasKey("idCompetidor");

                    b.ToTable("Competidores");
                });
#pragma warning restore 612, 618
        }
    }
}
