using System;
using HtmlAgilityPack;
using DataLawyer.Dominio;
using DataLawyer.Servico;
using System.Collections.Generic;
using System.Web;

namespace DataLawyer.Rastreamento
{
    public abstract class Rastreador
    {
        protected HtmlWeb _htmlWeb = new HtmlWeb();        

        public void Rastreie(string numeroDoProcesso, GrauDeProcesso grau)
        {
            try
            {
                var uri = Uri(numeroDoProcesso, grau);
                var html = _htmlWeb.Load(uri);

                var processo = Obtenha(numeroDoProcesso, grau,html);                
                processo = ServicoDeProcesso.Instancia.Grave(processo);

                var movimentacoes = ObtenhaMovimentacoes(processo, html);
                foreach(var movimentacao  in movimentacoes)
                {
                    ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);
                }                
            }
            catch(Exception ex) 
            {
                ServicoDeLog.Instancia.Registre(ex.Message);
            }
        }

        protected string Texto(HtmlDocument html, string xpath)
        {
            try
            {
                var texto = HttpUtility.HtmlDecode(html.DocumentNode.SelectSingleNode(xpath)?.InnerText);
                return texto;
            }
            catch
            {
                return null;
            }
        }

        protected abstract Uri Uri(string numeroDoProcesso, GrauDeProcesso grau);
        protected abstract Processo Obtenha(string numeroDoProcesso, GrauDeProcesso grau, HtmlDocument html);
        protected abstract IEnumerable<MovimentacaoDeProcesso> ObtenhaMovimentacoes(Processo processo, HtmlDocument html);
    }
}    
