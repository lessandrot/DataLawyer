using System;
using System.Collections.Generic;
using System.Linq;
using DataLawyer.Dominio;
using DataLawyer.Dominio.Modelo;
using DataLawyer.Persistencia.Configuracao;
using Microsoft.EntityFrameworkCore;

namespace DataLawyer.Persistencia.Repositorio
{
    public class RepositorioDeProcesso
    {
        private static RepositorioDeProcesso _instancia = null;
        public static RepositorioDeProcesso Instancia => _instancia ?? new RepositorioDeProcesso();
        private RepositorioDeProcesso() { }

        public Processo Obtenha(int id)
        {
            using var contexto = new Contexto();

            var processo = contexto.Processo.Find(id);
            if (processo is null) throw new ApplicationException("Processo inexistente.");

            return processo;
        }

        public Processo Obtenha(string numero)
        {
            using var contexto = new Contexto();

            var processo = contexto.Processo.FirstOrDefault(p => p.Numero == numero);
            if (processo is null) throw new ApplicationException("Processo inexistente.");

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
            if (processo is null) throw new ApplicationException("Processo não informado.");
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

            var movimentacoes = RepositorioDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            if (movimentacoes.Any()) throw new ApplicationException("Não é permitido excluir processo com movimentação.");

            contexto.Processo.Remove(processo);
            contexto.SaveChanges();
        }
    }
}