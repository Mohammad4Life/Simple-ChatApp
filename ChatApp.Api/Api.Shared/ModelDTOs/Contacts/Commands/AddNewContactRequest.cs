namespace Api.Shared.ModelDTOs.Contacts.Commands;

public record AddNewContactRequest(string FirstName, string LastName, string PhoneNumber);