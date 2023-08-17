using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLawyer.Servico;
using DataLawyer.Rastreamento;
using System.Collections.Generic;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.Api.Controllers
{
    [Route("api/rastreamentos")]
    public class RastreamentoController : BaseController
    {
        [HttpGet, Route("tjba/segundograu/{numeroDoProcesso}")]
        public async Task<ActionResult<Processo>> Get(string numeroDoProcesso)
            => await Execute(() => new RastreadorTJBA(GrauDeProcesso.Segundo).Rastreie(numeroDoProcesso).Processo);

        [HttpGet, Route("tjba/segundograu/movimentacoes/{numeroDoProcesso}")]
        public async Task<ActionResult<IEnumerable<MovimentacaoDeProcesso>>> GetProgress(string numeroDoProcesso)
            => await Execute(() => new RastreadorTJBA(GrauDeProcesso.Segundo).Rastreie(numeroDoProcesso).Movimentacoes);

        [HttpPost, Route("tjba/segundograu/{numeroDoProcesso}")]
        public async Task<ActionResult<IEnumerable<string>>> Post(string numeroDoProcesso)
            => await Execute(() => ServicoDeRastreamento.Instancia.RastreieTJBA(numeroDoProcesso, GrauDeProcesso.Segundo).Mensagens);
    }
}