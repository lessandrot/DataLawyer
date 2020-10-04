using Microsoft.EntityFrameworkCore;
using DataLawyer.Dominio;

namespace DataLawyer.Persistencia
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Processo>().HasKey(p => p.Id);            

            modelBuilder.Entity<MovimentacaoDeProcesso>().HasKey(m => m.Id);
            modelBuilder.Entity<MovimentacaoDeProcesso>().HasOne(m => m.Processo);
            modelBuilder.Entity<MovimentacaoDeProcesso>().Property("DataHoraNumerica");            
            modelBuilder.Entity<MovimentacaoDeProcesso>().Ignore(m => m.DataHora);           
            
            modelBuilder.Entity<LogDeErro>().HasKey(m => m.Id);
            modelBuilder.Entity<LogDeErro>().Property("DataHoraNumerica");            
            modelBuilder.Entity<LogDeErro>().Ignore(m => m.DataHora);            
        }
    }
}