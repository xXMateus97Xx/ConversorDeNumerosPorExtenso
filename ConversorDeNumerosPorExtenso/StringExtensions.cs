using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorDeNumerosPorExtenso
{
    internal static class StringExtensions
    {
        public static bool CompareIgnoringAccents(this string s1, string s2)
        {
            return string.Compare(
                s1.RemoveAccents(), s2.RemoveAccents(), StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public static string RemoveAccents(this string s)
        {
            Encoding destEncoding = Encoding.GetEncoding("iso-8859-8");

            return destEncoding.GetString(
                Encoding.Convert(Encoding.UTF8, destEncoding, Encoding.UTF8.GetBytes(s)));
        }
    }
}
