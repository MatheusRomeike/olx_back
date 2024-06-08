using Data.Configuration;
using Domain.Anuncio;
using Domain.AnuncioCategoria;
using Domain.Categoria;
using Domain.FotoAnuncio;
using Domain.Interesse;
using Domain.Mensagem;
using Domain.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
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
#if DEBUG
            DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env.local"));
#else
            DotNetEnv.Env.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));
#endif
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new AnuncioConfiguration());
            modelBuilder.ApplyConfiguration(new AnuncioCategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new FotoAnuncioConfiguration());
            modelBuilder.ApplyConfiguration(new InteresseConfiguration());
            modelBuilder.ApplyConfiguration(new MensagemConfiguration());
        }
        #endregion

        #region Objetos
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Anuncio> Anuncio { get; set; }
        public DbSet<AnuncioCategoria> AnuncioCategoria { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<FotoAnuncio> FotoAnuncio { get; set; }
        public DbSet<Interesse> Interesse { get; set; }
        public DbSet<Mensagem> Mensagem { get; set; }
        #endregion
    }
}
