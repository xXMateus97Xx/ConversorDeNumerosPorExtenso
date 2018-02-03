using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversorDeNumerosPorExtenso
{
    public class Conversor
    {
        private static readonly string[] _conectores;
        private static readonly string[] _multiplicadores;
        private static readonly ReadOnlyDictionary<string, int> _numWords;

        static Conversor()
        {
            _conectores = new[] { "e" };

            _multiplicadores = new[]
            {
                "mil",
                "milhao",
                "bilhao",
                "milhoes",
                "bilhoes"
            };

            var numWords = new Dictionary<string, int>
            {
                { "zero", 0 },
                { "um", 1 },
                { "dois", 2 },
                { "tres", 3},
                { "quatro",4 },
                { "cinco", 5 },
                { "seis", 6 },
                { "sete", 7 },
                { "oito", 8 },
                { "nove", 9 },
                { "dez", 10 },
                { "onze", 11 },
                { "doze", 12 },
                { "treze", 13 },
                { "catorze", 14 },
                { "quinze", 15 },
                { "dezesseis", 16 },
                { "dezessete", 17 },
                { "dezoito", 18 },
                { "dezenove", 19 },
                { "vinte", 20 },
                { "trinta", 30 },
                { "quarenta", 40 },
                { "cinquenta", 50 },
                { "sessenta", 60 },
                { "setenta", 70 },
                { "oitenta", 80 },
                { "noventa", 90 },
                { "cem", 100 },
                { "cento", 100 },
                { "duzentos", 200 },
                { "trezentos", 300 },
                { "quatrocentos", 400 },
                { "quinhentos", 500 },
                { "seiscentos", 600 },
                { "setecentos", 700 },
                { "oitocentos", 800 },
                { "novecentos", 900 },
                { "mil", 1000 },
                { "milhao", 1000000 },
                { "bilhao", 1000000000 },
                { "milhoes", 1000000 },
                { "bilhoes", 1000000000 }
            };

            _numWords = new ReadOnlyDictionary<string, int>(numWords);
        }

        public static int StringToInt(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            text = text.Trim().ToLower();

            if (text.All(x => char.IsDigit(x)))
                return Convert.ToInt32(text);

            var result = 0;

            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var partialResult = 0;
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (_conectores.Contains(word))
                    continue;

                var numword = _numWords.FirstOrDefault(x => x.Key.CompareIgnoringAccents(word));
                if (numword.Equals(default(KeyValuePair<string, int>)))
                    throw new ApplicationException($"invalid word '{word}'");

                if (_multiplicadores.Contains(numword.Key))
                {
                    partialResult = partialResult * numword.Value;
                    result += partialResult;
                    partialResult = 0;
                }
                else
                {
                    partialResult += numword.Value;
                    if (i == words.Length - 1)
                        result += partialResult;
                }
            }
            return result;
        }
    }
}
