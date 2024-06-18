using Api.Application.Exceptions;
using Api.DataAccess.UnitOfWork;
using Api.Shared.Enums;
using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using MediatR;

namespace Api.Application.Services.ApplicationUsers.Commands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public CreateAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateAccountResponse> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            return new CreateAccountResponse(AccountCreationState.AccountCreated);
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
