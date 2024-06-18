using Api.Application.Exceptions;
using Api.DataAccess.UnitOfWork;
using Api.Shared.ModelDTOs.Contacts.Queries;
using System.Data;
using Dapper;
using MediatR;

namespace Api.Application.Services.Contacts.Queries;

public class GetAllContactsCommandHandler : IRequestHandler<GetAllContactsCommand, List<GetAllContactsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllContactsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllContactsResponse>> Handle(GetAllContactsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var parameters = new DynamicParameters();

            var contacts = await _unitOfWork.ApplicationReadDbConnection.QueryAsync<GetAllContactsResponse>("GetAllContacts", parameters, null, CommandType.StoredProcedure, cancellationToken);

            //I should prolly convert profile photo guid to a url with file service at this point.

            return contacts;
        }
        catch (CustomException ex)
        {
            throw new CustomException(ex.Message);
        }
    }
}
