using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Entities;
using Models.Request;
using Models.Response;
using Repositories;
using Service.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web_API.ResponseModel;

namespace Service;
public class SecureService : ISecureService
{
    private readonly IUserRepository _userRepository;
    public SecureService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        var users = await _userRepository.GetAllUsersAsync();
        var checkUserExist = users.FirstOrDefault(x => x.UserName == request.UserName);
        if (checkUserExist == null)
            return new ApiResponse<LoginResponse>(Common.Message.VALIDATE_MESSAGE_USER_NOT_EXIST, null, 400);
        if (!PasswordHelper.VerifyPasswordHash(request.Password, checkUserExist.PasswordHash, checkUserExist.PasswordSalt))
            return new ApiResponse<LoginResponse>(Common.Message.MESSAGE_USER_lOGIN_WRONG_PASSWORD, null, 400);
        var response = new LoginResponse()
        {
            Token = CreateToken(checkUserExist),
            UserResponse = GetUserResponse(checkUserExist)
        };
        return new ApiResponse<LoginResponse>(Common.Message.MESSAGE_USER_LOGIN_SUCCESSFUL, response, 200);
    }

    public async Task<ApiResponse<UserResponse>> RegisterAsync(UserRegisterRequest request)
    {
        string checkUser = await this.checkExistUser(request);
        if (!checkUser.IsNullOrEmpty())
            return new ApiResponse<UserResponse>(checkUser, null, 400);
        var userRegister = new UserEntity()
        {
            UserName = request.UserName,
            Email = request.Email,
            Role = request.Role,
        };
        PasswordHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        userRegister.PasswordSalt = passwordSalt;
        userRegister.PasswordHash = passwordHash;
        // save user
        try
        {
            var user = await _userRepository.CreateUserAsync(userRegister);
            var result = GetUserResponse(userRegister);
            if (result == null)
            {
                return new ApiResponse<UserResponse>(Common.Message.MESSAGE_USER_REGISTER_FAIL, result, 500);
            }
            //logic return status code 
            var apiRespone = new ApiResponse<UserResponse>(Common.Message.MESSAGE_USER_REGISTER_SUCCESSFUL, result, 200);
            return apiRespone;
        }
        catch (Exception ex)
        {
            string message = $"Error occurs when create user: {ex.Message}";
            return new ApiResponse<UserResponse>(message, null, 500);
        }

    }

    private async Task<String> checkExistUser(UserRegisterRequest request)
    {
        var users = await _userRepository.GetAllUsersAsync();
        if (users.Select(x => x.UserName).Contains(request.UserName))
            return Common.Message.VALIDATE_MESSAGE_USER_NAME_DUPLICATE;
        else if (users.Select(x => x.Email).Contains(request.Email))
            return Common.Message.VALIDATE_MESSAGE_USER_EMAIL_DUPLICATE;
        return "";
    }

    private UserResponse GetUserResponse(UserEntity user)
    {
        return new UserResponse()
        {
            UserId = user.UserId.ToString(),
            UserName = user.UserName,
            Email = user.Email,
            DateCreate = user.DateCreate,
            IsSuspension = user.IsSuspension
        };
    }

    private String CreateToken(UserEntity user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            "abcdefghijklmnopqrstuvwsyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789-_.@"));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}

