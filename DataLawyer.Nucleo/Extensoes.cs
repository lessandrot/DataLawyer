using System;
using System.Text.RegularExpressions;

namespace DataLawyer.Nucleo
{
    public static class Extensoes
    {
        public static int ParaInteiro(this DateTime data) => int.Parse(data.ToString("yyyyMMdd"));
        public static DateTime ParaData(this int data) => DateTime.ParseExact(data.ToString(), "yyyyMMdd", null);
        public static int ParaInteiro(this TimeSpan hora) => int.Parse(hora.ToString("hhmmss"));
        public static TimeSpan ParaHora(this int hora) => TimeSpan.ParseExact(hora.ToString(), "hhmmss", null);
        public static string ToShortTimeString(this TimeSpan hora) => hora.ToString("hh:mm");
        public static string SomenteNumeros(this string numero)
        {
            var regex = new Regex("[^0-9]");
            return regex.Replace(numero ?? string.Empty, string.Empty);
        }
    }
}