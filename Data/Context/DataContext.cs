using Domain.Domain.Login;
using Microsoft.EntityFrameworkCore;

namespace Application.Context
{
    public class DataContext : DbContext
    {
        #region Construtor
        public DataContext() { }

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
