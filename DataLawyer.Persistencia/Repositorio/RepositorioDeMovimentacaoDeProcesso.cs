using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataLawyer.Dominio;
using DataLawyer.Persistencia.Configuracao;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.Persistencia.Repositorio
{
    public class RepositorioDeMovimentacaoDeProcesso
    {
        private static RepositorioDeMovimentacaoDeProcesso _instancia = null;
        public static RepositorioDeMovimentacaoDeProcesso Instancia => _instancia ?? new RepositorioDeMovimentacaoDeProcesso();
        private RepositorioDeMovimentacaoDeProcesso() { }

        public IEnumerable<MovimentacaoDeProcesso> Obtenha(int processoId)
        {
            using var contexto = new Contexto();
            var processo = RepositorioDeProcesso.Instancia.Obtenha(processoId);

            var movimentacoes = contexto.MovimentacaoDeProcesso.Include(m => m.Processo).AsNoTracking()
                                                               .Where(m => m.Processo.Equals(processo))
                                                               .ToList().OrderByDescending(p => p.DataHora);

            return movimentacoes;
        }

        public void Grave(MovimentacaoDeProcesso movimentacao)
        {
            if (movimentacao is null) throw new Exception("Movimentação não informada.");
            movimentacao.EhValido();

            using var contexto = new Contexto();

            movimentacao.Processo = contexto.Processo.Find(movimentacao.Processo.Id);
            if (movimentacao.Processo is null) throw new Exception("Processo inexistente.");

            var movimentacaoExistente = contexto.MovimentacaoDeProcesso.Find(movimentacao.Id);
            if (movimentacaoExistente is null)
            {
                contexto.MovimentacaoDeProcesso.Add(movimentacao);
                contexto.SaveChanges();
                return;
            }

            movimentacao.Id = movimentacaoExistente.Id;
            movimentacaoExistente.MergeProperties(movimentacao);

            contexto.MovimentacaoDeProcesso.Update(movimentacaoExistente);
            contexto.SaveChanges();
        }

        public void Exclua(int id)
        {
            using var contexto = new Contexto();
            var movimentacao = contexto.MovimentacaoDeProcesso.Find(id);
            if (movimentacao is null) throw new Exception("Movimentação inexistente.");

            contexto.MovimentacaoDeProcesso.Remove(movimentacao);
            contexto.SaveChanges();
        }

        public void ExcluaTodas(int processoId)
        {
            using var contexto = new Contexto();

            var movimentacoes = contexto.MovimentacaoDeProcesso.Where(m => m.Processo.Id == processoId).ToList();
            contexto.MovimentacaoDeProcesso.RemoveRange(movimentacoes);
            contexto.SaveChanges();
        }
    }
}