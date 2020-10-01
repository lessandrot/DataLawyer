using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLawyer.Dominio;
using HtmlAgilityPack;

namespace DataLawyer.Rastreamento
{
    public class RastreadorTJBA : Rastreador
    {
        protected override Processo Obtenha(string numeroDoProcesso, GrauDeProcesso grau, HtmlDocument html)
        {
            var processo = new Processo(numeroDoProcesso, grau)
            {
                Classe = Texto(html, "//table[4]/tr/td/table[2]/tr[2]/td[2]/table/tr/td/span/span"),
                Area = Texto(html, "(//table[4]/tr/td/table[2]/tr[3]/td[2]/table/tr/td/text())[last()]"),
                Assunto = Texto(html, "//table[4]/tr/td/table[2]/tr[4]/td[2]/span"),
                Origem = Texto(html, "(//table[4]/tr/td/table[2]/tr[5]/td[2]/span/text())[last()]"),
                Distribuicao = Texto(html, "//table[4]/tr/td/table[2]/tr[7]/td[2]/span"),
                Relator = Texto(html, "//table[4]/tr/td/table[2]/tr[8]/td[2]/span")
            };

            return processo;
        }

        protected override IEnumerable<MovimentacaoDeProcesso> ObtenhaMovimentacoes(Processo processo, HtmlDocument html)
        {
            var datas = html.DocumentNode.SelectNodes("//table[@id='tabelaUltimasMovimentacoes']/tr/td[1]")?
                                          .Select(n => HttpUtility.HtmlDecode(n.InnerText)) ?? new List<string>();

            var descricoes = html.DocumentNode.SelectNodes("//table[@id='tabelaUltimasMovimentacoes']/tr/td[3]")?
                                              .Select(n => HttpUtility.HtmlDecode(n.InnerText)) ?? new List<string>();

            var movimentacoes = datas.Zip(descricoes).Select(i =>
            {
                return new MovimentacaoDeProcesso(processo, DateTime.Parse(i.First), i.Second);
            });

            return movimentacoes;
        }

        protected override Uri Uri(string numeroDoProcesso, GrauDeProcesso grau)
        {
            var builder = new UriBuilder("http://esaj.tjba.jus.br/cpo/sg/search.do");

            var query = HttpUtility.ParseQueryString(builder.Query);
            query["paginaConsulta"] = "1";
            query["cbPesquisa"] = "NUMPROC";
            query["tipoNuProcesso"] = "SAJ";
            query["numeroDigitoAnoUnificado"] = string.Empty;
            query["foroNumeroUnificado"] = string.Empty;
            query["dePesquisaNuUnificado"] = string.Empty;
            query["dePesquisa"] = numeroDoProcesso;
            builder.Query = query.ToString();

            return new Uri(builder.ToString());
        }
    }
}
