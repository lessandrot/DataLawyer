using System.Collections.Generic;
using DataLawyer.Dominio;
using DataLawyer.Persistencia;

namespace DataLawyer.Servico
{
    public class ServicoDeLog
    {
        private static ServicoDeLog _instancia = null;
        public static ServicoDeLog Instancia => _instancia ?? new ServicoDeLog();
        private ServicoDeLog() { }

        private PersistenciaDeLog _persistencia = PersistenciaDeLog.Instancia;

        public void Registre(string erro) => _persistencia.Registre(new LogDeErro(erro));
        public IEnumerable<LogDeErro> Obtenha() => _persistencia.Obtenha();
    }
}