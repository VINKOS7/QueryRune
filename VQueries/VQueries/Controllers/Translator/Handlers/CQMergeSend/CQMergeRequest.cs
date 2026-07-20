using MediatR;
using Newtonsoft.Json;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.CQMergeSend;

public record CQMergeRequest : IRequest<CQMergeResponse>
{
    [JsonProperty("runes")] public string Runes { get; set; }
}
