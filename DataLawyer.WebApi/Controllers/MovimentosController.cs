using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.WebApi.Controllers;

public class MovimentosController: BaseController
{
    [HttpGet("{processoId}")]
    public IActionResult ObtenhaMovimentos(int processoId) => Execute(() => ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processoId));

    [HttpPut]
    public IActionResult GraveMovimento(MovimentacaoDeProcesso movimentacao)
    {
        return Execute(() =>
        {
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);
            return movimentacao;
        });
    }

    [HttpDelete("{id}")]
    public IActionResult ExcluaMovimento(int id) => Execute(() => ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(id));
}