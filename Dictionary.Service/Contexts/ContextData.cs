using System;
using Dictionary.Service.Constants;

namespace Dictionary.Service.Contexts;

public class ContextData
{
    /// <summary>
    ///     Id người dùng
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Email đăng nhập
    /// </summary>
    public string Email { get; set; }

    public string UserName { get; set; }
    
    public string DisplayName { get; set; }
    
    public string Avatar { get; set; }
    public DateTime TokenExpired { get; set; }
    
    public Guid DictionaryId { get; set; }
    
    public string DictionaryName { get; set; }
}