﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Contexts;
using Dictionary.Service.DtoEdit.Authentication;
using Dictionary.Service.Exceptions;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers;

/// <summary>
///     Controller Người dùng
/// </summary>
/// CreatedBy: dqdat (20/07/2021)
[Route("api/[controller]s")]
public class UserController : BaseController<UserEntity>
{
    /// <summary>
    ///     Repo Người dùng
    /// </summary>
    private readonly IUserRepo _userRepo;

    /// <summary>
    ///     Service Người dùng
    /// </summary>
    private readonly IUserService _userService;

    /// <summary>
    ///     Hàm khởi tạo
    /// </summary>
    /// <param name="UserService"></param>
    /// <param name="UserRepo"></param>
    public UserController(IUserService userService, IUserRepo userRepo, IServiceProvider serviceProvider) : base(
        userService, userRepo, serviceProvider)
    {
        _userService = userService;
        _userRepo = userRepo;
    }

    /// <summary>
    ///     Lấy Thông tin người dùng
    /// </summary>
    [HttpGet("info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var contextData = _contextService.Get();
        try
        {
            var res = await _userRepo.GetUserInfo(contextData.UserId);
            if (res != null)
            {
                var actionResult = new ApiResult(200, Resources.getDataSuccess, "", res);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ApiResult(204, Resources.noReturnData, "", new List<UserEntity>());
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ApiResult(500, Resources.error, exception.Message, new List<UserEntity>());
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Lấy Thông tin người dùng
    /// </summary>
    [HttpGet("getUser/{userId}")]
    public async Task<IActionResult> getUser(Guid userId)
    {
        try
        {
            var res = await _userRepo.GetUserInfo(userId);
            if (res != null)
            {
                var actionResult = new ApiResult(200, Resources.getDataSuccess, "", res);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ApiResult(204, Resources.noReturnData, "", new List<UserEntity>());
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ApiResult(500, Resources.error, exception.Message, new List<UserEntity>());
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Đăng nhập
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var res = await _userService.Login(model);
            if (res != null)
            {
                var actionResult = new ApiResult(200, Resources.getDataSuccess, "", res);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ApiResult(204, Resources.noReturnData, "", null);
                return Ok(actionResult);
            }
        }
        catch (ValidateException exception)
        {
            var actionResult = new ApiResult(exception.resultCode, exception.Message, "", exception.DataErr);
            return Ok(actionResult);
        }
        catch (Exception exception)
        {
            //var actionResult = new DAResult(500, Resources.error, exception.Message, null);
            var actionResult = new ApiResult(500, exception.StackTrace, exception.Message, null);
            Console.WriteLine(exception.StackTrace);
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Lấy token
    /// </summary>
    [HttpGet("token")]
    public async Task<IActionResult> GetToken()
    {
        var contextData = _contextService.Get();
        try
        {
            var res = await _userService.GetToken(contextData.UserId);
            if (res != null)
            {
                var actionResult = new ApiResult(200, Resources.getDataSuccess, "", res);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ApiResult(204, Resources.noReturnData, "", new List<UserEntity>());
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ApiResult(500, Resources.error, exception.Message, new List<UserEntity>());
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Đăng nhập
    /// </summary>
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupModel model)
    {
        try
        {
            var res = await _userService.Signup(model);
            return Ok(res);
        }
        catch (Exception exception)
        {
            var actionResult = new ApiResult(500, Resources.error, exception.Message, null);
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Reset Password
    /// </summary>
    [HttpPut("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
    {
        try
        {
            var contextService = _contextService.Get();
            resetPassword.user_id = contextService.UserId;
            await _userService.ResetPassword(resetPassword);

            var actionResult = new ApiResult(200, "Cập nhật mật khẩu thành công!", "", 1);
            return Ok(actionResult);
        }
        catch (ValidateException exception)
        {
            var actionResult = new ApiResult(200, Resources.error, exception.Message, exception.DataErr);
            return Ok(actionResult);
        }
        catch (Exception exception)
        {
            var actionResult = new ApiResult(500, Resources.error, exception.Message, null);
            return Ok(actionResult);
        }
    }


    /// <summary>
    ///     Reset Password
    /// </summary>
    [HttpPut("update")]
    public async Task<IActionResult> updateUser([FromBody] UpdateUser userUpdate)
    {
        try
        {
            var contextService = _contextService.Get();
            userUpdate.user_id = contextService.UserId;
            var res = await _userService.UpdateUser(userUpdate);
            if (res != null)
            {
                var actionResult = new ApiResult(200, "Cập nhật thông tin tài khoản thành công!", "", res);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ApiResult(204, "Cập nhật thất bại!", "", null);
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ApiResult(500, Resources.error, exception.Message, null);
            return Ok(actionResult);
        }
    }

    /// <summary>
    ///     Cập nhật trạng thái Bị chặn/ Đang hoạt dộng
    /// </summary>
    [HttpPut("updateStatus")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateUser userUpdate)
    {
        try
        {
            var res = await _userService.UpdateStatus(userUpdate.is_block, userUpdate.user_id);
            if (res != null)
            {
                var actionResult = new ApiResult(200, "Cập nhật thông tin khách hàng thành công!", "", res);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ApiResult(204, "Cập nhật thất bại!", "", null);
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ApiResult(500, Resources.error, exception.Message, null);
            return Ok(actionResult);
        }
    }
}