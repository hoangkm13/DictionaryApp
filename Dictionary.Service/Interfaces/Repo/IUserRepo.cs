using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Repo;

/// <summary>
///     Interface Repo Người dùng
/// </summary>
public interface IUserRepo : IBaseRepo
{
    /// <summary>
    ///     Lấy thông tin người dùng
    /// </summary>
    /// <param name="id">id người dùng</param>
    Task<UserInfo> GetUserInfo(Guid id);

    /// <summary>
    ///     Đăng nhập
    /// </summary>
    /// <param name="model"></param>
    Task<UserEntity> Login(LoginModel model);
}