using Newtonsoft.Json;

namespace VQueries.Api.Controllers.TranslatorController.Handlers.CQMergeSend;

public record CQMergeResponse
{
    [JsonProperty("response")] public string delta { get; set; }
}