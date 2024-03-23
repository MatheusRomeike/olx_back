using Domain.Domain.FotoAnuncio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class FotoAnuncioConfiguration : IEntityTypeConfiguration<FotoAnuncio>
    {
        public void Configure(EntityTypeBuilder<FotoAnuncio> builder)
        {
            builder.HasKey(i => new { i.AnuncioId, i.SequenciaFotoAnuncio });

            builder.Property(x => x.AnuncioId).IsRequired();
            builder.Property(x => x.SequenciaFotoAnuncio).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Foto).IsRequired().HasMaxLength(512);

            builder.HasOne(x => x.Anuncio).WithMany().HasForeignKey(x => x.AnuncioId);
        }
    }
}
