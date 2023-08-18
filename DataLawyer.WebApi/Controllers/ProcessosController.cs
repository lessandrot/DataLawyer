using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.WebApi.Controllers;

public class ProcessosController: BaseController
{    
    public IActionResult Get() => Execute(() => ServicoDeProcesso.Instancia.Obtenha());

    [HttpGet, Route("{id}")]
    public IActionResult Get(int id) => Execute(() => ServicoDeProcesso.Instancia.Obtenha(id));

    [HttpPut]
    public IActionResult Put(Processo processo)
    {
        return Execute(() =>
        {
            ServicoDeProcesso.Instancia.Grave(processo);
            return processo;
        });
    }

    [HttpDelete, Route("{id}")]
    public IActionResult Delete(int id) => Execute(() => ServicoDeProcesso.Instancia.Exclua(id));
}