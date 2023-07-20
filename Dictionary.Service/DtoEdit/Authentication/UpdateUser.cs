using System;

namespace Dictionary.Service.DtoEdit.Authentication;

public class UpdateUser
{
    /// <summary>
    ///     user_id
    /// </summary>
    public Guid user_id { get; set; }

    public string displayName { get; set; }

    public string fullName { get; set; }

    public string birthday { get; set; }
    
    public string position { get; set; }
    
    public string avatar { get; set; }
}