using MediatR;

using VQueries.Api.Services.AFST;
using VQueries.Domain.Aggregates.Translator;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.Send;

public class SendHandler : IRequestHandler<SendRequest, SendResponse>
{
    private readonly ILogger<SendHandler> _logger;
    private readonly IAFST<IDictionary<char, string>> _alphabetFST;
    private readonly ITranslatorRepo _translatorRepo;

    private readonly HttpClient _httpClient;

    public SendHandler(ILogger<SendHandler> logger, HttpClient client, ITranslatorRepo translator, IAFST<IDictionary<char, string>> alphabetFST)
    {
        _logger = logger;
        _httpClient = client;
        _translatorRepo = translator;
        _alphabetFST = alphabetFST;
    }

    public async Task<SendResponse> Handle(SendRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var translator = _translatorRepo.GetByAlphabetAsync(_alphabetFST.Alphabet.Keys.ToString() ?? throw new Exception("CRIT: rootAlphabet is null"));

            var response = await _httpClient.GetAsync((await translator).TranslateElement(_alphabetFST.SimpleUnrunedMerged + request.Runes));

            _logger.LogInformation("attempt send merged CombineQuery: success");

            return new SendResponse() { Response = await response.Content.ReadAsStringAsync() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());

            throw;
        }
    }
}
