using System;

namespace Dictionary.Service.DtoEdit.Authentication;

public class ResetPassword
{

    /// <summary>
    ///     password hiện tại
    /// </summary>
    public string old_password { get; set; }

    /// <summary>
    ///     new_password
    /// </summary>
    public string new_password { get; set; }
}