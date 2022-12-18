using Domain.Domain.Login;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Pessoa;

namespace Data.Configuration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("PESSOA");

            builder.HasKey(x => new { x.PessoaId, x.CpfCnpj });

            builder.Property(x => x.PessoaId).ValueGeneratedOnAdd().HasColumnName("PESSOAID");
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(50).HasColumnName("NOME");
            builder.Property(x => x.CpfCnpj).IsRequired().HasMaxLength(14).HasColumnName("CPFCNPJ");
            builder.Property(x => x.TipoPessoa).IsRequired().HasColumnName("TIPOPESSOA");
            builder.Property(x => x.DataNascimento).IsRequired().HasColumnName("DATANASCIMENTO");
            builder.Property(x => x.DataCadastro).IsRequired().HasColumnName("DATACADASTRO");
            builder.Property(x => x.DataAlteracao).IsRequired().HasColumnName("DATAALTERACAO");
        }
    }
}
