using System;
using System.Linq;
using NUnit.Framework;
using DataLawyer.Servico;
using DataLawyer.Dominio;

namespace DataLawyer.Teste.Servico
{
    public class TesteDeMovimentacoes
    {
        [Test]
        public void DeveGravarExcluir()
        {
            CrieProcesso();
            AltereProcesso();

            CrieMovimentacao1();
            AltereMovimentacao1();

            CrieMovimentacao2();            
            Exclua();
        }     

        private void CrieProcesso()
        {
            var processo = Obtenha("00000000000000000001");
            ServicoDeProcesso.Instancia.Grave(processo);

            processo = ServicoDeProcesso.Instancia.Obtenha().FirstOrDefault();
            Assert.IsNotNull(processo);
        }

        private void AltereProcesso()
        {
            var processo = ServicoDeProcesso.Instancia.Obtenha().First();

            processo.Grau = GrauDeProcesso.Segundo;
            processo.Classe = "Apelaçăo2";
            processo.Area = "Cível2";
            processo.Assunto = "Vícios de Construçăo2";
            processo.Origem = "Comarca de Feira de Santana2";
            processo.Distribuicao = "Primeira Câmara Cível2";
            processo.Relator = "MARIA DE LOURDES2";
            ServicoDeProcesso.Instancia.Grave(processo);

            processo = ServicoDeProcesso.Instancia.Obtenha(processo.Id);
            Assert.AreEqual(GrauDeProcesso.Segundo, processo.Grau);
            Assert.AreEqual("Apelaçăo2", processo.Classe);
            Assert.AreEqual("Cível2", processo.Area);
            Assert.AreEqual("Vícios de Construçăo2", processo.Assunto);
            Assert.AreEqual("Comarca de Feira de Santana2", processo.Origem);
            Assert.AreEqual("Primeira Câmara Cível2", processo.Distribuicao);
            Assert.AreEqual("MARIA DE LOURDES2", processo.Relator);
        }

        private void CrieMovimentacao1()
        {
            var processo = ServicoDeProcesso.Instancia.Obtenha().First();
            var movimentacao = new MovimentacaoDeProcesso(processo, "Movimentação");
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);

            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            Assert.IsTrue(movimentacoes.Any());
        }

        private void AltereMovimentacao1()
        {
            var novaData = DateTime.Today.AddDays(1);
            var novaHora = new TimeSpan(22, 3, 59);

            var processo = ServicoDeProcesso.Instancia.Obtenha().First();
            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            var persistido = movimentacoes.First();
            persistido.Descricao = "Movimentação1";
            persistido.DataHora = novaData;            
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(persistido);

            movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            persistido = movimentacoes.First();
            Assert.AreEqual("Movimentação1", persistido.Descricao);
            Assert.AreEqual(novaData, persistido.DataHora);            
        }

        private void CrieMovimentacao2()
        {
            var processo = ServicoDeProcesso.Instancia.Obtenha().FirstOrDefault();
            var movimentacao = new MovimentacaoDeProcesso(processo, "Movimentação2");
            ServicoDeMovimentacaoDeProcesso.Instancia.Grave(movimentacao);

            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            Assert.AreEqual(2, movimentacoes.Count());
        }

        private void Exclua()
        {
            var processo = ServicoDeProcesso.Instancia.Obtenha().FirstOrDefault();

            var e = Assert.Throws<Exception>(() => ServicoDeProcesso.Instancia.Exclua(processo.Id));
            Assert.That(e.Message, Is.EqualTo("Não é permitido excluir processo com movimentação."));            

            var movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(movimentacoes.First().Id);
            ServicoDeMovimentacaoDeProcesso.Instancia.Exclua(movimentacoes.Last().Id);
            movimentacoes = ServicoDeMovimentacaoDeProcesso.Instancia.Obtenha(processo.Id);
            Assert.IsFalse(movimentacoes.Any());

            ServicoDeProcesso.Instancia.Exclua(processo.Id);
            var processos = ServicoDeProcesso.Instancia.Obtenha();
            Assert.IsFalse(processos.Any());
        }

        private Processo Obtenha(string numero)
        {
            return new Processo(numero, GrauDeProcesso.Primeiro)
            {
                Classe = "Apelaçăo",
                Area = "Cível",
                Assunto = "Vícios de Construçăo",
                Origem = "Comarca de Feira de Santana",
                Distribuicao = "Primeira Câmara Cível",
                Relator = "MARIA DE LOURDES"
            };
        }
    }
}