using Domain.Domain.Login;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public string ObterStringConexao()
        {
            return "Data Source=MATHEUS;Initial Catalog=BaseApi;Integrated Security=True;MultipleActiveResultSets=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ObterStringConexao());
                base.OnConfiguring(optionsBuilder);
            }
        }
        #endregion
    }
}
