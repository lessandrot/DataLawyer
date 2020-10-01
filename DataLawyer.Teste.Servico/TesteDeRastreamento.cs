using System;
using System.Linq;
using NUnit.Framework;
using DataLawyer.Servico;
using DataLawyer.Dominio;
using DataLawyer.Rastreamento;

namespace DataLawyer.Teste.Servico
{
    public class TesteDeRastreamento
    {
        [Test]
        public void DeveRastrearEGravar()
        {
            var rastreador = new RastreadorTJBA();
            rastreador.Rastreie("0809979-67.2015.8.05.0080", GrauDeProcesso.Segundo);

            var processo = ServicoDeProcesso.Instancia.Obtenha().First();
            Assert.AreEqual("Apelação", processo.Classe);
            Assert.AreEqual("Cível", processo.Area);
            Assert.AreEqual("Vícios de Construção", processo.Assunto);
            Assert.AreEqual("Comarca de Feira de Santana / Foro de comarca Feira De Santana / 1ª V Dos Feitos De Rel De Cons Civ E Comerciais", processo.Origem);
            Assert.AreEqual("Primeira Câmara Cível", processo.Distribuicao);
            Assert.AreEqual("MARIA DE LOURDES PINHO MEDAUAR", processo.Relator);
        }
    }
}