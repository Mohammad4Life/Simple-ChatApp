using Api.Application.Exceptions;
using Api.DataAccess.Models;
using Api.DataAccess.UnitOfWork;
using Api.Shared.ModelDTOs.ApplicationUsers.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Api.Application.Services.ApplicationUsers.Queries;

public class GetByUserNameCommandHandler : IRequestHandler<GetByUserNameCommand, GetByUserNameResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    public GetByUserNameCommandHandler(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<GetByUserNameResponse> Handle(GetByUserNameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.ApplicationUserRepository.GetByUserName(request.Command.UserName);

            if (user == null)
                throw new CustomException(Message: "نتیجه ای یافت نشد :(");

            //string profilePhotoUrl
            //if ()

            throw new NotImplementedException();
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
