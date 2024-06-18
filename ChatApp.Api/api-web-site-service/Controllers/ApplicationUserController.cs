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

    [HttpPost]
    [Route(nameof(VerifyPhoneNumber))]
    public async Task<ApiResponse<VerifyPhoneNumberResponse>> VerifyPhoneNumber(VerifyPhoneNumberRequest request, CancellationToken cancellationToken)
    {
        return ApiResponse<VerifyPhoneNumberResponse>.Success(await _mediator.Send(new VerifyPhoneNumberCommand(request, cancellationToken)));
    }

    [HttpPost]
    [Route(nameof(AddAccountDetails))]
    public async Task<ApiResponse<AddAcountDetailsResponse>> AddAccountDetails(AddAcountDetailsRequest request, CancellationToken cancellationToken)
    {
        return ApiResponse<AddAcountDetailsResponse>.Success(await _mediator.Send(new AddAcountDetailsCommand(request, cancellationToken)));
    }

    [HttpPost]
    [Route(nameof(CreateAccount))]
    public async Task<ApiResponse<CreateAccountResponse>> CreateAccount(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        return ApiResponse<CreateAccountResponse>.Success(await _mediator.Send(new CreateAccountCommand (request, cancellationToken)));
    }
}