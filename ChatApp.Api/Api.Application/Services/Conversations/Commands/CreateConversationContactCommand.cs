using Api.Shared.ModelDTOs.Conversations.Commands;
using MediatR;

namespace Api.Application.Services.Conversations.Commands;

public record CreateConversationContactCommand(CreateConversationContactRequest Command, CancellationToken CancellationToken) : IRequest<CreateConversationContactResponse>;