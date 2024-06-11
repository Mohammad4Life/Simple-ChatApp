namespace Api.Shared.ModelDTOs.ApplicationUsers.Commands;

public record AddAcountDetailsRequest(string FirstName, string LastName, DateTime BirthDate, string UserName);