using Microsoft.EntityFrameworkCore;
using DataLawyer.Dominio;
using DataLawyer.Nucleo;

namespace DataLawyer.Persistencia
{
    internal class Contexto : DbContext
    {
        public DbSet<LogDeErro> LogDeErro { get; set; }
        public DbSet<Processo> Processo { get; set; }
        public DbSet<MovimentacaoDeProcesso> MovimentacaoDeProcesso { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = base.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Processo>().HasKey(p => p.Id);
            modelBuilder.Entity<Processo>().Property("GrauId");
            modelBuilder.Entity<Processo>().Ignore(p => p.Grau);

            modelBuilder.Entity<MovimentacaoDeProcesso>().HasKey(m => m.Id);            
            modelBuilder.Entity<MovimentacaoDeProcesso>().Property("DataNumerica");
            modelBuilder.Entity<MovimentacaoDeProcesso>().Property("HoraNumerica");
            modelBuilder.Entity<MovimentacaoDeProcesso>().Ignore(m => m.Data);
            modelBuilder.Entity<MovimentacaoDeProcesso>().Ignore(m => m.Hora);
            modelBuilder.Entity<MovimentacaoDeProcesso>().HasOne(m => m.Processo);

            modelBuilder.Entity<LogDeErro>().HasKey(m => m.Id);
            modelBuilder.Entity<LogDeErro>().Property("DataNumerica");
            modelBuilder.Entity<LogDeErro>().Property("HoraNumerica");
            modelBuilder.Entity<LogDeErro>().Ignore(m => m.Data);
            modelBuilder.Entity<LogDeErro>().Ignore(m => m.Hora);
        }
    }
}