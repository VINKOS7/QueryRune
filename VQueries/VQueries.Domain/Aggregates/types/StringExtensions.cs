public static class StringExtensions
{
    public static Dictionary<TKey, TValue> ToDictionary<TSource, TKey, TValue>(
        this IEnumerable<TSource> source,
        Func<TSource, int, TKey> keySelector,
        Func<TSource, int, TValue> valueSelector)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
        if (valueSelector == null) throw new ArgumentNullException(nameof(valueSelector));

        var dictionary = new Dictionary<TKey, TValue>();
        int index = 0;

        foreach (var item in source)
        {
            TKey key = keySelector(item, index);
            TValue value = valueSelector(item, index);

            dictionary.Add(key, value);
            index++;
        }

        return dictionary;
    }
}