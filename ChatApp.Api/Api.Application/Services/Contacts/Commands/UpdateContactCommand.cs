using Api.Shared.ModelDTOs.Contacts.Commands;
using MediatR;

namespace Api.Application.Services.Contacts.Commands;

public record UpdateContactCommand(UpdateContactRequest Command, CancellationToken CancellationToken) : IRequest<UpdateContactResponse>;