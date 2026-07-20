public class CQUE
{
    // СЖАТИЕ: Из 8 символов делает массив из 4 чисел (длина ровно в 2 раза меньше)
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

    // РАСЖАТИЕ: Из массива чисел восстанавливает исходную строку текста
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
