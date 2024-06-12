using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_web_site_service.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;
    protected readonly ILogger _logger;
    public BaseController(IMediator mediator, ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
}
