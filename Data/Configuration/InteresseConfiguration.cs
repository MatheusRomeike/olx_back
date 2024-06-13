using Domain.Interesse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class InteresseConfiguration : IEntityTypeConfiguration<Interesse>
    {
        public void Configure(EntityTypeBuilder<Interesse> builder)
        {
            builder.HasKey(i => new { i.UsuarioId, i.AnuncioId });

            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.AnuncioId).IsRequired();
            builder.Property(x => x.DataCriacao).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany(x => x.Interesses).HasForeignKey(x => x.UsuarioId);

            builder.HasOne(x => x.Anuncio).WithMany(x => x.Interesses).HasForeignKey(x => x.AnuncioId);
        }
    }
}
