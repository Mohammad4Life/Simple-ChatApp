using Api.Application.Exceptions;
using Api.DataAccess.Models;
using Api.DataAccess.UnitOfWork;
using Api.Shared.ModelDTOs.Contacts.Commands;
using AutoMapper;
using MediatR;

namespace Api.Application.Services.Contacts.Commands;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, UpdateContactResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateContactCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateContactResponse> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var contact = await _unitOfWork.ContactRepository.Get(request.Command.Id);

            _mapper.Map<UpdateContactRequest, Contact>(request.Command, contact);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new UpdateContactResponse(contact.Id);
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
