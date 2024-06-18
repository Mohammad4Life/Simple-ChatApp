using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using MediatR;

namespace Api.Application.Services.ApplicationUsers.Commands;

public record CreateAccountCommand(CreateAccountRequest Command, CancellationToken CancellationToken) : IRequest<CreateAccountResponse>;