using MediatR;
using Newtonsoft.Json;
using VQueries.Domain.Aggregates.Translator;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.AlphabetInit;

public record AlphabetInitRequest : IRequest<AlphabetInitResponse>
{
    [JsonProperty("alphabet")] public required string Alphabet { get; set; }
    [JsonProperty("baseQuery")] public required string baseForwardUrl { get; set; }

    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("description")] public string? Description { get; set; }
}

public record AlphabetInitCommand : IAddTranslator
{
    public required string BaseForwardUrl { get; set; }
    public required IDictionary<char, string[]> Runes { get; set; }

    public string? Name { set; get; }
    public string? Description { set; get; }

    public AlphabetInitCommand(AlphabetInitRequest command) => new AlphabetInitCommand(command)
    {
        BaseForwardUrl = command.baseForwardUrl,
        Runes = new Dictionary<char, string[]>(),

        Name = command.Name ?? string.Empty,
        Description = command.Description ?? string.Empty,
    };
}