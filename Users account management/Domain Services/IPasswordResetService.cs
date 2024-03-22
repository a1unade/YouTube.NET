namespace Users_account_management.Domain_Services
{
    public interface IPasswordResetService
    {
        bool RequestPasswordReset(string email);
        bool ResetPassword(string email, string code, string newPassword);
    }
}
