using Newtonsoft.Json;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.AlphabetInit;

public record AlphabetInitResponse
{
    [JsonProperty("webAlphabet")] required public IDictionary<char, string[]> WebAlphabet { get; set; }

    [JsonProperty("shortDomain")] required public string? ShortDomain { get; set; }
}