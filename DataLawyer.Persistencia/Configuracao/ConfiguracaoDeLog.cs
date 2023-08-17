using DataLawyer.Dominio.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLawyer.Persistencia.Configuracao
{
    internal class ConfiguracaoDeLog: IEntityTypeConfiguration<LogDeErro>
    {
        public void Configure(EntityTypeBuilder<LogDeErro> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property("DataHoraNumerica");
            builder.Ignore(m => m.DataHora);
        }
    }
}