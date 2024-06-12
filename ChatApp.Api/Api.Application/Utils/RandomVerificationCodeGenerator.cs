namespace Api.Application.Utils;

public static class RandomVerificationCodeGenerator
{
    public static  string GenerateVerificationCode(int Length = 6)
    {
        string verificationCode = string.Empty;
        for (int i = 0; i < Length; i++)
        {
            Random rng = new Random();
            int num = rng.Next(0, 10);
            verificationCode = verificationCode + num.ToString();
        }

        return verificationCode;
    }
}
