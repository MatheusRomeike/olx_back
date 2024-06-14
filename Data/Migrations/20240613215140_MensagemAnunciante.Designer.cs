﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240613215140_MensagemAnunciante")]
    partial class MensagemAnunciante
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Anuncio.Anuncio", b =>
                {
                    b.Property<int>("AnuncioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AnuncioId"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<int>("EstadoAnuncio")
                        .HasColumnType("integer");

                    b.Property<decimal>("Preco")
                        .HasColumnType("numeric");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<int?>("UsuarioId1")
                        .HasColumnType("integer");

                    b.HasKey("AnuncioId");

                    b.HasIndex("UsuarioId");

                    b.HasIndex("UsuarioId1");

                    b.ToTable("Anuncio");
                });

            modelBuilder.Entity("Domain.AnuncioCategoria.AnuncioCategoria", b =>
                {
                    b.Property<int>("AnuncioId")
                        .HasColumnType("integer");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("integer");

                    b.Property<int?>("AnuncioId1")
                        .HasColumnType("integer");

                    b.Property<int>("CategoriaId1")
                        .HasColumnType("integer");

                    b.HasKey("AnuncioId", "CategoriaId");

                    b.HasIndex("AnuncioId1");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CategoriaId1");

                    b.ToTable("AnuncioCategoria");
                });

            modelBuilder.Entity("Domain.Categoria.Categoria", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoriaId"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("Domain.FotoAnuncio.FotoAnuncio", b =>
                {
                    b.Property<int>("AnuncioId")
                        .HasColumnType("integer");

                    b.Property<int>("SequenciaFotoAnuncio")
                        .HasColumnType("integer");

                    b.Property<int?>("AnuncioId1")
                        .HasColumnType("integer");

                    b.HasKey("AnuncioId", "SequenciaFotoAnuncio");

                    b.HasIndex("AnuncioId1");

                    b.ToTable("FotoAnuncio");
                });

            modelBuilder.Entity("Domain.Interesse.Interesse", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<int>("AnuncioId")
                        .HasColumnType("integer");

                    b.Property<int?>("AnuncioId1")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("UsuarioId1")
                        .HasColumnType("integer");

                    b.HasKey("UsuarioId", "AnuncioId");

                    b.HasIndex("AnuncioId");

                    b.HasIndex("AnuncioId1");

                    b.HasIndex("UsuarioId1");

                    b.ToTable("Interesse");
                });

            modelBuilder.Entity("Domain.Mensagem.Mensagem", b =>
                {
                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.Property<int>("AnuncioId")
                        .HasColumnType("integer");

                    b.Property<int>("SequenciaMensagem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SequenciaMensagem"));

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Texto")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("UsuarioId", "AnuncioId", "SequenciaMensagem");

                    b.HasIndex("AnuncioId");

                    b.ToTable("Mensagem");
                });

            modelBuilder.Entity("Domain.Usuario.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("UsuarioId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Domain.Anuncio.Anuncio", b =>
                {
                    b.HasOne("Domain.Usuario.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuario.Usuario", null)
                        .WithMany("Anuncios")
                        .HasForeignKey("UsuarioId1");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.AnuncioCategoria.AnuncioCategoria", b =>
                {
                    b.HasOne("Domain.Anuncio.Anuncio", "Anuncio")
                        .WithMany()
                        .HasForeignKey("AnuncioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Anuncio.Anuncio", null)
                        .WithMany("AnuncioCategorias")
                        .HasForeignKey("AnuncioId1");

                    b.HasOne("Domain.Categoria.Categoria", null)
                        .WithMany("AnunciosCategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Categoria.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anuncio");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Domain.FotoAnuncio.FotoAnuncio", b =>
                {
                    b.HasOne("Domain.Anuncio.Anuncio", "Anuncio")
                        .WithMany()
                        .HasForeignKey("AnuncioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Anuncio.Anuncio", null)
                        .WithMany("FotosAnuncio")
                        .HasForeignKey("AnuncioId1");

                    b.Navigation("Anuncio");
                });

            modelBuilder.Entity("Domain.Interesse.Interesse", b =>
                {
                    b.HasOne("Domain.Anuncio.Anuncio", "Anuncio")
                        .WithMany()
                        .HasForeignKey("AnuncioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Anuncio.Anuncio", null)
                        .WithMany("Interesses")
                        .HasForeignKey("AnuncioId1");

                    b.HasOne("Domain.Usuario.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuario.Usuario", null)
                        .WithMany("Interesses")
                        .HasForeignKey("UsuarioId1");

                    b.Navigation("Anuncio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Mensagem.Mensagem", b =>
                {
                    b.HasOne("Domain.Anuncio.Anuncio", "Anuncio")
                        .WithMany("Mensagens")
                        .HasForeignKey("AnuncioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Usuario.Usuario", "Usuario")
                        .WithMany("Mensagens")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anuncio");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Domain.Anuncio.Anuncio", b =>
                {
                    b.Navigation("AnuncioCategorias");

                    b.Navigation("FotosAnuncio");

                    b.Navigation("Interesses");

                    b.Navigation("Mensagens");
                });

            modelBuilder.Entity("Domain.Categoria.Categoria", b =>
                {
                    b.Navigation("AnunciosCategorias");
                });

            modelBuilder.Entity("Domain.Usuario.Usuario", b =>
                {
                    b.Navigation("Anuncios");

                    b.Navigation("Interesses");

                    b.Navigation("Mensagens");
                });
#pragma warning restore 612, 618
        }
    }
}
