using System;
using System.Text;

namespace DataLawyer.Dominio.Modelo
{
    public class Processo
    {
        public Processo() { }

        public Processo(string numero, GrauDeProcesso grau)
        {
            Numero = numero;
            Grau = grau;
        }

        public int Id { get; set; }
        public string Numero { get; set; }
        public GrauDeProcesso Grau { get; set; }

        public string Classe { get; set; }
        public string Area { get; set; }
        public string Assunto { get; set; }
        public string Origem { get; set; }
        public string Distribuicao { get; set; }
        public string Relator { get; set; }

        public override bool Equals(object obj) => Numero.Equals((obj as Processo)?.Numero);
        public override int GetHashCode() => Numero.GetHashCode();
        public override string ToString() => Numero;

        public bool EhValido(bool? dispareExcessao = true)
        {
            var erros = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Numero)) erros.AppendLine("O número do processo deve ser informado.");
            else
            {
                Numero = Numero.SomenteNumeros();
                if (Numero.Length != 20) erros.AppendLine("O número do processo deve estar no padrão CNJ.");

                Classe = Classe.FormateTexto();
                if (string.IsNullOrWhiteSpace(Classe)) erros.AppendLine("A classe do processo deve ser informada.");

                Area = Area.FormateTexto();
                if (string.IsNullOrWhiteSpace(Area)) erros.AppendLine("A área do processo deve ser informada.");

                Assunto = Assunto.FormateTexto();
                if (string.IsNullOrWhiteSpace(Assunto)) erros.AppendLine("O assunto do processo deve ser informado.");

                Origem = Origem.FormateTexto();
                if (string.IsNullOrWhiteSpace(Origem)) erros.AppendLine("A Origem do processo deve ser informada.");

                Distribuicao = Distribuicao.FormateTexto();
                if (string.IsNullOrWhiteSpace(Distribuicao)) erros.AppendLine("A distribuição do processo deve ser informada.");

                Relator = Relator.FormateTexto();
                if (string.IsNullOrWhiteSpace(Relator)) erros.AppendLine("O relator do processo deve ser informado.");
            }

            var ehValido = erros.Length == 0;
            if (dispareExcessao == true && !ehValido) throw new ApplicationException(erros.ToString());

            return ehValido;
        }
    }
}