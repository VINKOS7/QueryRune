using Dotseed.Domain;

namespace VQueries.Domain.Aggregates.Translator;

internal static class IntExtns
{
    public static string ArrayToString(this int[] array)
    {
        if (array == null || array.Length == 0) return "";

        char[] res = new char[array.Length];

        for (int i = 0; i < array.Length; i++) res[i] = (char)array[i];
        return new string(res);
    }
}

public class Translator : Entity, IAggregateRoot
{
    public Guid Id { get; set; } = new ();

    public required string BaseQuery { get; set; }
    public required Dictionary<char, string[]> Runes { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }

    public static Translator From(IAddTranslator command) => new()
    {
        BaseQuery = command.BaseForwardUrl,
        Runes = (Dictionary<char, string[]>) command.Runes,

        Name = command.Name ?? string.Empty,
        Description = command.Description ?? string.Empty,
    };

    public static Dictionary<char, string[]> GetRunesFromAlphabet(string alphabet) => alphabet
        .SelectMany(rune => alphabet.Select(delta => (rune, value: $"{rune}{delta}")))
        .GroupBy(x => x.rune)
        .ToDictionary(g => g.Key, g => g.Select(x => x.value).ToArray());

    public string TranslateElement(string runes) => Decompress(runes.Select(rune => (int) rune).ToArray(), Runes.Keys.ToString() ?? throw new Exception("domain error: empty alphabet"));

    /// YOU SHOULD WRITE ONLY FREANDLY UDON CODE
    /// what is it - code use only system or\and libs from the frandly list libs for udon
    public int[] Compress(string input, string clientAlphabet)
    {
        if (string.IsNullOrEmpty(input) || input.Length % 2 != 0 || string.IsNullOrEmpty(clientAlphabet)) return new int[0];

        int baseLen = clientAlphabet.Length;
        int totalBlocks = input.Length / 2;
        int[] res = new int[totalBlocks];

        for (int b = 0; b < totalBlocks; b++)
        {
            int idx1 = clientAlphabet.IndexOf(input[b * 2]);
            int idx2 = clientAlphabet.IndexOf(input[b * 2 + 1]);
            if (idx1 < 0 || idx2 < 0) return new int[0];

            // Формула плоской матрицы: переводим пару в один уникальный ID
            res[b] = (idx1 * baseLen) + idx2;
        }
        return res;
    }

    public string Decompress(int[] input, string clientAlphabet)
    {
        if (input == null || input.Length == 0 || string.IsNullOrEmpty(clientAlphabet)) return "";

        int baseLen = clientAlphabet.Length;
        char[] res = new char[input.Length * 2];
        int resIdx = 0;

        for (int b = 0; b < input.Length; b++)
        {
            int id = input[b];

            // Обратная математика матрицы: достаем координаты X и Y из ID
            int idx1 = id / baseLen;
            int idx2 = id % baseLen;

            res[resIdx] = clientAlphabet[idx1];
            res[resIdx + 1] = clientAlphabet[idx2];
            resIdx += 2;
        }
        return new string(res);
    }
}
