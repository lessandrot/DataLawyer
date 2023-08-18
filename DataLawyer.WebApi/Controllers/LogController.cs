using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;

namespace DataLawyer.WebApi.Controllers;

public class LogController: BaseController
{
    [HttpGet("erros")]
    public IActionResult Get() => Execute(() => ServicoDeLog.Instancia.Obtenha());
}