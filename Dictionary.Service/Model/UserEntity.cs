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
    /// <summary>
    ///     PK
    ///     <summary>
    [Key]
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
    ///     Địa chỉ email của người dùng
    ///     <summary>
    public string email { get; set; }

    /// <summary>
    ///     Mật khẩu đăng nhập
    ///     <summary>
    public string password { get; set; }

    /// <summary>
    ///     Số điện thoại người dùng
    ///     <summary>
    public string phone { get; set; }

    /// <summary>
    ///     Quyển người dùng (Người mua hàng, Quản trị hệ thống)
    ///     <summary>
    public Role role { get; set; }

    /// <summary>
    ///     Có bị chặn hoạt động hay không
    ///     <summary>
    public bool is_block { get; set; }

    /// <summary>
    ///     Giới tính
    /// </summary>
    public int? gender { get; set; }

    /// <summary>
    ///     Ngày sinh
    /// </summary>
    public DateTime? date_of_birth { get; set; }
}