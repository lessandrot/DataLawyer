using System.ComponentModel;

namespace DataLawyer.Dominio.Modelo
{
    public enum GrauDeProcesso
    {
        [Description("Primeiro grau")]
        Primeiro = 1,

        [Description("Segundo grau")]
        Segundo = 2
    }
}