using System;

namespace DataLawyer.Nucleo
{
    public class LogDeErro
    {
        private LogDeErro() { }
        public LogDeErro(string mensagem)
        {
            Data = DateTime.Now;
            Hora = DateTime.Now.TimeOfDay;
            Mensagem = mensagem;
        }

        public int Id { get; set; }

        private int DataNumerica;
        public DateTime Data
        {
            get => DataNumerica.ParaData();
            private set => DataNumerica = value.ParaInteiro();
        }

        private int HoraNumerica;
        public TimeSpan Hora
        {
            get => HoraNumerica.ParaHora();
            private set => HoraNumerica = value.ParaInteiro();
        }

        private string _mensagem;
        public string Mensagem
        {
            get => _mensagem;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("A mensagem do LOG deve ser informada.");
                _mensagem = value.Trim();
            }
        }
    }
}
