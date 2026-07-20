using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections;
namespace VQueries.Api.Services.AFST;

public interface IAFST<TRunes>
{
    int Dimension { get; }

    ICollection<TRunes> Alphabets { get; }

    TRunes Alphabet { get; }

    void SetContext(ISetContextCommand<TRunes> alphabletFiniteStateTransreducer);

    IList<string> SimpleUnrunedMerged { get; }
}