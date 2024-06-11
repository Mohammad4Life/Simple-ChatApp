using Kavenegar;
using Microsoft.Extensions.Logging;

namespace Api.Shared.Services;

public interface ISmsProvider
{
    Task<bool> SendVerificationCode(string PhoneNumber);
}

public class SmsProvider : ISmsProvider
{
    private readonly ILogger<SmsProvider> _logger;
    public SmsProvider(ILogger<SmsProvider> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendVerificationCode(string PhoneNumber)
    {
        string verificationCode = string.Empty;
        for(int i = 0; i < 6; i++)
        {
            Random rng = new Random();
            int num = rng.Next(0, 10);
            verificationCode = verificationCode + num.ToString();
        }

        string Message = $"با سلام کد زیر جهت احراز هویت شما ارسال شده است. لطفا آن را در اختیار کس دیگری قرار ندهید.\n{verificationCode}";

        //Pass the sms system token later when you bought kavenegar dashboard.
        Kavenegar.KavenegarApi api = new KavenegarApi("");

        //Pass the sender number later when you bought kavenegar dashboard.
        await api.Send("", PhoneNumber, Message);

        return true;

    }
}