using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DataLawyer.Dominio
{
    public static class Extensoes
    {
        public static long ParaInteiro(this DateTime dataHora) => long.Parse(dataHora.ToString("yyyyMMddHHmm"));
        public static DateTime ParaDataHora(this long dataHora) => DateTime.ParseExact(dataHora.ToString(), "yyyyMMddHHmm", null);

        public static string ToShortDateTimeString(this DateTime dataHora) => dataHora.ToString("dd/MM/yyyy HH:mm");
        public static string ToShortTimeString(this TimeSpan hora) => hora.ToString("HH:mm");

        public static string SomenteNumeros(this string numero)
        {
            var regex = new Regex("[^0-9]");
            return regex.Replace(numero ?? string.Empty, string.Empty);
        }

        public static string FormateTexto(this string texto)
        {
            var regex = new Regex(@"\s+");
            return regex.Replace(texto ?? string.Empty, " ").Trim();
        }

        public static string ToDescription(this Enum value)
        {
            var type = value.GetType();
            var member = type.GetMembers().FirstOrDefault(m => m.Name == Enum.GetName(type, value));
            var attribute = member?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return attribute?.Description ?? value.ToString();
        }

        public static void MergeProperties<T>(this T target, T source)
        {
            var t = typeof(T);
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null) prop.SetValue(target, value, null);
            }
        }
    }
}