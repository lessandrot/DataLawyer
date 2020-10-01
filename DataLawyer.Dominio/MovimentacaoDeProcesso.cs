using System;
using DataLawyer.Nucleo;

namespace DataLawyer.Dominio
{
    public class MovimentacaoDeProcesso
    {
        private MovimentacaoDeProcesso() { }

        public MovimentacaoDeProcesso(Processo processo, DateTime data, string descricao)
        {
            Data = data;            
            Descricao = descricao;
            Processo = processo;
        }

        public MovimentacaoDeProcesso(Processo processo, DateTime data, TimeSpan hora, string descricao)
        {
            Data = data;
            Hora = hora;
            Descricao = descricao;
            Processo = processo;
        }

        public int Id { get; set; }
        
        private Processo _processo;
        public Processo Processo
        {
            get => _processo;
            set => _processo = value ?? throw new Exception("O processo da movimentação deve ser informado.");
        }

        private int DataNumerica;
        public DateTime Data
        {
            get => DataNumerica.ParaData();
            set => DataNumerica = value.ParaInteiro();
        }

        private int HoraNumerica;
        public TimeSpan Hora
        {
            get => HoraNumerica.ParaHora();
            set => HoraNumerica = value.ParaInteiro();
        }

        private string _descricao;
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("A descrição da movimentação do processo deve ser informada.");
                _descricao = value.Trim();
            }
        }        

        public override bool Equals(object obj)
        {
            var movimentacao = (obj as MovimentacaoDeProcesso);
            return Processo.Equals(movimentacao?.Processo) && Data.Equals(movimentacao.Data) && Hora.Equals(movimentacao.Hora);
        }

        public override int GetHashCode() => $"{Processo}.{Data}.{Hora}".GetHashCode();
        public override string ToString() => $"Processo {Processo} - {Data.ToShortDateString()} {Hora.ToShortTimeString()} - {Descricao}";
    }
}