namespace Api.Shared.ModelDTOs.Contacts.Commands;

//Note that user Id is a refrence to an existing ApplicationUser which is responsible for the contact that we add to our list.
public record AddNewContactRequest(string Name, string LastName, string PhoneNumber, string UserId);