using System;
using System.Collections.Generic;
using System.Linq;
using DataLawyer.Dominio;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Persistencia
{
    public class PersistenciaDeProcesso
    {
        private static PersistenciaDeProcesso _instancia = null;
        public static PersistenciaDeProcesso Instancia => _instancia ?? new PersistenciaDeProcesso();
        private PersistenciaDeProcesso() { }

        public Processo Obtenha(int id)
        {
            using var contexto = new Contexto();

            var processo = contexto.Processo.Find(id);
            if (processo is null) throw new Exception("Processo inexistente.");

            return processo;
        }

        public Processo Obtenha(string numero)
        {
            using var contexto = new Contexto();

            var processo = contexto.Processo.FirstOrDefault(p => p.Numero == numero);
            if (processo is null) throw new Exception("Processo inexistente.");

            return processo;
        }

        public IEnumerable<Processo> Obtenha()
        {
            using var contexto = new Contexto();
            var processos = contexto.Processo.AsNoTracking()
                                             .ToList().OrderBy(p => p.Numero);
            return processos;
        }

        public void Grave(Processo processo)
        {
            if (processo is null) throw new Exception("Processo não informado.");
            processo.EhValido();

            using var contexto = new Contexto();
            var processoExistente = contexto.Processo.FirstOrDefault(p => p.Numero == processo.Numero);

            if (processoExistente is null)
            {
                contexto.Processo.Add(processo);
                contexto.SaveChanges();
                return;
            }

            processo.Id = processoExistente.Id;
            processoExistente.MergeProperties(processo);

            contexto.Processo.Update(processoExistente);
            contexto.SaveChanges();
        }

        public void Exclua(int id)
        {
            using var contexto = new Contexto();
            var processo = Obtenha(id);

            var movimentacoes = PersistenciaDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            if (movimentacoes.Any()) throw new Exception("Não é permitido excluir processo com movimentação.");

            contexto.Processo.Remove(processo);
            contexto.SaveChanges();
        }
    }
}