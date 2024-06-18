using Api.Application.Exceptions;
using Api.DataAccess.Models;
using Api.DataAccess.UnitOfWork;
using Api.Shared.Enums;
using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Api.Application.Services.ApplicationUsers.Commands;

public class AddAcountDetailsCommandHandler : IRequestHandler<AddAcountDetailsCommand, AddAcountDetailsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddAcountDetailsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AddAcountDetailsResponse> Handle(AddAcountDetailsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.ApplicationUserRepository.GetByPhoneNumber(request.Command.PhoneNumber);

            user.FirstName = request.Command.FirstName;

            user.LastName = request.Command.LastName;

            user.BirthDate = request.Command.BirthDate;

            user.UserName = request.Command.UserName;

            await _unitOfWork.CommitAsync(cancellationToken);

            return new AddAcountDetailsResponse(AccountCreationState.DetailProvided);
        }
        catch(CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}