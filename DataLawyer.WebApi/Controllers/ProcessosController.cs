using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.WebApi.Controllers;

public class ProcessosController: BaseController
{    
    public IActionResult ObtenhaProcessos() => Execute(() => ServicoDeProcesso.Instancia.Obtenha());

    [HttpGet, Route("{id}")]
    public IActionResult ObtenhaProcesso(int id) => Execute(() => ServicoDeProcesso.Instancia.Obtenha(id));

    [HttpPut]
    public IActionResult GraveProcesso(Processo processo)
    {
        return Execute(() =>
        {
            ServicoDeProcesso.Instancia.Grave(processo);
            return processo;
        });
    }

    [HttpDelete, Route("{id}")]
    public IActionResult ExcluaProcesso(int id) => Execute(() => ServicoDeProcesso.Instancia.Exclua(id));
}