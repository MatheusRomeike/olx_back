﻿using Domain.FotoAnuncio;
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
            builder.Property(x => x.SequenciaFotoAnuncio).IsRequired();

            builder.HasOne(x => x.Anuncio).WithMany(x => x.FotosAnuncio).HasForeignKey(x => x.AnuncioId);
        }
    }
}
