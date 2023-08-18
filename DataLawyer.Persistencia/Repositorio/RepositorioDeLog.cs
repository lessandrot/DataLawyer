using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using DataLawyer.Persistencia.Configuracao;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.Persistencia.Repositorio
{
    public class RepositorioDeLog
    {
        private static RepositorioDeLog _instancia = null;
        public static RepositorioDeLog Instancia => _instancia ?? new RepositorioDeLog();        

        public void Registre(LogDeErro log)
        {
            if (log is null) return;

            using var contexto = new Contexto();
            contexto.LogDeErro.Add(log);
            contexto.SaveChanges();
        }

        public IEnumerable<LogDeErro> Obtenha()
        {
            using var contexto = new Contexto();
            var logs = contexto.LogDeErro.AsNoTracking()
                                         .ToList().OrderByDescending(p => p.DataHora);
            return logs;
        }
    }
}