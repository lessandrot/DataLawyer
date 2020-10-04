using System;
using System.Linq;
using DataLawyer.Dominio;
using DataLawyer.Persistencia;
using DataLawyer.Rastreamento;

namespace DataLawyer.Servico
{
    public class ServicoDeRastreamento
    {
        private static ServicoDeRastreamento _instancia = null;
        public static ServicoDeRastreamento Instancia => _instancia ?? new ServicoDeRastreamento();
        private ServicoDeRastreamento() { }

        public ResultadoDeRastreio RastreieTJBA(string numeroDoProcesso, GrauDeProcesso grau)
        {
            try
            {
                var rastreador = new RastreadorTJBA(grau);
                var resultado = rastreador.Rastreie(numeroDoProcesso);

                if (resultado.Processo?.EhValido(false) ?? false)
                {
                    PersistenciaDeProcesso.Instancia.Grave(resultado.Processo);

                    var movimentacoes = resultado.Movimentacoes.Where(m => m.EhValido(false));
                    if (movimentacoes.Any())
                    {
                        PersistenciaDeMovimentacaoDeProcesso.Instancia.ExcluaTodas(resultado.Processo.Id);
                        foreach (var movimentacao in movimentacoes)
                        {                            
                            PersistenciaDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);
                        }
                    }
                }

                return resultado;
            }
            catch (Exception ex)
            {
                ServicoDeLog.Instancia.Registre(ex.Message);
                return new ResultadoDeRastreio(ex.Message);
            }
        }
    }
}