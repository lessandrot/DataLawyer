using NUnit.Framework;
using DataLawyer.Servico;
using System.Linq;
using System;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.Teste.Servico
{
    public class TesteDeRastreamento
    {
        [Test]
        public void DeveRastrear()
        {
            var resultado = ServicoDeRastreamento.Instancia.RastreieTJBA("0809979-67.2015.8.05.0080", GrauDeProcesso.Segundo);
            var processo = resultado.Processo;
            Assert.AreEqual("Apelação", processo.Classe);
            Assert.AreEqual("Cível", processo.Area);
            Assert.AreEqual("Vícios de Construção", processo.Assunto);
            Assert.AreEqual("Comarca de Feira de Santana / Foro de comarca Feira De Santana / 1ª V Dos Feitos De Rel De Cons Civ E Comerciais", processo.Origem);
            Assert.AreEqual("Primeira Câmara Cível", processo.Distribuicao);
            Assert.AreEqual("MARIA DE LOURDES PINHO MEDAUAR", processo.Relator);
            
            var processos = ServicoDeProcesso.Instancia.Obtenha();
            Assert.AreEqual(1, processos.Count());
            
            processo = processos.FirstOrDefault();
            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            var totalMovimentado = movimentacoes.Count();

            // Rastreando novamente para validar duplicidade
            ServicoDeRastreamento.Instancia.RastreieTJBA(processo.Numero, GrauDeProcesso.Segundo);

            processos = ServicoDeProcesso.Instancia.Obtenha();
            Assert.AreEqual(1, processos.Count());

            movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            Assert.AreEqual(totalMovimentado, movimentacoes.Count());

            Exclua();
        }

        private void Exclua()
        {
            var processo = ServicoDeProcesso.Instancia.Obtenha().FirstOrDefault();
            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);

            foreach( var movimentacao in movimentacoes)
            {
                ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(movimentacao.Id);
            }                                 

            ServicoDeProcesso.Instancia.Exclua(processo.Id);            
        }
    }
}