using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using MediatR;

namespace Api.Application.Services.ApplicationUsers.Commands;

public record RegisterPhoneNumberCommand(RegisterPhoneNumberRequest Command, CancellationToken CancellationToken) : IRequest<RegisterPhoneNumberResponse>;