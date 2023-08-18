using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.WebApi.Controllers;

public class MovimentosController: BaseController
{
    [HttpGet("{processoId}")]
    public IActionResult Get(int processoId) => Execute(() => ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processoId));

    [HttpPut]
    public IActionResult Put(MovimentacaoDeProcesso movimentacao)
    {
        return Execute(() =>
        {
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);
            return movimentacao;
        });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) => Execute(() => ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(id));
}