using Domain.Domain.Login;
using Domain.Domain.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configuration
{
    public class LoginConfiguration : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.HasKey(i => i.LoginId);

            builder.Property(x => x.LoginId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(50);

            builder.HasOne(x => x.Usuario).WithOne().HasForeignKey<Usuario>(x => x.UsuarioId);
        }
    }
}
