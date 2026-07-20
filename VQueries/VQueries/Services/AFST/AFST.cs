using System.Collections;
using VQueries.Api.Services.AFST.types;

namespace VQueries.Api.Services.AFST;

public class AFST<TRunes> : IAFST<TRunes> where TRunes : IDictionary<char, string>// maybe will write lock 
{
    public IList<string> SimpleUnrunedMerged { get; set; } = [];
    public AFST (IDictionary<char, string> dictionary) => Alphabets = new List<TRunes>();

    public void SetContext(ISetContextCommand<TRunes> context)
    {
        Alphabet = context.Alphabet;
        Dimension = context.Dimension;
        Alphabets = [context.Alphabet];
    }

    public ICollection<TRunes> Alphabets { get; private set; }
    public int Dimension { get; private set; }
    public TRunes Alphabet { get => Alphabets.ElementAt(Dimension);  set; }

    public void MoveDimension(AlphabetVector vector = AlphabetVector.Next) => Dimension += (int) vector;
    public TRunes AlphabetByDimension() => Alphabets.ElementAt(Dimension);
}