using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using MediatR;

namespace Api.Application.Services.ApplicationUsers.Commands;

public record AddAcountDetailsCommand(AddAcountDetailsRequest Command, CancellationToken CancellationToken) : IRequest<AddAcountDetailsResponse>;