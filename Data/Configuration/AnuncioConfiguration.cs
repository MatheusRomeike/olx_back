using Domain.Anuncio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class AnuncioConfiguration : IEntityTypeConfiguration<Anuncio>
    {
        public void Configure(EntityTypeBuilder<Anuncio> builder)
        {
            builder.HasKey(i => new { i.UsuarioId, i.AnuncioId });

            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.AnuncioId).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Preco).IsRequired();
            builder.Property(x => x.EstadoAnuncio).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.FotosAnuncio).WithOne().HasForeignKey(x => x.AnuncioId);

            builder.HasMany(x => x.Mensagens).WithOne().HasForeignKey(x => x.AnuncioId);

            builder.HasMany(x => x.Interesses).WithOne().HasForeignKey(x => x.AnuncioId);
        }
    }
}
