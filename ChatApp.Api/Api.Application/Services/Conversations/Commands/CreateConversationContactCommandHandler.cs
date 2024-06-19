using Api.Application.Exceptions;
using Api.DataAccess.UnitOfWork;
using Api.Shared.ModelDTOs.Conversations.Commands;
using AutoMapper;
using MediatR;

namespace Api.Application.Services.Conversations.Commands;

public class CreateConversationContactCommandHandler : IRequestHandler<CreateConversationContactCommand, CreateConversationContactResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateConversationContactCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateConversationContactResponse> Handle(CreateConversationContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (_unitOfWork.ConversationRepository.Exists(request.Command.UserId, _unitOfWork.ContactRepository.GetUserIdByContactId(request.Command.ContactId).ToString()))
                throw new CustomException(Message: "چت از قبل وجودذ دارد.");

            throw new NotImplementedException();
        }
        catch (CustomException ex)
        {
            throw new CustomException();
        }
    }
}
