using System;

class Program
{
    static void Main(string[] args)
    {
        var cque = new CQUE();
        string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789-._~:/?#[]@!$&'()*+,;=%";

        // Входная строка клиента (8 символов)
        string original = "robawsvi";

        // Кодируем: на выходе получаем массив чисел (ровно 4 элемента)
        int[] encoded = cque.Compress(original, alphabet);

        // Декодируем обратно в строку
        string decoded = cque.Decompress(encoded, alphabet);

        Console.WriteLine($"Original      ({original.Length} chrs): {original}");
        Console.WriteLine($"Compressed IDs ({encoded.Length} items): {IntArrayToString(encoded)}");
        Console.WriteLine($"Decompress    ({decoded.Length} chrs): {decoded}");

        Console.ReadLine();
    }

    public static string IntArrayToString(int[] array)
    {
        if (array == null || array.Length == 0) return "";

        char[] res = new char[array.Length];

        for (int i = 0; i < array.Length; i++) res[i] = (char) array[i];
        return new string(res);
    }
}