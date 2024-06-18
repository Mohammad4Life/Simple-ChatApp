using Api.Shared.ModelDTOs.Contacts.Commands;
using MediatR;

namespace Api.Application.Services.Contacts.Commands;

public record AddNewContactCommand(AddNewContactRequest Command, CancellationToken CancellationToken) : IRequest<AddNewContactResponse>;