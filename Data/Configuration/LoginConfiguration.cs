using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Login;

namespace Data.Configuration
{
    public class LoginConfiguration : IEntityTypeConfiguration<Login>
    {
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.ToTable("LOGIN");

            builder.HasKey(i => i.LoginId);

            builder.Property(x => x.LoginId).ValueGeneratedOnAdd().HasColumnName("LOGINID");
            builder.Property(x => x.Usuario).IsRequired().HasMaxLength(50).HasColumnName("USUARIO");
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(50).HasColumnName("SENHA");
            builder.Property(x => x.TipoLogin).IsRequired().HasColumnName("TIPOUSUARIO");
            builder.Property(x => x.DataCadastro).IsRequired().HasColumnName("DATACADASTRO");
            builder.Property(x => x.DataAlteracao).IsRequired().HasColumnName("DATAALTERACAO");
        }
    }
}
