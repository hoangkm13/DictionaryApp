namespace Dictionary.Service.DtoEdit.Authentication;

public class ResetPasswordByToken
{
    public string verifyToken { get; set; }

    public string newPassword { get; set; }
}