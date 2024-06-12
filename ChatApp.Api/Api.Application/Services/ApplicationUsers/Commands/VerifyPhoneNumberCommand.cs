using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using MediatR;

namespace Api.Application.Services.ApplicationUsers.Commands;

public record VerifyPhoneNumberCommand(VerifyPhoneNumberRequest Command, CancellationToken CancellationToken) : IRequest<VerifyPhoneNumberResponse>;
