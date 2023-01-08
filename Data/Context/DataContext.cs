using Domain.Domain.Login;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Context
{
    public class DataContext : DbContext
    {
        #region Construtor
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        #endregion

        #region Métodos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }
        #endregion

        #region Objetos
        public DbSet<Login> Login { get; set; }
        #endregion
    }
}
