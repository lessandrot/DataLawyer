using DataLawyer.Dominio.Modelo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLawyer.Persistencia.Configuracao
{
    internal class ConfiguracaoDeMovimentacao: IEntityTypeConfiguration<MovimentacaoDeProcesso>
    {
        public void Configure(EntityTypeBuilder<MovimentacaoDeProcesso> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasOne(m => m.Processo);
            builder.Property("DataHoraNumerica");
            builder.Ignore(m => m.DataHora);
        }
    }
}