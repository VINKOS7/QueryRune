using MediatR;

using VQueries.Api.Services.AFST;
using VQueries.Domain.Aggregates.Translator;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.AlphabetInit;

public class AlphabetInitHandler : IRequestHandler<AlphabetInitRequest, AlphabetInitResponse>
{
    private readonly IAFST<IDictionary<char, string>> _aFST;
    private readonly ITranslatorRepo _translatorRepo;
    private readonly ILogger<AlphabetInitHandler> _logger;

    public AlphabetInitHandler(ITranslatorRepo translatorRepo, ILogger<AlphabetInitHandler> logger, IAFST<IDictionary<char, string>> aFST)
    {
        _logger = logger;
        _translatorRepo = translatorRepo;
        _aFST = aFST;
    }

    public async Task<AlphabetInitResponse> Handle(AlphabetInitRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var id = _translatorRepo.GetIdByAlphabetAsync(request.Alphabet);

            var webAlphabet = Translator.GetRunesFromAlphabet(request.Alphabet);

            _aFST.SetContext(new SetContextCommand<IDictionary<char, string>>() { Alphabet = (IDictionary<char, string>) webAlphabet, Dimension = 2, BaseQuery = request.baseForwardUrl });

            if (await id != Guid.Empty) return new () { WebAlphabet = webAlphabet, ShortDomain = "http://v.ro" };
            else
            {
                var translator = Translator.From(new AlphabetInitCommand(request) { Runes = Translator.GetRunesFromAlphabet(request.Alphabet), BaseForwardUrl = request.baseForwardUrl });

                await _translatorRepo.AddAsync(translator);

                await _translatorRepo.UnitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation($"attempt init alphabet - success: added new with ID: { translator.Id }");

                return new () { WebAlphabet = translator.Runes, ShortDomain = "http://v.ro" };
            }
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex.Message);

            throw;
        }
    }
}
