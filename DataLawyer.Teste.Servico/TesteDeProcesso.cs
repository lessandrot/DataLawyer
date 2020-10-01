using NUnit.Framework;
using DataLawyer.Servico;
using DataLawyer.Dominio;

namespace DataLawyer.Teste.Servico
{
    public class TesteDeProcesso
    {
        [Test]
        public void DeveGravarExcluir()
        {
            var processo = new Processo("00000000000000000001", GrauDeProcesso.Primeiro);
            processo.Classe = "Apelação";
            processo.Area = "Cível";
            processo.Assunto = "Vícios de Construção";
            processo.Origem = "Comarca de Feira de Santana";
            processo.Distribuicao = "Primeira Câmara Cível";
            processo.Relator = "MARIA DE LOURDES";
            ServicoDeProcesso.Instancia.Grave(processo);
            var persistido = ServicoDeProcesso.Instancia.Obtenha(1);
            Assert.IsNotNull(persistido);

            persistido = ServicoDeProcesso.Instancia.Obtenha(1);            
            persistido.Grau = GrauDeProcesso.Segundo;
            persistido.Classe = "Apelação2";
            persistido.Area = "Cível2";
            persistido.Assunto = "Vícios de Construção2";
            persistido.Origem = "Comarca de Feira de Santana2";
            persistido.Distribuicao = "Primeira Câmara Cível2";
            persistido.Relator = "MARIA DE LOURDES2";
            ServicoDeProcesso.Instancia.Grave(persistido);

            persistido = ServicoDeProcesso.Instancia.Obtenha(1);
            Assert.AreEqual(GrauDeProcesso.Segundo, persistido.Grau);
            Assert.AreEqual("Apelação2", persistido.Classe);
            Assert.AreEqual("Cível2", persistido.Area);
            Assert.AreEqual("Vícios de Construção2", persistido.Assunto);
            Assert.AreEqual("Comarca de Feira de Santana2", persistido.Origem);
            Assert.AreEqual("Primeira Câmara Cível2", persistido.Distribuicao);
            Assert.AreEqual("MARIA DE LOURDES2", persistido.Relator);

            ServicoDeProcesso.Instancia.Exclua(persistido);
            persistido = ServicoDeProcesso.Instancia.Obtenha(1);
            Assert.IsNull(persistido);
        }
    }
}