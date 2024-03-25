using Domain.UsuarioRelatorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class UsuarioRelatorioConfiguration : IEntityTypeConfiguration<UsuarioRelatorio>
    {
        public void Configure(EntityTypeBuilder<UsuarioRelatorio> builder)
        {
            builder.HasKey(i => new { i.UsuarioId, i.SequenciaRelatorio });

            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.SequenciaRelatorio).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Descricao).IsRequired().HasMaxLength(60);
            builder.Property(x => x.DataCadastro).IsRequired();

            builder.HasOne(x => x.Usuario).WithMany().HasForeignKey(x => x.UsuarioId);
        }
    }
}
