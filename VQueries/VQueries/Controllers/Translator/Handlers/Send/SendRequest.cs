using MediatR;
using Newtonsoft.Json;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.Send;

public record SendRequest : IRequest<SendResponse>
{
    [JsonProperty("runes")] public string Runes { get; set; }
}
