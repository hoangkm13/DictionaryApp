using System;

namespace Dictionary.Service.DtoEdit;

public class UpdateUser
{
    /// <summary>
    ///     user_id
    /// </summary>
    public Guid user_id { get; set; }

    /// <summary>
    ///     Họ người dùng
    ///     <summary>
    public string first_name { get; set; }

    /// <summary>
    ///     Tên người dùng
    ///     <summary>
    public string last_name { get; set; }

    /// <summary>
    ///     Số điện thoại người dùng
    ///     <summary>
    public string phone { get; set; }

    /// <summary>
    ///     Giới tính
    /// </summary>
    public int? gender { get; set; }

    /// <summary>
    ///     Ngày sinh
    /// </summary>
    public DateTime? date_of_birth { get; set; }

    /// <summary>
    ///     user_id
    /// </summary>
    public string email { get; set; }

    public bool is_block { get; set; }
}