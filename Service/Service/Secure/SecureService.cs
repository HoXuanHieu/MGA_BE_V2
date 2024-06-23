using Models;
using Models.Entities;
using Models.Request;
using Models.Response;
using Service.Helper;
using Web_API.ResponseModel;

namespace Service;
public class SecureService : ISecureService
{
    public Task<UserResponse> LoginAsync(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<UserResponse>> RegisterAsync(UserRegisterRequest request)
    {
        var userRegister = new UserEntity()
        {
            UserName = request.UserName,
            Email = request.Email,
            Role = request.Role,
        };
        BcryptHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        userRegister.PasswordSalt = passwordSalt;
        userRegister.PasswordHash = passwordHash;
        // save user
        var result = GetUserResponse(userRegister);
        //logic return status code 
        var apiRespone = new ApiResponse<UserResponse>("hehe", result, 200);
        return apiRespone;
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
}

