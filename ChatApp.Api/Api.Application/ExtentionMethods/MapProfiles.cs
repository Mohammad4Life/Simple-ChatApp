using Api.DataAccess.Models;
using Api.Shared.ModelDTOs.Contacts.Commands;
using AutoMapper;

namespace Api.Application.ExtentionMethods;

public class MapProfiles : Profile
{
    public MapProfiles()
    {
        CreateMap<Contact, AddNewContactRequest>().ReverseMap();
    }
}
