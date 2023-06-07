﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.Contexts;
using Dictionary.Service.DtoEdit;
using Dictionary.Service.Exceptions;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Interfaces.Service;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Dictionary.Service.Service;

public class UserService : BaseService, IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo,
        IHttpContextAccessor httpContextAccessor) : base(userRepo)
    {
        _userRepo = userRepo;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Dictionary<string, object>> Login(LoginModel model)
    {
        var user = await _userRepo.Login(model);
        var context = new ContextData();
        if (user != null)
        {
            context.UserId = user.user_id;
            context.Email = user.email;
            context.FirstName = user.first_name;
            context.LastName = user.last_name;
            context.Avatar = user.avatar;
            context.CartId = user.cart_id;
            context.Role = user.role;
        }

        var jwtTokenConfig =
            JsonConvert.DeserializeObject<JwtTokenConfig>(_userRepo.GetConfiguration()
                .GetConnectionString("JwtTokenConfig"));

        var data = await GetContextReturn(context, jwtTokenConfig);
        return data;
    }

    /// <summary>
    ///     Reset Password
    /// </summary>
    public async Task ResetPassword(ResetPassword resetPassword)
    {
        var field = "user_id";
        object value = resetPassword.user_id;

        var existedUser = await _userRepo.GetAsync<UserEntity>(field, value);
        var res = existedUser.FirstOrDefault();

        if (res == null) throw new ValidateException("User doesn't exist", "");
        var verified = BCrypt.Net.BCrypt.Verify(resetPassword.password, res.password);
        if (!verified)
            throw new ValidateException("Mật khẩu không chính xác, vui lòng kiểm tra lại", 0,
                int.Parse(ResultCode.WrongPassword));
        var encodedData = BCrypt.Net.BCrypt.HashPassword(resetPassword.new_password);
        res.password = encodedData;

        await _userRepo.UpdateAsync<UserEntity>(res, "password");
    }


    public async Task<UserInfo> UpdateUser(UpdateUser updateUser)
    {
        var field = "user_id";
        object value = updateUser.user_id;

        var existedUser = await _userRepo.GetAsync<UserEntity>(field, value);
        var res = existedUser.FirstOrDefault();

        if (res == null) throw new ValidateException("User doesn't exist", "");

        res.first_name = updateUser.first_name;
        res.last_name = updateUser.last_name;
        res.email = updateUser.email;
        res.phone = updateUser.phone;
        res.avatar = Common.SaveImage(_httpContextAccessor.HttpContext.Request.Host.Value, updateUser.avatar);
        res.gender = updateUser.gender;
        res.date_of_birth = updateUser.date_of_birth;

        await _userRepo.UpdateAsync<UserEntity>(res);

        var data = await _userRepo.GetUserInfo(updateUser.user_id);

        return data;
    }

    public async Task<DAResult> Signup(SignupModel model)
    {
        var newUser = new UserEntity();
        newUser.user_id = Guid.NewGuid();
        newUser.cart_id = Guid.NewGuid();
        newUser.email = model.email;
        newUser.phone = model.phone;
        newUser.password = BCrypt.Net.BCrypt.HashPassword(model.password);
        newUser.first_name = model.first_name.Trim();
        newUser.last_name = model.last_name.Trim();
        // newUser.avatar = Common.SaveImage(_httpContextAccessor.HttpContext.Request.Host.Value, model.avatar);
        newUser.is_block = false;
        newUser.role = Role.Customer;
        newUser.gender = model.gender;
        // Check tồn tại Email
        var existUserEmail = (await _userRepo.GetAsync<UserEntity>("email", newUser.email))?.FirstOrDefault();
        if (existUserEmail != null)
            return new DAResult(int.Parse(ResultCode.ExistEmail), Resources.msgExistEmail, "", newUser);
        // Check tồn tại Số điện thoại
        var existUserPhone = (await _userRepo.GetAsync<UserEntity>("phone", newUser.phone))?.FirstOrDefault();
        if (existUserPhone != null)
            return new DAResult(int.Parse(ResultCode.ExistPhone), Resources.msgExistPhone, "", newUser);
        var user = await _userRepo.InsertAsync<UserEntity>(newUser);
        
        var context = new ContextData();
        if (user != null)
        {
            context.UserId = user.user_id;
            context.Email = user.email;
            context.FirstName = user.first_name;
            context.LastName = user.last_name;
            context.Avatar = user.avatar;
            context.CartId = user.cart_id;
            context.Role = user.role;
        }

        var jwtTokenConfig =
            JsonConvert.DeserializeObject<JwtTokenConfig>(_userRepo.GetConfiguration()
                .GetConnectionString("JwtTokenConfig"));

        var data = await GetContextReturn(context, jwtTokenConfig);
        return new DAResult(200, Resources.signupSuccess, "", data);
    }

    public async Task<Dictionary<string, object>> GetToken(Guid userId)
    {
        var user = await _userRepo.GetByIdAsync<UserEntity>(userId);
        var context = new ContextData();
        if (user != null)
        {
            context.UserId = user.user_id;
            context.Email = user.email;
            context.FirstName = user.first_name;
            context.LastName = user.last_name;
            context.Avatar = user.avatar;
            context.CartId = user.cart_id;
            context.Role = user.role;
        }

        var jwtTokenConfig =
            JsonConvert.DeserializeObject<JwtTokenConfig>(_userRepo.GetConfiguration()
                .GetConnectionString("JwtTokenConfig"));

        var data = await GetContextReturn(context, jwtTokenConfig);
        return data;
    }

    public async Task<UserEntity> UpdateStatus(bool status, Guid userId)
    {
        var user = await _userRepo.GetByIdAsync<UserEntity>(userId);
        if (user != null)
        {
            user.is_block = status;
            return await _userRepo.UpdateAsync<UserEntity>(user, nameof(UserEntity.is_block));
        }

        return user;
    }

    /// <summary>
    ///     Lấy thông tin trả về client
    /// </summary>
    /// <param name="user"></param>
    /// <param name="expiredSeconds">Thời gian hết hạn</param>
    private async Task<Dictionary<string, object>> GetContextReturn(ContextData context,
        JwtTokenConfig jwtTokenConfig)
    {
        var token = CreateAuthenToken(context, jwtTokenConfig);
        var result = new Dictionary<string, object>
        {
            { "Token", token },
            { "TokenTimeout", jwtTokenConfig.ExpiredSeconds },
            {
                "Context", new
                {
                    context.UserId,
                    context.Email,
                    context.FirstName,
                    context.LastName,
                    Avatar = Common.GetUrlImage(_httpContextAccessor.HttpContext.Request.Host.ToString(),
                        context.Avatar),
                    context.CartId,
                    context.TokenExpired,
                    context.Role
                }
            }
        };
        return result;
    }

    /// <summary>
    ///     Tạo token Authen
    /// </summary>
    private string CreateAuthenToken(ContextData context, JwtTokenConfig jwtTokenConfig)
    {
        var claimIdentity = new ClaimsIdentity();
        claimIdentity.AddClaim(new Claim(TokenKeys.UserId, context.UserId.ToString()));
        claimIdentity.AddClaim(new Claim(TokenKeys.Email, context.Email));
        claimIdentity.AddClaim(new Claim(TokenKeys.FirstName, context.FirstName));
        claimIdentity.AddClaim(new Claim(TokenKeys.LastName, context.LastName));
        claimIdentity.AddClaim(new Claim(TokenKeys.CartId, context.CartId.ToString()));

        var expire = DateTime.Now.AddSeconds(jwtTokenConfig.ExpiredSeconds);
        context.TokenExpired = expire;
        claimIdentity.AddClaim(new Claim(TokenKeys.TokenExpired, context.TokenExpired.ToString()));
        var key = Encoding.ASCII.GetBytes(jwtTokenConfig.SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = expire,
            Subject = claimIdentity,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var smeToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(smeToken);
        return token;
    }
}