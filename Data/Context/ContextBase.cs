using Domain.Domain.Login;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Context
{
    public class ContextBase : DbContext
    {
        #region Construtor
        public ContextBase() { }

        public ContextBase(DbContextOptions<ContextBase> opcoes) : base(opcoes) { }
        #endregion

        #region Objetos
        public DbSet<Login> Login { get; set; }
        #endregion

        #region Métodos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = new NpgsqlConnectionStringBuilder()
                {
                    Host = "localhost",
                    Port = 5432,
                    Database = "postgres",
                    Username = "postgres",
                    Password = "0911",
                    Pooling = true
                }.ToString();

                optionsBuilder.UseNpgsql(connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }
        #endregion
    }
}
