using System;
using DataLawyer.Nucleo;

namespace DataLawyer.Dominio
{
    public class Processo
    {
        private Processo() { }
        public Processo(string numero, GrauDeProcesso grau)
        {
            Numero = numero;
            Grau = grau;
        }

        public int Id { get; set; }

        private string _numero;
        public string Numero
        {
            get => _numero;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("O número do processo deve ser informado.");

                _numero = value.SomenteNumeros();
                if (_numero.Length != 20) throw new Exception("O número do processo deve estar no padrão CNJ.");
            }
        }

        private int GrauId;
        public GrauDeProcesso Grau
        {
            get => GrauDeProcesso.Obtenha(GrauId);
            set => GrauId = value?.Id ?? throw new Exception("O grau do processo deve ser informado.");
        }

        private string _classe;
        public string Classe { get => _classe; set => _classe = value?.Trim(); }

        private string _area;
        public string Area { get => _area; set => _area = value?.Trim(); }

        private string _assunto;
        public string Assunto { get => _assunto; set => _assunto = value?.Trim(); }

        private string _origem;
        public string Origem { get => _origem; set => _origem = value?.Trim(); }

        private string _distribuicao;
        public string Distribuicao { get => _distribuicao; set => _distribuicao = value?.Trim(); }

        private string _relator;
        public string Relator { get => _relator; set => _relator = value?.Trim(); }

        public override bool Equals(object obj) => Numero.Equals((obj as Processo)?.Numero);
        public override int GetHashCode() => Numero.GetHashCode();
        public override string ToString() => Numero;
    }
}