using Api.DataAccess.Utils;
using Kavenegar;
using Microsoft.Extensions.Logging;

namespace Api.Shared.Services;

public interface ISmsProvider
{
    Task<bool> SendVerificationCode(string VerificationCode, string PhoneNumber);
}

public class SmsProvider : ISmsProvider
{
    private readonly ILogger<SmsProvider> _logger;
    public SmsProvider(ILogger<SmsProvider> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendVerificationCode(string VerificationCode, string PhoneNumber)
    {
        string Message = $"با سلام کد زیر جهت احراز هویت شما ارسال شده است. لطفا آن را در اختیار کس دیگری قرار ندهید.\n{VerificationCode}";

        //Pass the sms system token later when you bought kavenegar dashboard.
        Kavenegar.KavenegarApi api = new KavenegarApi(StaticVariables.SmsApiKey);

        //Pass the sender number later when you bought kavenegar dashboard.
        await api.Send(StaticVariables.SmsSender, PhoneNumber, Message);

        return true;

    }
}