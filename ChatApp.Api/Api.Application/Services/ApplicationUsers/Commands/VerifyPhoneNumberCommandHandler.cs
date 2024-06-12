using Api.Application.Exceptions;
using Api.DataAccess.Models;
using Api.DataAccess.UnitOfWork;
using Api.Shared.Enums;
using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using MediatR;

namespace Api.Application.Services.ApplicationUsers.Commands;

public class VerifyPhoneNumberCommandHandler : IRequestHandler<VerifyPhoneNumberCommand, VerifyPhoneNumberResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public VerifyPhoneNumberCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<VerifyPhoneNumberResponse> Handle(VerifyPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            ApplicationUser user = await _unitOfWork.ApplicationUserRepository.GetByPhoneNumber(request.Command.PhoneNumber);

            if (user.VerificationCode != request.Command.VerificationCode)
                throw new CustomException(Message: "کد وارد شده صحیح نمیباشد.");

            user.PhoneNumberConfirmed = true;

            await _unitOfWork.CommitAsync(cancellationToken);

            return new VerifyPhoneNumberResponse(AccountCreationState.NumberVerifided);

        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}