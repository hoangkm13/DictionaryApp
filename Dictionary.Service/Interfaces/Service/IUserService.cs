using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit.Authentication;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Service;

/// <summary>
///     Interface service Người dùng
/// </summary>
public interface IUserService : IBaseService
{
    /// <summary>
    ///     Đăng nhập
    /// </summary>
    /// <param name="model"></param>
    Task<Dictionary<string, object>> Login(LoginModel model);

    /// <summary>
    ///     Đăng ký
    /// </summary>
    /// <param name="model"></param>
    Task<ServiceResult> Signup(SignupModel model);

    /// <summary>
    ///     Reset Password
    /// </summary>
    /// <param name="resetPassword"></param>
    Task ResetPassword(ResetPassword resetPassword);

    /// <summary>
    ///     update user info
    /// </summary>
    /// <param name="updateUser"></param>
    /// <returns></returns>
    Task<UserInfo> UpdateUser(UpdateUser updateUser);

    /// <summary>
    ///     Lấy token
    /// </summary>
    Task<Dictionary<string, object>> GetToken(Guid userId);

    /// <summary>
    ///     chập nhật trạng thái
    /// </summary>
    Task<UserEntity> UpdateStatus(bool status, Guid userId);
}