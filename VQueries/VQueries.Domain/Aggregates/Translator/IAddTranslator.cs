namespace VQueries.Domain.Aggregates.Translator;

public interface IAddTranslator
{
    public IDictionary<char, string[]> Runes { get; set; }

    public string BaseForwardUrl { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}
