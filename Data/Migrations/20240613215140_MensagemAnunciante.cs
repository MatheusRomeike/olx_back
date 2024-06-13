using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MensagemAnunciante : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensagem_Anuncio_AnuncioId1",
                table: "Mensagem");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensagem_Usuario_UsuarioId1",
                table: "Mensagem");

            migrationBuilder.DropIndex(
                name: "IX_Mensagem_AnuncioId1",
                table: "Mensagem");

            migrationBuilder.DropIndex(
                name: "IX_Mensagem_UsuarioId1",
                table: "Mensagem");

            migrationBuilder.DropColumn(
                name: "AnuncioId1",
                table: "Mensagem");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Mensagem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnuncioId1",
                table: "Mensagem",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Mensagem",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_AnuncioId1",
                table: "Mensagem",
                column: "AnuncioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_UsuarioId1",
                table: "Mensagem",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagem_Anuncio_AnuncioId1",
                table: "Mensagem",
                column: "AnuncioId1",
                principalTable: "Anuncio",
                principalColumn: "AnuncioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagem_Usuario_UsuarioId1",
                table: "Mensagem",
                column: "UsuarioId1",
                principalTable: "Usuario",
                principalColumn: "UsuarioId");
        }
    }
}
