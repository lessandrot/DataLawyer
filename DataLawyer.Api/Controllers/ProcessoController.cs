using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLawyer.Dominio;
using DataLawyer.Servico;

namespace DataLawyer.Api.Controllers
{
    [Route("api/processos")]
    public class ProcessoController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Processo>>> Get() 
            => await Execute(() => ServicoDeProcesso.Instancia.Obtenha());

        [HttpGet, Route("{id:int}")]
        public async Task<ActionResult<Processo>> Get(int id) 
            => await Execute(() => ServicoDeProcesso.Instancia.Obtenha(id));

        [HttpPut]
        public async Task<ActionResult<Processo>> Put([FromBody] Processo processo)
        {
            return await Execute(() =>
            {
                ServicoDeProcesso.Instancia.Grave(processo);
                return processo;
            });
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<ActionResult> Delete(int id)
            => await Execute(() => ServicoDeProcesso.Instancia.Exclua(id));
    }
}