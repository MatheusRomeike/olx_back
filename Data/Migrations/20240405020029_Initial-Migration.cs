using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    FotoPerfil = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    DataNascimento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Anuncio",
                columns: table => new
                {
                    AnuncioId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Descricao = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    EstadoAnuncio = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UsuarioId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anuncio", x => x.AnuncioId);
                    table.ForeignKey(
                        name: "FK_Anuncio_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anuncio_Usuario_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRelatorio",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    SequenciaRelatorio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UsuarioId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRelatorio", x => new { x.UsuarioId, x.SequenciaRelatorio });
                    table.ForeignKey(
                        name: "FK_UsuarioRelatorio_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRelatorio_Usuario_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "AnuncioCategoria",
                columns: table => new
                {
                    AnuncioId = table.Column<int>(type: "integer", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: false),
                    CategoriaId1 = table.Column<int>(type: "integer", nullable: false),
                    AnuncioId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnuncioCategoria", x => new { x.AnuncioId, x.CategoriaId });
                    table.ForeignKey(
                        name: "FK_AnuncioCategoria_Anuncio_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnuncioCategoria_Anuncio_AnuncioId1",
                        column: x => x.AnuncioId1,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId");
                    table.ForeignKey(
                        name: "FK_AnuncioCategoria_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnuncioCategoria_Categoria_CategoriaId1",
                        column: x => x.CategoriaId1,
                        principalTable: "Categoria",
                        principalColumn: "CategoriaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FotoAnuncio",
                columns: table => new
                {
                    AnuncioId = table.Column<int>(type: "integer", nullable: false),
                    SequenciaFotoAnuncio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Foto = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    AnuncioId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FotoAnuncio", x => new { x.AnuncioId, x.SequenciaFotoAnuncio });
                    table.ForeignKey(
                        name: "FK_FotoAnuncio_Anuncio_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FotoAnuncio_Anuncio_AnuncioId1",
                        column: x => x.AnuncioId1,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId");
                });

            migrationBuilder.CreateTable(
                name: "Interesse",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    AnuncioId = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AnuncioId1 = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interesse", x => new { x.UsuarioId, x.AnuncioId });
                    table.ForeignKey(
                        name: "FK_Interesse_Anuncio_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interesse_Anuncio_AnuncioId1",
                        column: x => x.AnuncioId1,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId");
                    table.ForeignKey(
                        name: "FK_Interesse_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interesse_Usuario_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Mensagem",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    AnuncioId = table.Column<int>(type: "integer", nullable: false),
                    SequenciaMensagem = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Texto = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AnuncioId1 = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagem", x => new { x.UsuarioId, x.AnuncioId, x.SequenciaMensagem });
                    table.ForeignKey(
                        name: "FK_Mensagem_Anuncio_AnuncioId",
                        column: x => x.AnuncioId,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensagem_Anuncio_AnuncioId1",
                        column: x => x.AnuncioId1,
                        principalTable: "Anuncio",
                        principalColumn: "AnuncioId");
                    table.ForeignKey(
                        name: "FK_Mensagem_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensagem_Usuario_UsuarioId1",
                        column: x => x.UsuarioId1,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_UsuarioId",
                table: "Anuncio",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_UsuarioId1",
                table: "Anuncio",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioCategoria_AnuncioId1",
                table: "AnuncioCategoria",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioCategoria_CategoriaId",
                table: "AnuncioCategoria",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioCategoria_CategoriaId1",
                table: "AnuncioCategoria",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_FotoAnuncio_AnuncioId1",
                table: "FotoAnuncio",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Interesse_AnuncioId",
                table: "Interesse",
                column: "AnuncioId");

            migrationBuilder.CreateIndex(
                name: "IX_Interesse_AnuncioId1",
                table: "Interesse",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Interesse_UsuarioId1",
                table: "Interesse",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_AnuncioId",
                table: "Mensagem",
                column: "AnuncioId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_AnuncioId1",
                table: "Mensagem",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_UsuarioId1",
                table: "Mensagem",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRelatorio_UsuarioId1",
                table: "UsuarioRelatorio",
                column: "UsuarioId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnuncioCategoria");

            migrationBuilder.DropTable(
                name: "FotoAnuncio");

            migrationBuilder.DropTable(
                name: "Interesse");

            migrationBuilder.DropTable(
                name: "Mensagem");

            migrationBuilder.DropTable(
                name: "UsuarioRelatorio");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Anuncio");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
