using System;
using System.Collections.Generic;
using DataLawyer.Nucleo;

namespace DataLawyer.Dominio
{
    public class GrauDeProcesso : EnumeradorSeguro<GrauDeProcesso>
    {
        public static GrauDeProcesso Primeiro = new GrauDeProcesso(1, "Primeiro Grau");
        public static GrauDeProcesso Segundo = new GrauDeProcesso(2, "Segundo Grau");

        private GrauDeProcesso(int id, string descricao) : base(id, descricao) { }

        public static GrauDeProcesso Obtenha(int id) => id switch
        {
            1 => Primeiro,
            2 => Segundo,
            _ => throw new Exception("Grau de processo inválido."),
        };

        public static IEnumerable<GrauDeProcesso> Obtenha() => new[] { Primeiro, Segundo };
    }
}