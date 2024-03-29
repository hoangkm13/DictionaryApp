﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Service.Constants;
using Dictionary.Service.Contexts;
using Dictionary.Service.DtoEdit.Authentication;
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
    private readonly IDictionaryRepo _dictionaryRepo;

    public UserService(
        IUserRepo userRepo,
        IDictionaryRepo dictionaryRepo,
        IHttpContextAccessor httpContextAccessor) : base(userRepo)
    {
        _userRepo = userRepo;
        _httpContextAccessor = httpContextAccessor;
        _dictionaryRepo = dictionaryRepo;
    }

    public async Task<Dictionary<string, object>> Login(LoginModel model)
    {
        var user = await _userRepo.Login(model);
        var context = new ContextData();
        if (user != null)
        {
            var dictionary = await _dictionaryRepo.GetAsync<DictionaryEntity>("user_id", user.user_id);
            
            DictionaryEntity lastDictionary = dictionary
                .MaxBy(r => r.last_view_at);
            
            context.UserId = user.user_id;
            context.Email = user.email;
            context.DisplayName = user.display_name;
            context.Avatar = user.avatar;
            context.UserName = user.user_name;
            context.DictionaryId = lastDictionary.dictionary_id;
            context.DictionaryName = lastDictionary.dictionary_name;
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

        if (res == null) throw new ValidateException("User doesn't exist", "", 400);
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

        if (res == null) throw new ValidateException("User doesn't exist",  null, 400);
        res.email = updateUser.email;
        res.birthday = updateUser.date_of_birth;

        await _userRepo.UpdateAsync<UserEntity>(res);

        var data = await _userRepo.GetUserInfo(updateUser.user_id);

        return data;
    }

    public async Task<ServiceResult> Signup(SignupModel model)
    {
        var newUser = new UserEntity();
        newUser.user_id = Guid.NewGuid();
        newUser.email = model.email;
        newUser.password = BCrypt.Net.BCrypt.HashPassword(model.password);
        // newUser.avatar = Common.SaveImage(_httpContextAccessor.HttpContext.Request.Host.Value, model.avatar);
        // Check tồn tại Email
        var existUserEmail = (await _userRepo.GetAsync<UserEntity>("email", newUser.email))?.FirstOrDefault();
        // if (existUserEmail != null)
            // return new ServiceResult(int.Parse(ResultCode.ExistEmail), Resources.msgExistEmail, "", newUser);
        // Check tồn tại Số điện thoại
        var user = await _userRepo.InsertAsync<UserEntity>(newUser);
        
        var context = new ContextData();
        if (user != null)
        {
            context.UserId = user.user_id;
            context.Email = user.email;
        }

        var jwtTokenConfig =
            JsonConvert.DeserializeObject<JwtTokenConfig>(_userRepo.GetConfiguration()
                .GetConnectionString("JwtTokenConfig"));

        var data = await GetContextReturn(context, jwtTokenConfig);
        // return new ServiceResult(200, Resources.signupSuccess, "", data);
        return null;
    }

    public async Task<Dictionary<string, object>> GetToken(Guid userId)
    {
        var user = await _userRepo.GetByIdAsync<UserEntity>(userId);
        var context = new ContextData();
        if (user != null)
        {
            context.UserId = user.user_id;
            context.Email = user.email;
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
        // if (user != null)
        // {
        //     return await _userRepo.UpdateAsync<UserEntity>(user, nameof(UserEntity.is_block));
        // }

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
            { "token", token },
            { "userId", context.UserId },
            { "username", context.UserName},
            { "displayName", context.DisplayName},
            { "avatar", context.Avatar},
            { "dictionaryId", context.DictionaryId},
            { "dictionaryName", context.DictionaryName}
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
        // claimIdentity.AddClaim(new Claim(TokenKeys.FirstName, context.FirstName));
        // claimIdentity.AddClaim(new Claim(TokenKeys.LastName, context.LastName));

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