using System;
using Dictionary.Service.Attributes;
using Dictionary.Service.Constants;

namespace Dictionary.Service.Model;

/// <summary>
///     Bảng thông tin người dùng
///     <summary>
[Table("user")]
public class UserEntity
{

    [Key]
    public Guid user_id { get; set; }

    public string email { get; set; }
    
    public string password { get; set; }
    
    public string full_name { get; set; }
    
    public int status { get; set; }
    
    public string user_name { get; set; }
    
    public DateTime? birthday { get; set; }
    
    public string position { get; set; }
    
    public string avatar { get; set; }
    
    public string display_name { get; set; }

    public DateTime created_at { get; set; }
    
    public DateTime modified_at { get; set; }

    public string verify_token { get; set; }
}