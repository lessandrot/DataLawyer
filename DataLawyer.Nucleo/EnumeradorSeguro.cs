using System;

namespace DataLawyer.Nucleo
{
    public abstract class EnumeradorSeguro<T>
    {
        public int Id { get; set; }

        private string _descricao;
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new Exception("A descrição do enumerador deve ser informada.");
                _descricao = value.Trim();
            }
        }

        public EnumeradorSeguro(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }     

        public override bool Equals(object obj) => Id == ((obj as EnumeradorSeguro<T>)?.Id);
        public override int GetHashCode() => Id.GetHashCode();
        public override string ToString() => Descricao;
    }
}