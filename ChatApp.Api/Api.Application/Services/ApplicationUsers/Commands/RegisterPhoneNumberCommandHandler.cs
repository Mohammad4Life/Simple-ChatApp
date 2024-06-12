using Api.Application.Exceptions;
using Api.Application.Utils;
using Api.DataAccess.Models;
using Api.DataAccess.UnitOfWork;
using Api.Shared.ModelDTOs.ApplicationUsers.Commands;
using Api.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Api.Shared.Enums;

namespace Api.Application.Services.ApplicationUsers.Commands;

public class RegisterPhoneNumberCommandHandler : IRequestHandler<RegisterPhoneNumberCommand, RegisterPhoneNumberResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    //private readonly RandomVerificationCodeGenerator _generator;
    public readonly ISmsProvider _smsProvider;
    public RegisterPhoneNumberCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, /*RandomVerificationCodeGenerator*/ /*generator*/, ISmsProvider smsProvider)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        //_generator = generator;

    }

    public async Task<RegisterPhoneNumberResponse> Handle(RegisterPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.ApplicationUserRepository.GetByPhoneNumber(request.Command.PhoneNumber);

            if(user == null)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                
                applicationUser.PhoneNumber = request.Command.PhoneNumber;

                string verificationCode = RandomVerificationCodeGenerator.GenerateVerificationCode();
                
                applicationUser.VerificationCode = verificationCode;

                await _userManager.CreateAsync(applicationUser);

                await _userManager.AddToRoleAsync(applicationUser, "User");
                
                await _unitOfWork.CommitAsync(cancellationToken);

                var result = await _smsProvider.SendVerificationCode(verificationCode, request.Command.PhoneNumber);

                return new RegisterPhoneNumberResponse(AccountCreationState.NumberProvided);
            }
            else
            {
                string verificationCode = RandomVerificationCodeGenerator.GenerateVerificationCode();

                user.VerificationCode = verificationCode;

                await _userManager.UpdateAsync(user);

                await _unitOfWork.CommitAsync(cancellationToken);

                var result = _smsProvider.SendVerificationCode(verificationCode, request.Command.PhoneNumber);

                return new RegisterPhoneNumberResponse(AccountCreationState.NumberProvided);
            }
        }
        catch(CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
