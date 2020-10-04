using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;
using DataLawyer.Dominio;

namespace DataLawyer.Api.Controllers
{
    [Route("api/logs")]
    public class LogController : BaseController
    {
        [HttpGet, Route("erros")]
        public async Task<ActionResult<IEnumerable<LogDeErro>>> Get() 
            => await Execute(() => ServicoDeLog.Instancia.Obtenha());
    }
}