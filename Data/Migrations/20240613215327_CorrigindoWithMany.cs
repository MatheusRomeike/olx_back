using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoWithMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anuncio_Usuario_UsuarioId1",
                table: "Anuncio");

            migrationBuilder.DropForeignKey(
                name: "FK_AnuncioCategoria_Anuncio_AnuncioId1",
                table: "AnuncioCategoria");

            migrationBuilder.DropForeignKey(
                name: "FK_FotoAnuncio_Anuncio_AnuncioId1",
                table: "FotoAnuncio");

            migrationBuilder.DropForeignKey(
                name: "FK_Interesse_Anuncio_AnuncioId1",
                table: "Interesse");

            migrationBuilder.DropForeignKey(
                name: "FK_Interesse_Usuario_UsuarioId1",
                table: "Interesse");

            migrationBuilder.DropIndex(
                name: "IX_Interesse_AnuncioId1",
                table: "Interesse");

            migrationBuilder.DropIndex(
                name: "IX_Interesse_UsuarioId1",
                table: "Interesse");

            migrationBuilder.DropIndex(
                name: "IX_FotoAnuncio_AnuncioId1",
                table: "FotoAnuncio");

            migrationBuilder.DropIndex(
                name: "IX_AnuncioCategoria_AnuncioId1",
                table: "AnuncioCategoria");

            migrationBuilder.DropIndex(
                name: "IX_Anuncio_UsuarioId1",
                table: "Anuncio");

            migrationBuilder.DropColumn(
                name: "AnuncioId1",
                table: "Interesse");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Interesse");

            migrationBuilder.DropColumn(
                name: "AnuncioId1",
                table: "FotoAnuncio");

            migrationBuilder.DropColumn(
                name: "AnuncioId1",
                table: "AnuncioCategoria");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Anuncio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnuncioId1",
                table: "Interesse",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Interesse",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnuncioId1",
                table: "FotoAnuncio",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnuncioId1",
                table: "AnuncioCategoria",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Anuncio",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interesse_AnuncioId1",
                table: "Interesse",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Interesse_UsuarioId1",
                table: "Interesse",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_FotoAnuncio_AnuncioId1",
                table: "FotoAnuncio",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioCategoria_AnuncioId1",
                table: "AnuncioCategoria",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Anuncio_UsuarioId1",
                table: "Anuncio",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Anuncio_Usuario_UsuarioId1",
                table: "Anuncio",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnuncioCategoria_Anuncio_AnuncioId1",
                table: "AnuncioCategoria",
                column: "AnuncioId1",
                principalTable: "Anuncio",
                principalColumn: "AnuncioId");

            migrationBuilder.AddForeignKey(
                name: "FK_FotoAnuncio_Anuncio_AnuncioId1",
                table: "FotoAnuncio",
                column: "AnuncioId1",
                principalTable: "Anuncio",
                principalColumn: "AnuncioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interesse_Anuncio_AnuncioId1",
                table: "Interesse",
                column: "AnuncioId1",
                principalTable: "Anuncio",
                principalColumn: "AnuncioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interesse_Usuario_UsuarioId1",
                table: "Interesse",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId");
        }
    }
}
