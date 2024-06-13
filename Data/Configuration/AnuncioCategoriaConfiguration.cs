using Domain.AnuncioCategoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class AnuncioCategoriaConfiguration : IEntityTypeConfiguration<AnuncioCategoria>
    {
        public void Configure(EntityTypeBuilder<AnuncioCategoria> builder)
        {
            builder.HasKey(i => new { i.AnuncioId, i.CategoriaId });

            builder.Property(x => x.AnuncioId).IsRequired();
            builder.Property(x => x.CategoriaId).IsRequired();

            builder.HasOne(x => x.Anuncio).WithMany(x => x.AnuncioCategorias).HasForeignKey(x => x.AnuncioId);

            builder.HasOne(x => x.Categoria).WithMany(x => x.AnunciosCategorias).HasForeignKey(x => x.CategoriaId);
        }
    }
}
