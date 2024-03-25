using Domain.Mensagem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class MensagemConfiguration : IEntityTypeConfiguration<Mensagem>
    {
        public void Configure(EntityTypeBuilder<Mensagem> builder)
        {
            builder.HasKey(i => new { i.UsuarioId, i.AnuncioId, i.SequenciaMensagem });

            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.AnuncioId).IsRequired();
            builder.Property(x => x.SequenciaMensagem).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Texto).IsRequired().HasMaxLength(128);
            builder.Property(x => x.DataCriacao).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.UsuarioId);

            builder.HasOne(x => x.Anuncio).WithMany().HasForeignKey(x => x.AnuncioId);
        }
    }
}
