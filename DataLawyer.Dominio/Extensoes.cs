using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DataLawyer.Dominio
{
    public static class Extensoes
    {
        private static CultureInfo _culture = CultureInfo.GetCultureInfo("pt-BR");

        public static long ParaInteiro(this DateTime dataHora) => long.Parse(dataHora.ToString("yyyyMMddHHmm"));
        public static DateTime ParaDataHora(this long dataHora) => DateTime.ParseExact(dataHora.ToString(), "yyyyMMddHHmm", null);

        public static string ToShortDateTime(this DateTime date) => date.ToString("dd/MM/yyyy HH:mm:ss", _culture);
        public static string ToShortDateTime(this DateTime? date) => date.HasValue ? date.Value.ToShortDateTime() : string.Empty;
        public static string ToShortDate(this DateTime date) => date.ToString("dd/MM/yyyy", _culture);
        public static string ToShortDate(this DateTime? date) => date.HasValue ? date.Value.ToShortDate() : string.Empty;

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

        public static string FullMessage(this Exception ex)
        {
            var details = new StringBuilder(ex.Message);

            var inner = ex.InnerException;
            var innerMessage = inner?.Message;
            while (true)
            {
                inner = inner?.InnerException;
                if (inner is null) break;
                innerMessage = inner.Message;
            }
            if (!string.IsNullOrEmpty(innerMessage)) details.Append($" - {innerMessage}");

            return details.ToString();
        }

        public static string FullMessageWithStack(this Exception ex)
        {
            var details = new StringBuilder(ex.FullMessage());
            if (!string.IsNullOrEmpty(ex.StackTrace)) details.Append($" => {ex.StackTrace}");

            return details.ToString();
        }
    }
}