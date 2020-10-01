using System;
using System.Linq;
using NUnit.Framework;
using DataLawyer.Servico;
using DataLawyer.Dominio;
using System.Collections.Generic;

namespace DataLawyer.Teste.Servico
{
    public class TesteDeMovimentacoes
    {
        [Test]
        public void DeveGravarExcluir()
        {
            var processo = CrieProcesso();
            AltereProcesso();
            ValideProcesso();

            var movimentacoes = CrieMovimentacao1(processo);
            AltereMovimentacao1(processo);

            movimentacoes = CrieMovimentacao2(processo);

            ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(movimentacoes.First());
            ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(movimentacoes.Last());
            movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo);
            Assert.IsFalse(movimentacoes.Any());

            ServicoDeProcesso.Instancia.Exclua(processo);
        }

        private Processo CrieProcesso()
        {
            var processo = new Processo("00000000000000000001", GrauDeProcesso.Segundo);
            ServicoDeProcesso.Instancia.Grave(processo);

            processo = ServicoDeProcesso.Instancia.Obtenha(1);
            Assert.IsNotNull(processo);

            return processo;
        }

        private void AltereProcesso()
        {
            var processo = ServicoDeProcesso.Instancia.Obtenha(1);

            processo.Grau = GrauDeProcesso.Segundo;
            processo.Classe = "Apelaçăo";
            processo.Area = "Cível";
            processo.Assunto = "Vícios de Construçăo";
            processo.Origem = "Comarca de Feira de Santana";
            processo.Distribuicao = "Primeira Câmara Cível";
            processo.Relator = "MARIA DE LOURDES";

            ServicoDeProcesso.Instancia.Grave(processo);
        }

        private void ValideProcesso()
        {
            var processo = ServicoDeProcesso.Instancia.Obtenha(1);

            Assert.AreEqual(GrauDeProcesso.Segundo, processo.Grau);
            Assert.AreEqual("Apelaçăo", processo.Classe);
            Assert.AreEqual("Cível", processo.Area);
            Assert.AreEqual("Vícios de Construçăo", processo.Assunto);
            Assert.AreEqual("Comarca de Feira de Santana", processo.Origem);
            Assert.AreEqual("Primeira Câmara Cível", processo.Distribuicao);
            Assert.AreEqual("MARIA DE LOURDES", processo.Relator);
        }

        private IEnumerable<MovimentacaoDeProcesso> CrieMovimentacao1(Processo processo)
        {
            var data = DateTime.Today;
            var hora = new TimeSpan(23, 3, 59);

            var movimentacao = new MovimentacaoDeProcesso(processo, data, hora, "Movimentação");
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);

            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo);
            Assert.IsTrue(movimentacoes.Any());

            return movimentacoes;
        }

        private void AltereMovimentacao1(Processo processo)
        {
            var novaData = DateTime.Today.AddDays(1);
            var novaHora = new TimeSpan(22, 3, 59);

            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo);
            var persistido = movimentacoes.First();
            persistido.Descricao = "Movimentação1";
            persistido.Data = novaData;
            persistido.Hora = novaHora;
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(persistido);

            movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo);
            persistido = movimentacoes.First();
            Assert.AreEqual("Movimentação1", persistido.Descricao);
            Assert.AreEqual(novaData, persistido.Data);
            Assert.AreEqual(novaHora, persistido.Hora);
        }

        private IEnumerable<MovimentacaoDeProcesso> CrieMovimentacao2(Processo processo)
        {
            var data = DateTime.Today;
            var hora = new TimeSpan(23, 3, 59);

            var movimentacao = new MovimentacaoDeProcesso(processo, data, hora, "Movimentação2");
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);

            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo);
            Assert.AreEqual(2, movimentacoes.Count());

            return movimentacoes;
        }
    }
}