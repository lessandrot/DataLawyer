using System;
using System.Text;

namespace DataLawyer.Dominio
{
    public class MovimentacaoDeProcesso
    {
        public MovimentacaoDeProcesso()
        {
            DataHora = DateTime.Now;
        }

        public MovimentacaoDeProcesso(Processo processo, string descricao)
        {
            Processo = processo;
            Descricao = descricao;
            DataHora = DateTime.Now;            
        }

        public int Id { get; set; }

        public Processo Processo { get; set; }

        private long DataHoraNumerica; // para persistência
        public DateTime DataHora
        {
            get => DataHoraNumerica.ParaDataHora();
            set => DataHoraNumerica = value.ParaInteiro();
        }

        public string Descricao { get; set; }

        public override bool Equals(object obj)
        {
            var movimentacao = (obj as MovimentacaoDeProcesso);
            return Processo.Equals(movimentacao?.Processo) && DataHora.Equals(movimentacao.DataHora);
        }

        public override int GetHashCode() => $"{Processo}.{DataHora}".GetHashCode();
        public override string ToString() => $"Processo {Processo} - {DataHora.ToShortDateTimeString()}";

        public bool EhValido(bool? dispareExcessao = true)
        {
            var erros = new StringBuilder();

            if (Processo is null) erros.AppendLine("O processo da movimentação deve ser informado.");

            Descricao = Descricao.FormateTexto();
            if (string.IsNullOrWhiteSpace(Descricao)) erros.AppendLine("A descrição da movimentação do processo deve ser informada.");

            var ehValido = erros.Length == 0;
            if (dispareExcessao == true && !ehValido) throw new Exception(erros.ToString());

            return ehValido;
        }
    }
}