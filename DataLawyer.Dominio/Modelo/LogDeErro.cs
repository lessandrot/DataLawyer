using System;

namespace DataLawyer.Dominio.Modelo
{
    public class LogDeErro
    {
        private LogDeErro() { }
        public LogDeErro(string mensagem)
        {
            DataHora = DateTime.Now;
            Mensagem = mensagem;
        }

        public int Id { get; set; }

        private long DataHoraNumerica; // para persistência
        public DateTime DataHora
        {
            get => DataHoraNumerica.ParaDataHora();
            private set => DataHoraNumerica = value.ParaInteiro();
        }

        private string _mensagem;
        public string Mensagem
        {
            get => _mensagem;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("A mensagem de LOG deve ser informada.");
                _mensagem = value.Trim();
            }
        }
    }
}