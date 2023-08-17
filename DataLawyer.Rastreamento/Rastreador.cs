using System;
using System.Web;
using System.Linq;
using HtmlAgilityPack;
using System.Collections.Generic;
using DataLawyer.Dominio;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.Rastreamento
{
    public abstract class Rastreador
    {
        private HtmlWeb _htmlWeb = new HtmlWeb();

        public Rastreador(GrauDeProcesso grau) => Grau = grau;

        public GrauDeProcesso Grau { get; set; }

        public ResultadoDeRastreio Rastreie(string numeroDoProcesso)
        {
            var resultado = new ResultadoDeRastreio();
            var processo = $"O processo '{numeroDoProcesso}' de { Grau.ToDescription().ToLower()}";

            try
            {
                var uri = Uri(numeroDoProcesso);
                var html = _htmlWeb.Load(uri);

                resultado.Processo = Obtenha(numeroDoProcesso, html);

                if (!resultado.Processo?.EhValido(false) ?? true)
                {
                    resultado.Adicione($"{processo} não foi encontrado.");
                    return resultado;
                }

                resultado.Adicione($"{processo} foi encontrado com sucesso.");

                resultado.Movimentacoes = ObtenhaMovimentacoes(resultado.Processo, html);
                if (resultado.Movimentacoes.Any(m => m.EhValido(false)))
                {
                    resultado.Adicione($"Este processo possui movimentação. Total de {resultado.Movimentacoes.Count(m => m.EhValido())}.");
                }
                else
                {
                    resultado.Adicione($"Não foi encontrado movimentações para este processo.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Adicione(ex.Message);
                return resultado;
            }
        }

        protected string Texto(HtmlDocument html, string xpath)
        {
            try
            {
                var texto = Texto(html.DocumentNode.SelectSingleNode(xpath)?.InnerText);
                return texto;
            }
            catch { return null; }
        }

        protected string Texto(string valor)
        {
            try
            {
                var texto = HttpUtility.HtmlDecode(valor);
                return texto.FormateTexto();
            }
            catch { return null; }
        }

        protected abstract Uri Uri(string numeroDoProcesso);
        protected abstract Processo Obtenha(string numeroDoProcesso, HtmlDocument html);
        protected abstract IEnumerable<MovimentacaoDeProcesso> ObtenhaMovimentacoes(Processo processo, HtmlDocument html);
    }
}