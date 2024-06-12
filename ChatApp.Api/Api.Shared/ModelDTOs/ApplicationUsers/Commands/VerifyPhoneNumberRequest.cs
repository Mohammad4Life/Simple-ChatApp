namespace Api.Shared.ModelDTOs.ApplicationUsers.Commands;

public record VerifyPhoneNumberRequest(string VerificationCode, string PhoneNumber);