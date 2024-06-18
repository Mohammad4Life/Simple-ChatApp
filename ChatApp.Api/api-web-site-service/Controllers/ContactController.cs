using Api.Application.Services.Contacts.Commands;
using Api.Application.Utils;
using Api.Shared.ModelDTOs.Contacts.Commands;
using api_web_site_service.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_web_site_service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactController : BaseController
{
    public ContactController(Mediator mediator, ILogger logger) : base(mediator, logger)
    {
        
    }

    [HttpPost]
    [Route(nameof(AddNewContact))]
    public async Task<ApiResponse<AddNewContactResponse>> AddNewContact(AddNewContactRequest request, CancellationToken cancellationToken)
    {
        return ApiResponse<AddNewContactResponse>.Success(await _mediator.Send(new AddNewContactCommand(request, cancellationToken)));
    }

    [HttpPost]
    [Route(nameof(UpdateContact))]
    public async Task<ApiResponse<UpdateContactResponse>> UpdateContact(UpdateContactRequest request, CancellationToken cancellationToken)
    {
        return ApiResponse<UpdateContactResponse>.Success(await _mediator.Send(new UpdateContactCommand(request, cancellationToken)));
    }

    [HttpPost]
    [Route(nameof(DeleteContact))]
    public async Task<ApiResponse<DeleteContactResponse>> DeleteContact(DeleteContactRequest request, CancellationToken cancellationToken)
    {
        return ApiResponse<DeleteContactResponse>.Success(await _mediator.Send(new DeleteContactCommand(request, cancellationToken)));
    }
}
