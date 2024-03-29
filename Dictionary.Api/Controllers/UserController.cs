﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.DtoEdit.Authentication;
using Dictionary.Service.Exceptions;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers;

[Route("api/[controller]s")]
public class UserController : BaseController<UserEntity>
{
    private readonly IUserRepo _userRepo;
    
    private readonly IUserService _userService;
    
    public UserController(IUserService userService, IUserRepo userRepo, IServiceProvider serviceProvider) : base(
        userService, userRepo, serviceProvider)
    {
        _userService = userService;
        _userRepo = userRepo;
    }
    
    [HttpGet("info")]
    public async Task<IActionResult> GetUserInfo()
    {
        var contextData = _contextService.Get();
        try
        {
            var res = await _userRepo.GetUserInfo(contextData.UserId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                    new List<UserEntity>(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult =
                new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    [HttpGet("getUser/{userId}")]
    public async Task<IActionResult> getUser(Guid userId)
    {
        try
        {
            var res = await _userRepo.GetUserInfo(userId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                    new List<UserEntity>(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var res = await _userService.Login(model);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                    new List<UserEntity>(), "204");
                return Ok(actionResult);
            }
        }
        catch (ValidateException exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, exception.Message, "", 0, "400");
            return Ok(actionResult);
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            Console.WriteLine(exception.StackTrace);
            return Ok(actionResult);
        }
    }
    
    [HttpGet("token")]
    public async Task<IActionResult> GetToken()
    {
        var contextData = _contextService.Get();
        try
        {
            var res = await _userService.GetToken(contextData.UserId);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, Resources.getDataSuccess, "", res, null);
                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, Resources.noReturnData, "",
                    new List<UserEntity>(), "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

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
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }
    
    [HttpPut("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
    {
        try
        {
            var contextService = _contextService.Get();
            resetPassword.user_id = contextService.UserId;
            await _userService.ResetPassword(resetPassword);

            var actionResult = new ServiceResult((int)ApiStatus.Success, "Cập nhật mật khẩu thành công!", "", null, null);

            return Ok(actionResult);
        }
        catch (ValidateException exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, exception.Message, "", 0, "400");
            return Ok(actionResult);
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }
    
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
                var actionResult = new ServiceResult((int)ApiStatus.Success, "Cập nhật thông tin tài khoản thành công!", "", null, null);

                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, "Cập nhật thất bại!", "",
                    null, "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }

    [HttpPut("updateStatus")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateUser userUpdate)
    {
        try
        {
            var res = await _userService.UpdateStatus(userUpdate.is_block, userUpdate.user_id);
            if (res != null)
            {
                var actionResult = new ServiceResult((int)ApiStatus.Success, "Cập nhật thông tin khách hàng thành công!", "", null, null);

                return Ok(actionResult);
            }
            else
            {
                var actionResult = new ServiceResult((int)ApiStatus.Fail, "Cập nhật thất bại!", "",
                    null, "204");
                return Ok(actionResult);
            }
        }
        catch (Exception exception)
        {
            var actionResult = new ServiceResult((int)ApiStatus.Exception, Resources.error, exception.Message, null, "500");
            return Ok(actionResult);
        }
    }
}