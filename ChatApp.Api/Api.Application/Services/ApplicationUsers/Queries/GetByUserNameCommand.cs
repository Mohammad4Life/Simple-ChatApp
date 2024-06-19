using Api.Shared.ModelDTOs.ApplicationUsers.Queries;
using MediatR;

namespace Api.Application.Services.ApplicationUsers.Queries;

public record GetByUserNameCommand(GetByUserNameRequest Command, CancellationToken CancellationToken) : IRequest<GetByUserNameResponse>;