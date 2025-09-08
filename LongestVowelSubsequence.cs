using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VowelSubsequenceApp
{
    public class Wowel
    {
        public class VowelSequenceResult
        {
            [JsonPropertyName("word")]
            public string Word { get; set; }

            [JsonPropertyName("sequence")]
            public string Sequence { get; set; }

            [JsonPropertyName("length")]
            public int Length { get; set; }
        }

        public static string LongestVowelSubsequenceAsJson(List<string> words)
        {
            var results = new List<VowelSequenceResult>();
            var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

            foreach (var word in words)
            {
                string longest = "";
                string current = "";

                foreach (char c in word.ToLower())
                {
                    if (vowels.Contains(c))
                    {
                        current += c;
                        if (current.Length > longest.Length)
                        {
                            longest = current;
                        }
                    }
                    else
                    {
                        current = "";
                    }
                }

                results.Add(new VowelSequenceResult
                {
                    Word = word,
                    Sequence = longest,
                    Length = longest.Length
                });
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(results, options);
        }

        public static void Main(string[] args)
        {
            var testCases = new List<List<string>>
            {
                new List<string> { "aeiou", "bcd", "aaa" },
                new List<string> { "miscellaneous", "queue", "sky", "cooperative" },
                new List<string> { "sequential", "beautifully", "rhythms", "encyclopaedia" },
                new List<string> { "algorithm", "education", "idea", "strength" },
                new List<string>() // empty list
            };

            for (int i = 0; i < testCases.Count; i++)
            {
                Console.WriteLine($"Giriş {i + 1}:");
                string jsonOutput = LongestVowelSubsequenceAsJson(testCases[i]);
                Console.WriteLine(jsonOutput);
                Console.WriteLine();
            }
        }
    }
}
