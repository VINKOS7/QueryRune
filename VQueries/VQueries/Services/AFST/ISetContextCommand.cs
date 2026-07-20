namespace VQueries.Api.Services.AFST;

public interface ISetContextCommand<TRunes>
{
    int Dimension { get; }

    TRunes Alphabet { get; }

    string BaseQuery { get; }
}

public record SetContextCommand<TRunes>() : ISetContextCommand<TRunes>
{
    public int Dimension { get; init; }

    public TRunes Alphabet { get; init; }

    public string BaseQuery { get; init; }
}