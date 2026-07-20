using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MediatR;

using VQueries.Api.Controllers.TranslatorController.Handlers.AlphabetInit;
using VQueries.Api.Controllers.TranslatorController.Handlers.CQMergeSend;

namespace VQueries.Api.Controllers.TranslatorController;

[Route("translator")]
public class TranslatorController : Controller
{
    private readonly IMediator _mediator;

    public TranslatorController(IMediator mediator) => _mediator = mediator;

    [AllowAnonymous] [HttpGet("alphabet/init")] public Task<AlphabetInitResponse> AlphabetInit(AlphabetInitRequest request) => _mediator.Send(request);

    [AllowAnonymous] [HttpGet("runes/single")] public Task<CQMergeResponse> SingleSend(CQMergeRequest request) => _mediator.Send(request);

    [AllowAnonymous] [HttpGet("runes/combine")] public Task<CQMergeResponse> CQMergedSend(CQMergeRequest request) => _mediator.Send(request);
}
