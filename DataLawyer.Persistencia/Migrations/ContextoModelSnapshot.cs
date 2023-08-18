﻿using DataLawyer.Persistencia.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataLawyer.Persistencia.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("DataLawyer.Dominio.Modelo.LogDeErro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("DataHoraNumerica")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Mensagem")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LogDeErro");
                });

            modelBuilder.Entity("DataLawyer.Dominio.Modelo.MovimentacaoDeProcesso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("DataHoraNumerica")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descricao")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProcessoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProcessoId");

                    b.ToTable("MovimentacaoDeProcesso");
                });

            modelBuilder.Entity("DataLawyer.Dominio.Modelo.Processo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Area")
                        .HasColumnType("TEXT");

                    b.Property<string>("Assunto")
                        .HasColumnType("TEXT");

                    b.Property<string>("Classe")
                        .HasColumnType("TEXT");

                    b.Property<string>("Distribuicao")
                        .HasColumnType("TEXT");

                    b.Property<int>("Grau")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Numero")
                        .HasColumnType("TEXT");

                    b.Property<string>("Origem")
                        .HasColumnType("TEXT");

                    b.Property<string>("Relator")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Processo");
                });

            modelBuilder.Entity("DataLawyer.Dominio.Modelo.MovimentacaoDeProcesso", b =>
                {
                    b.HasOne("DataLawyer.Dominio.Modelo.Processo", "Processo")
                        .WithMany()
                        .HasForeignKey("ProcessoId");

                    b.Navigation("Processo");
                });
#pragma warning restore 612, 618
        }
    }
}