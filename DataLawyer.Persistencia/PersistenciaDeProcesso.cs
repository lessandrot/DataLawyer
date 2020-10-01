using System.Collections.Generic;
using System.Linq;
using DataLawyer.Dominio;

namespace DataLawyer.Persistencia
{
    public class PersistenciaDeProcesso
    {
        private static PersistenciaDeProcesso _instancia = null;
        public static PersistenciaDeProcesso Instancia => _instancia ?? new PersistenciaDeProcesso();
        private PersistenciaDeProcesso() { }

        public Processo Obtenha(int id)
        {
            using (var contexto = new Contexto())
            {
                return contexto.Processo.Find(id);
            }
        }

        public IEnumerable<Processo> Obtenha()
        {
            var processos = new List<Processo>();

            using (var contexto = new Contexto())
            {
                var lista = contexto.Processo.ToList();
                processos.AddRange(lista);
            }

            return processos.OrderBy(p => p.Numero);
        }

        public Processo Grave(Processo processo)
        {
            if (processo is null) return processo;

            using (var contexto = new Contexto())
            {
                var processoExistente = contexto.Processo.FirstOrDefault(p => p.Equals(processo));

                if (processoExistente is null)
                {
                    contexto.Processo.Add(processo);
                    contexto.SaveChanges();
                    return processo;
                }

                processoExistente.Grau = processo.Grau;
                processoExistente.Classe = processo.Classe;
                processoExistente.Area = processo.Area;
                processoExistente.Assunto = processo.Assunto;
                processoExistente.Origem = processo.Origem;
                processoExistente.Distribuicao = processo.Distribuicao;
                processoExistente.Relator = processo.Relator;
                contexto.Update(processoExistente);
                contexto.SaveChanges();

                return processo;
            }
        }

        public void Exclua(Processo processo)
        {
            using (var contexto = new Contexto())
            {
                var processoExistente = contexto.Processo.Find(processo.Id);
                contexto.Remove(processoExistente);
                contexto.SaveChanges();
            }
        }
    }
}