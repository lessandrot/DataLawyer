﻿using System;
using System.Linq;
using DataLawyer.Dominio.Modelo;
using DataLawyer.Persistencia.Repositorio;
using DataLawyer.Rastreamento;

namespace DataLawyer.Servico
{
    public class ServicoDeRastreamento
    {
        private static ServicoDeRastreamento _instancia = null;
        public static ServicoDeRastreamento Instancia => _instancia ?? new ServicoDeRastreamento();
        private ServicoDeRastreamento() { }

        public ResultadoDeRastreio ObtenhaProcessoDoTJBA(string numeroDoProcesso)
        {
            try
            {
                var rastreador = new RastreadorTJBA(GrauDeProcesso.Segundo);
                var resultado = rastreador.Rastreie(numeroDoProcesso);
                return resultado;
            }
            catch (Exception ex)
            {
                ServicoDeLog.Instancia.Registre(ex.Message);
                return new ResultadoDeRastreio(ex.Message);
            }
        }

        public ResultadoDeRastreio GraveProcessoDoTJBA(string numeroDoProcesso)
        {
            try
            {
                var resultado = ObtenhaProcessoDoTJBA(numeroDoProcesso);

                if (resultado.Processo?.EhValido(false) ?? false)
                {
                    RepositorioDeProcesso.Instancia.Grave(resultado.Processo);

                    var movimentacoes = resultado.Movimentacoes.Where(m => m.EhValido(false));
                    if (movimentacoes.Any())
                    {
                        RepositorioDeMovimentacaoDeProcesso.Instancia.ExcluaTodas(resultado.Processo.Id);
                        foreach (var movimentacao in movimentacoes)
                        {                            
                            RepositorioDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);
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