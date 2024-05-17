using Domain.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(i => i.UsuarioId);

            builder.Property(x => x.UsuarioId).IsRequired();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(60);
            builder.Property(x => x.DataNascimento).IsRequired();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(60);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(64);

            builder.HasMany(x => x.Anuncios).WithOne().HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.Mensagens).WithOne().HasForeignKey(x => x.UsuarioId);

            builder.HasMany(x => x.Interesses).WithOne().HasForeignKey(x => x.UsuarioId);
        }
    }
}
