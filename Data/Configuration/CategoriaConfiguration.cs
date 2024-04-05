using Domain.Categoria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(i => i.CategoriaId);

            builder.Property(x => x.CategoriaId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(60);

            builder.HasMany(x => x.AnunciosCategorias).WithOne().HasForeignKey(x => x.CategoriaId);
        }
    }
}
