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
            processo.Classe = "Apela��o";
            processo.Area = "C�vel";
            processo.Assunto = "V�cios de Constru��o";
            processo.Origem = "Comarca de Feira de Santana";
            processo.Distribuicao = "Primeira C�mara C�vel";
            processo.Relator = "MARIA DE LOURDES";
            ServicoDeProcesso.Instancia.Grave(processo);
            var persistido = ServicoDeProcesso.Instancia.Obtenha(1);
            Assert.IsNotNull(persistido);

            persistido = ServicoDeProcesso.Instancia.Obtenha(1);            
            persistido.Grau = GrauDeProcesso.Segundo;
            persistido.Classe = "Apela��o2";
            persistido.Area = "C�vel2";
            persistido.Assunto = "V�cios de Constru��o2";
            persistido.Origem = "Comarca de Feira de Santana2";
            persistido.Distribuicao = "Primeira C�mara C�vel2";
            persistido.Relator = "MARIA DE LOURDES2";
            ServicoDeProcesso.Instancia.Grave(persistido);

            persistido = ServicoDeProcesso.Instancia.Obtenha(1);
            Assert.AreEqual(GrauDeProcesso.Segundo, persistido.Grau);
            Assert.AreEqual("Apela��o2", persistido.Classe);
            Assert.AreEqual("C�vel2", persistido.Area);
            Assert.AreEqual("V�cios de Constru��o2", persistido.Assunto);
            Assert.AreEqual("Comarca de Feira de Santana2", persistido.Origem);
            Assert.AreEqual("Primeira C�mara C�vel2", persistido.Distribuicao);
            Assert.AreEqual("MARIA DE LOURDES2", persistido.Relator);

            ServicoDeProcesso.Instancia.Exclua(persistido);
            persistido = ServicoDeProcesso.Instancia.Obtenha(1);
            Assert.IsNull(persistido);
        }
    }
}