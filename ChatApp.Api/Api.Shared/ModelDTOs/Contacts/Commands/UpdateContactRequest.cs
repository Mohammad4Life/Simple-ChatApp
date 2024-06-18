namespace Api.Shared.ModelDTOs.Contacts.Commands;

public record UpdateContactRequest(int Id, string FirstName, string LastName, string PhoneNumber, string UserId);