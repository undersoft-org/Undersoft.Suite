namespace Undersoft.SDK.Service.Server.Accounts.Email;

public static class EmailTemplate
{
    public static string GetVerificationCodeMessage(string token)
    {
        return CodeMessage.Replace("{verification_code}", token);
    }

    public static string GetResetPasswordMessage(string password)
    {
        return PasswordMessage.Replace("{generated_password}", password);
    }

    public const string CodeMessage = "<table style=\"border-collapse: collapse; width: 100%; height: 36px;\" border=\"0\"><tbody><tr style=\"height: 18px;\"><td style=\"width: 100%; text-align: center; height: 18px;\"><h2><strong>YOUR VERIFICATION CODE</strong></h2></td></tr><tr style=\"height: 18px;\"><td style=\"width: 100%; text-align: center; height: 18px;\"><h1><span style=\"color: #339966;\">{verification_code}</span></h1></td></tr></tbody></table>";

    public const string PasswordMessage = "<table style=\"border-collapse: collapse; width: 100%; height: 36px;\" border=\"0\"><tbody><tr style=\"height: 18px;\"><td style=\"width: 100%; text-align: center; height: 18px;\"><h2><strong>YOUR GENERATED PASSWORD</strong></h2></td></tr><tr style=\"height: 18px;\"><td style=\"width: 100%; text-align: center; height: 18px;\"><h1><span style=\"color: #339966;\">{generated_password}</span></h1></td></tr></tbody></table>";
}