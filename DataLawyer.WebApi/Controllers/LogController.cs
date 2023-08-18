using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;

namespace DataLawyer.WebApi.Controllers;

public class LogController: BaseController
{
    [HttpGet("erros")]
    public IActionResult ObtenhaLogDeErros() => Execute(() => ServicoDeLog.Instancia.Obtenha());
}