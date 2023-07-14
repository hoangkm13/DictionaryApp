using Dictionary.Service.Model;

namespace Dictionary.Service.DtoEdit.Authentication;

public class UserInfo : UserEntity
{
    public string day { get; set; }
    public string month { get; set; }
    public string year { get; set; }
}