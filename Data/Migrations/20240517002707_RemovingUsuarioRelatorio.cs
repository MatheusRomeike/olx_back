using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovingUsuarioRelatorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioRelatorio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsuarioRelatorio",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    SequenciaRelatorio = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataCadastro = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRelatorio_UsuarioId1",
                table: "UsuarioRelatorio",
                column: "UsuarioId1");
        }
    }
}
