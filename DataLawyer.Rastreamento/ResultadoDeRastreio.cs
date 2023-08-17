using DataLawyer.Dominio.Modelo;
using System.Collections.Generic;

namespace DataLawyer.Rastreamento
{
    public class ResultadoDeRastreio
    {
        public ResultadoDeRastreio() { }
        public ResultadoDeRastreio(string mensagem) => _mensagens.Add(mensagem);

        public Processo Processo { get; internal set; }

        private List<MovimentacaoDeProcesso> _movimentacoes = new List<MovimentacaoDeProcesso>();
        public IEnumerable<MovimentacaoDeProcesso> Movimentacoes
        {
            get => _movimentacoes;
            set
            {
                _movimentacoes.Clear();
                if (value != null) _movimentacoes.AddRange(value);
            }
        }

        private List<string> _mensagens = new List<string>();
        public IEnumerable<string> Mensagens => _mensagens;
        public void Adicione(string mensagem) => _mensagens.Add(mensagem);
    }
}