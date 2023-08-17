using System;
using NUnit.Framework;
using DataLawyer.Dominio.Modelo;

namespace DataLawyer.Teste.Dominio
{
    public class TesteDeProcesso
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("   ")]
        public void NaoDeveExistirSemNumero(string numero)
        {
            var e = Assert.Throws<Exception>(() => Obtenha(numero).EhValido());
            Assert.IsTrue(e.Message.Contains("O número do processo deve ser informado."));
        }        

        [Test]
        [TestCase("1")]
        [TestCase("abcdefg")]
        [TestCase("!@#$%¨&*/?.")]
        [TestCase("1234567890123456789")]
        public void NaoDeveExistirComNumeroInvalido(string numero)
        {
            var e = Assert.Throws<Exception>(() => Obtenha(numero).EhValido());
            Assert.IsTrue(e.Message.Contains("O número do processo deve estar no padrão CNJ."));
        }

        [Test]
        public void DeveExistirComTodosOsDadosObrigatorios()
        {
            Assert.DoesNotThrow(() => Obtenha("12345678901234567890").EhValido());
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