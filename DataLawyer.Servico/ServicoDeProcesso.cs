using System.Collections.Generic;
using DataLawyer.Dominio;
using DataLawyer.Persistencia;

namespace DataLawyer.Servico
{
    public class ServicoDeProcesso
    {
        private static ServicoDeProcesso _instancia = null;
        public static ServicoDeProcesso Instancia => _instancia ?? new ServicoDeProcesso();
        private ServicoDeProcesso() { }

        private PersistenciaDeProcesso _persistencia = PersistenciaDeProcesso.Instancia;

        public Processo Obtenha(int id) => _persistencia.Obtenha(id);

        public IEnumerable<Processo> Obtenha() => _persistencia.Obtenha();

        public Processo Grave(Processo processo) => _persistencia.Grave(processo);

        public void Exclua(int id) => _persistencia.Exclua(id);
    }
}