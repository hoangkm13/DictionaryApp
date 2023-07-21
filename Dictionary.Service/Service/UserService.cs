using System;
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
    public async Task<ServiceResult> ResetPassword(ResetPassword resetPassword)
    {
        Guid user_id = new Guid("b0da65f9-dc39-11ed-a1e6-a44cc8756a37");
        var existedUser = await _userRepo.GetAsync<UserEntity>("user_id", user_id);
        var res = existedUser.FirstOrDefault();

        if (res == null)
        {
            return new ServiceResult(
                2, 
                "Người dùng không tồn tại", 
                "", 
                null,
                "400"
            );
        }
        var verified = resetPassword.old_password == res.password;
        if (!verified)
        {
            return new ServiceResult(
                2, 
                "Mật khẩu cũ không chính xaác!", 
                "", 
                null,
                "1000"
            );
        }

        res.password = resetPassword.new_password;

        await _userRepo.UpdateAsync<UserEntity>(res, "password");
        
        return new ServiceResult(
            1, 
            "Cập nhật mật khẩu thành công!", 
            "", 
            null,
            ""
            );
    }
    
    public async Task<Dictionary<string, object>> UpdateUser(UpdateUser updateUser)
    {
        var field = "user_id";
        object value = updateUser.user_id;

        var existedUser = await _userRepo.GetAsync<UserEntity>(field, value);
        var res = existedUser.FirstOrDefault();

        if (res == null) return null;
        res.birthday = DateTime.Parse(updateUser.birthday);
        res.avatar = updateUser.avatar;
        res.full_name = updateUser.fullName;
        res.position = updateUser.position;
        res.display_name = updateUser.displayName;

        await _userRepo.UpdateAsync<UserEntity>(res);

        return new Dictionary<string, object>
        {
            {"displayName", res.display_name},
            {"avatar", res.avatar}
        };
    }

    public async Task<ServiceResult> Signup(SignupModel model)
    {
        var newUser = new UserEntity();
        newUser.user_id = Guid.NewGuid();
        newUser.email = model.username;
        newUser.password = model.password;

        // Check tồn tại Email
        var existUserEmail = (await _userRepo.GetAsync<UserEntity>("email", newUser.email))?.FirstOrDefault();
        if (existUserEmail != null)
            return new ServiceResult(
                2, 
                Resources.msgExistEmail, 
                "", 
                null,
                "1001");
        
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
        return new ServiceResult(1, Resources.signupSuccess, "", data, "");
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

    public async Task<ServiceResult> ForgotPassword(ForgotModel forgotModel) 
    {
        var user = await _userRepo.GetAsync<UserEntity>("email", forgotModel.email);

        var foundUser = user.FirstOrDefault();
        
        if (foundUser == null)
        {
            return new ServiceResult(2, "Không tồn tại email người dùng!" , "", null, "1002");
        }

        return new ServiceResult(1, "Send verify token successfully!", "", null, "");
    }

    public async Task<ServiceResult> ActiveAccount()
    {
        string verifyToken = "verify_token_example";
        Guid user_id = new Guid("b0da65f9-dc39-11ed-a1e6-a44cc8756a37");

        var query_user = await _userRepo.GetAsync<UserEntity>("user_id", user_id);

        var user = query_user.FirstOrDefault();

        if (user == null)
        {
            return new ServiceResult(2, "User not exist!", "", null, "1001");
        }

        if (user.verify_token != verifyToken)
        {
            return new ServiceResult(2, "Token không hợp lệ!", "", null, "1003");
        }

        if (user.status == 1)
        {
            return new ServiceResult(2, "Người dùng đã kích hoạt tài khoản!", "", null, "9000");
        }

        user.status = 1;
        user.verify_token = "";

        await UpdateAsync<UserEntity>(user);
        
        return new ServiceResult(1, "Kích hoạt tài khoản thành công!", "", null, "");
    }

    public async Task<ServiceResult> ResetPasswordByVerifyToken(ResetPasswordByToken resetPasswordByToken)
    {
        string verifyToken = resetPasswordByToken.verifyToken;
        Guid user_id = new Guid("b0da65f9-dc39-11ed-a1e6-a44cc8756a37");
        
        var query_user = await _userRepo.GetAsync<UserEntity>("user_id", user_id);

        var user = query_user.FirstOrDefault();
        
        if (user == null)
        {
            return new ServiceResult(2, "User not exist!", "", null, "1001");
        }

        if (user.verify_token != verifyToken)
        {
            return new ServiceResult(2, "Token không hợp lệ!", "", null, "1003");
        }

        user.password = resetPasswordByToken.newPassword;
        user.verify_token = "";

        await _userRepo.UpdateAsync<UserEntity>(user);

        return new ServiceResult(1, "Cập nhật mật khẩu thành công!", "", null, "");
    }

    public async Task<ServiceResult> SendActiveEmail(SendActiveEmail sendActiveEmail)
    {
        var query_user = await _userRepo.GetAsync<UserEntity>("email", sendActiveEmail.username);

        var user = query_user.FirstOrDefault();

        if (user == null)
        {
            return new ServiceResult(2, "Email chưa được đăng ký!", "", null, "1003");
        }
        
        if (user.status == 1)
        {
            return new ServiceResult(2, "Tài khoản đã được kích hoạt!", "", null, "1003");
        }

        if (user.password != sendActiveEmail.password)
        {
            return new ServiceResult(2, "Tài khoản không tồn tại!", "", null, "1003");
        }

        user.verify_token = "verify_token";

        await _userRepo.UpdateAsync<UserEntity>(user);
        
        return new ServiceResult(1, "Gửi token kích hoạt tài khoản thành công!", "", null, ""); 
    }

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