using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AnuncioCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnuncioCategoria_Anuncio_AnuncioId1",
                table: "AnuncioCategoria");

            migrationBuilder.DropIndex(
                name: "IX_AnuncioCategoria_AnuncioId1",
                table: "AnuncioCategoria");

            migrationBuilder.DropColumn(
                name: "AnuncioId1",
                table: "AnuncioCategoria");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Anuncio",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Anuncio");

            migrationBuilder.AddColumn<int>(
                name: "AnuncioId1",
                table: "AnuncioCategoria",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnuncioCategoria_AnuncioId1",
                table: "AnuncioCategoria",
                column: "AnuncioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AnuncioCategoria_Anuncio_AnuncioId1",
                table: "AnuncioCategoria",
                column: "AnuncioId1",
                principalTable: "Anuncio",
                principalColumn: "AnuncioId");
        }
    }
}
