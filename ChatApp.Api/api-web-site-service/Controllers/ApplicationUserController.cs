using Api.Application.Services.ApplicationUsers.Commands;
using Api.Application.Utils;
using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using api_web_site_service.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_web_site_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationUserController : BaseController
{
    public ApplicationUserController(IMediator mediator, ILogger logger) : base(mediator, logger)
    {
        
    }

    [HttpPost]
    [Route(nameof(RegisterPhoneNumber))]
    public async Task<ApiResponse<RegisterPhoneNumberResponse>> RegisterPhoneNumber(RegisterPhoneNumberRequest request, CancellationToken cancellationToken)
    {
        return ApiResponse<RegisterPhoneNumberResponse>.Success(await _mediator.Send(new RegisterPhoneNumberCommand(request, cancellationToken)));
    }
}
