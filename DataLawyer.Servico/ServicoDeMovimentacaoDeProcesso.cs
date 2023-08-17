using System.Collections.Generic;
using DataLawyer.Dominio.Modelo;
using DataLawyer.Persistencia.Repositorio;

namespace DataLawyer.Servico
{
    public class ServicoDeMovimentacaoDeProcesso
    {
        private static ServicoDeMovimentacaoDeProcesso _instancia = null;
        public static ServicoDeMovimentacaoDeProcesso Instancia => _instancia ?? new ServicoDeMovimentacaoDeProcesso();
        private ServicoDeMovimentacaoDeProcesso() { }

        private RepositorioDeMovimentacaoDeProcesso _persistencia = RepositorioDeMovimentacaoDeProcesso.Instancia;

        public IEnumerable<MovimentacaoDeProcesso> Obtenha(int processoId) => _persistencia.Obtenha(processoId);

        public void Grave(MovimentacaoDeProcesso movimentacao) => _persistencia.Grave(movimentacao);

        public void Exclua(int id) => _persistencia.Exclua(id);
    }
}