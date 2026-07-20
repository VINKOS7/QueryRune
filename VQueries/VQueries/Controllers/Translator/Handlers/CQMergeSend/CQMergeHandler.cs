using MediatR;

using VQueries.Api.Services.AFST;
using VQueries.Domain.Aggregates.Translator;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.CQMergeSend;

public class CQMergeHandler : IRequestHandler<CQMergeRequest, CQMergeResponse>
{
    private readonly ILogger<CQMergeHandler> _logger;
    private readonly IAFST<IDictionary<char, string>> _alphabetFST;
    private readonly ITranslatorRepo _translatorRepo;

    public CQMergeHandler(ILogger<CQMergeHandler> logger, HttpClient client, ITranslatorRepo translator, IAFST<IDictionary<char, string>> alphabetFST)
    {
        _logger = logger;
        _translatorRepo = translator;
        _alphabetFST = alphabetFST;
    }

    public async Task<CQMergeResponse> Handle(CQMergeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var translator = _translatorRepo.GetByAlphabetAsync(_alphabetFST.Alphabet.Keys.ToString() ?? throw new Exception("CRIT: rootAlphabet is null"));

            _alphabetFST.SimpleUnrunedMerged.Add((await translator).TranslateElement(request.Runes));

            _logger.LogInformation("attempt merge CombineQuery: success");

            return new CQMergeResponse() { delta = "mmaybe parts server context" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            throw;
        }
    }
}
