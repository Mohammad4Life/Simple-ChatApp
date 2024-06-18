namespace Api.Shared.ModelDTOs.Contacts.Commands;

public record UpdateContactRequest(int Id, string Name, string LastName, string PhoneNumber, string UserId);