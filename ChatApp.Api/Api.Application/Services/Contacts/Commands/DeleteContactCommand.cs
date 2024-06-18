using Api.Shared.ModelDTOs.Contacts.Commands;
using MediatR;

namespace Api.Application.Services.Contacts.Commands;

public record DeleteContactCommand(DeleteContactRequest Command, CancellationToken CancellationToken) : IRequest<DeleteContactResponse>;