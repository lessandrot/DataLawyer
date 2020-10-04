using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLawyer.Dominio;
using DataLawyer.Servico;

namespace DataLawyer.Api.Controllers
{
    [Route("api/movimentacoes")]
    public class MovimentacaoDeProcessoController : BaseController
    {
        [HttpGet, Route("{processoId:int}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoDeProcesso>>> Get(int processoId)
            => await Execute(() => ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processoId));

        [HttpPut]
        public async Task<ActionResult<MovimentacaoDeProcesso>> Put([FromBody] MovimentacaoDeProcesso movimentacao)
            => await Execute(() => ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao));

        [HttpDelete, Route("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
            => await Execute(() => ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(id));
    }
}