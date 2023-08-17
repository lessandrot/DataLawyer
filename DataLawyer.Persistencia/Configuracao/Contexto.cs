using Microsoft.EntityFrameworkCore;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.Persistencia.Configuracao
{
    public class Contexto : DbContext
    {
        public Contexto() { }
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        public DbSet<LogDeErro> LogDeErro { get; set; }
        public DbSet<Processo> Processo { get; set; }
        public DbSet<MovimentacaoDeProcesso> MovimentacaoDeProcesso { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = base.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(Contexto).Assembly);
        }
    }
}