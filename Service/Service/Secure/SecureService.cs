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
        //var users = new List<UserEntity> { new UserEntity { UserId = Guid.NewGuid(), UserName = "hieu", Email = "aaabbababa", Role = "admin" } };
        var checkUserExist = users.FirstOrDefault(x => x.UserName == request.UserName);
        if (checkUserExist == null)
            return new ApiResponse<LoginResponse>("UserName is not existed", null, 400);
        if (!PasswordHelper.VerifyPasswordHash(request.Password, checkUserExist.PasswordHash, checkUserExist.PasswordSalt))
            return new ApiResponse<LoginResponse>("Wrong password! Please enter again", null, 400);
        var response = new LoginResponse()
        {
            Token = CreateToken(checkUserExist),
            UserResponse = GetUserResponse(checkUserExist)
        };
        return new ApiResponse<LoginResponse>("login success", response, 200);
    }

    public async Task<ApiResponse<UserResponse>> RegisterAsync(UserRegisterRequest request)
    {
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
            if(result == null)
            {
                return new ApiResponse<UserResponse>("Register new user fail", result, 500);
            }
            //logic return status code 
            var apiRespone = new ApiResponse<UserResponse>("User register successful", result, 200);
            return apiRespone;
        }
        catch (Exception ex)
        {
            string message = $"Error occurs when create user: {ex.Message}";
            return new ApiResponse<UserResponse>(message, null, 500);
        }

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

