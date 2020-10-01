using NUnit.Framework;
using System;
using DataLawyer.Dominio;

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
            var e = Assert.Throws<Exception>(() => new Processo(numero, GrauDeProcesso.Primeiro));
            Assert.That(e.Message, Is.EqualTo("O número do processo deve ser informado."));
        }

        [Test]
        public void NaoDeveExistirComNumeroInvalido()
        {
            var e = Assert.Throws<Exception>(() => new Processo("1", null));
            Assert.That(e.Message, Is.EqualTo("O número do processo deve estar no padrão CNJ."));
        }

        [Test]
        public void NaoDeveExistirSemGrau()
        {
            var e = Assert.Throws<Exception>(() => new Processo("00000000000000000001", null));
            Assert.That(e.Message, Is.EqualTo("O grau do processo deve ser informado."));
        }

        [Test]
        public void DeveExistirComTodosOsDadosObrigatorios()
        {            
            var processo = new Processo("00000000000000000001", GrauDeProcesso.Segundo);
            Assert.IsNotNull(processo);
        }
    }
}