using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.WebApi.Controllers;

public class RastreamentosController: BaseController
{
    [HttpGet("tjba/segundograu/{numeroDoProcesso}")]
    public IActionResult Get_TJBA_2G(string numeroDoProcesso) 
        => Execute(() => ServicoDeRastreamento.Instancia.ObtenhaTJBA(numeroDoProcesso, GrauDeProcesso.Segundo).Processo);

    [HttpGet("tjba/segundograu/movimentos/{numeroDoProcesso}")]
    public IActionResult Get_TJBA_2G_Movimentos(string numeroDoProcesso) => Execute(() 
        => ServicoDeRastreamento.Instancia.ObtenhaTJBA(numeroDoProcesso, GrauDeProcesso.Segundo).Movimentacoes);

    [HttpPut("tjba/segundograu/{numeroDoProcesso}")]
    public IActionResult Put_TJBA_2G(string numeroDoProcesso) => Execute(() => ServicoDeRastreamento.Instancia.GraveTJBA(numeroDoProcesso, GrauDeProcesso.Segundo).Mensagens);
}