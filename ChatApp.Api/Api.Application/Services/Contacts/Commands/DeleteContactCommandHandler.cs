using Api.Application.Exceptions;
using Api.DataAccess.UnitOfWork;
using Api.Shared.ModelDTOs.Contacts.Commands;
using MediatR;

namespace Api.Application.Services.Contacts.Commands;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, DeleteContactResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteContactCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteContactResponse> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var contact = await _unitOfWork.ContactRepository.Get(request.Command.Id);

            await _unitOfWork.ContactRepository.Delete(contact);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new DeleteContactResponse(Successed: true);
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
