using Api.Shared.ModelDTOs.Contacts.Queries;
using MediatR;

namespace Api.Application.Services.Contacts.Queries;

public record GetAllContactsCommand(CancellationToken CancellationToken) : IRequest<List<GetAllContactsResponse>>;