using System.Linq;
using System.Collections.Generic;
using DataLawyer.Dominio;

namespace DataLawyer.Persistencia
{
    public class PersistenciaDeMovimentacaoDeProcesso
    {
        private static PersistenciaDeMovimentacaoDeProcesso _instancia = null;
        public static PersistenciaDeMovimentacaoDeProcesso Instancia => _instancia ?? new PersistenciaDeMovimentacaoDeProcesso();
        private PersistenciaDeMovimentacaoDeProcesso() { }

        public IEnumerable<MovimentacaoDeProcesso> Obtenha(Processo processo)
        {
            var movimentacoes = new List<MovimentacaoDeProcesso>();

            using (var contexto = new Contexto())
            {                
                var lista = contexto.MovimentacaoDeProcesso.Where(m => m.Processo.Equals(processo));
                movimentacoes.AddRange(lista);
            }            

            return movimentacoes.OrderByDescending(p => p.Data).ThenByDescending(m => m.Hora);
        }

        public void Grave(MovimentacaoDeProcesso movimentacao)
        {
            if (movimentacao is null) return;

            using (var contexto = new Contexto())
            {                
                var movimentacaoExistente = contexto.MovimentacaoDeProcesso.FirstOrDefault(m => m.Equals(movimentacao));

                if (movimentacaoExistente is null)
                {
                    movimentacao.Processo = contexto.Processo.Find(movimentacao.Processo.Id);
                    contexto.Add(movimentacao);
                    contexto.SaveChanges();
                    return;
                }

                movimentacaoExistente.Data = movimentacao.Data;
                movimentacaoExistente.Hora = movimentacao.Hora;
                movimentacaoExistente.Descricao = movimentacao.Descricao;                
                contexto.Update(movimentacaoExistente);
                contexto.SaveChanges();
            }
        }

        public void Exclua(MovimentacaoDeProcesso movimentacao)
        {
            if (movimentacao is null) return;

            using (var contexto = new Contexto())
            {
                var movimentacaoExistente = contexto.MovimentacaoDeProcesso.Find(movimentacao.Id);
                contexto.Remove(movimentacaoExistente);
                contexto.SaveChanges();
            }
        }
    }
}