using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;

namespace DataLawyer.WebApi.Controllers;
public class RastreamentosController: BaseController
{
    [HttpGet("tjba/segundograu/{numeroDoProcesso}")]
    public IActionResult ObtenhaProcessoDoTJBA(string numeroDoProcesso) 
        => Execute(() => ServicoDeRastreamento.Instancia.ObtenhaProcessoDoTJBA(numeroDoProcesso).Processo);

    [HttpGet("tjba/segundograu/movimentos/{numeroDoProcesso}")]
    public IActionResult ObtenhaMovimentosDoTJBA(string numeroDoProcesso) => Execute(() 
        => ServicoDeRastreamento.Instancia.ObtenhaProcessoDoTJBA(numeroDoProcesso).Movimentacoes);

    [HttpPut("tjba/segundograu/{numeroDoProcesso}")]
    public IActionResult GraveProcessoDoTJBA(string numeroDoProcesso) => Execute(() 
        => ServicoDeRastreamento.Instancia.GraveProcessoDoTJBA(numeroDoProcesso).Mensagens);
}