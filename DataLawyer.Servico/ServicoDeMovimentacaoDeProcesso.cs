using System.Collections.Generic;
using DataLawyer.Dominio;
using DataLawyer.Persistencia;

namespace DataLawyer.Servico
{
    public class ServicoDeMovimentacaoDeProcesso
    {
        private static ServicoDeMovimentacaoDeProcesso _instancia = null;
        public static ServicoDeMovimentacaoDeProcesso Instancia => _instancia ?? new ServicoDeMovimentacaoDeProcesso();
        private ServicoDeMovimentacaoDeProcesso() { }

        private PersistenciaDeMovimentacaoDeProcesso _persistencia = PersistenciaDeMovimentacaoDeProcesso.Instancia;

        public IEnumerable<MovimentacaoDeProcesso> Obtenha(Processo processo) => _persistencia.Obtenha(processo);

        public void Grave(MovimentacaoDeProcesso movimentacao) => _persistencia.Grave(movimentacao);

        public void Exclua(MovimentacaoDeProcesso movimentacao) => _persistencia.Exclua(movimentacao);
    }
}