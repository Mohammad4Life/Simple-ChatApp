using Api.Shared.Enums;

namespace Api.Shared.ModelDTOs.ApplicationUsers.Commands;

public record VerifyPhoneNumberResponse(AccountCreationState State);