using Api.Application.Exceptions;
using Api.DataAccess.Models;
using Api.DataAccess.UnitOfWork;
using Api.Shared.ModelDTOs.Contacts.Commands;
using AutoMapper;
using MediatR;

namespace Api.Application.Services.Contacts.Commands;

public class AddNewContactCommandHandler : IRequestHandler<AddNewContactCommand, AddNewContactResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddNewContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AddNewContactResponse> Handle(AddNewContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _unitOfWork.ContactRepository.CheckDuplicate(x => x.ContactPhoneNumber == request.Command.PhoneNumber);

            if (result)
                throw new CustomException("مخاطب از قبل موجود است.");

            var check = await _unitOfWork.ApplicationUserRepository.GetByPhoneNumber(request.Command.PhoneNumber);

            if(check == null)
                throw new CustomException("این شماره حساب چت ندارد.");

            var contact = _mapper.Map<Contact>(request.Command);

            contact.UserId = check.Id;

            await _unitOfWork.ContactRepository.AddAsync(contact);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new AddNewContactResponse(contact.Id);
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
