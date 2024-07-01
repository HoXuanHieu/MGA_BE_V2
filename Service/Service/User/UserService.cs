using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Entities;
using Models.Response;
using Repositories;
using Service.Helper;
using System.Data;
using Web_API.ResponseModel;

namespace Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<UserResponse>> CreateUserAsync(CreateUserRequest request)
    {
        //String password = PasswordHelper.GeneratePassword();
        String password = "123456";
        var data = new UserEntity()
        {
            UserName = request.UserName,
            Email = request.Email,
            Role = request.Role
        };
        PasswordHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        data.PasswordHash = passwordHash;
        data.PasswordSalt = passwordSalt;
        var response = await _repository.CreateUserAsync(data);
        if (response.Equals(Common.Message.MESSAGE_USER_CREATE_SUCCESSFUL))
        {
            var user = await _repository.GetUserByUserName(request.UserName);
            var userResponse = new UserResponse(user.UserId, user.UserName, user.Email, user.DateCreate, user.IsSuspension);
            return new ApiResponse<UserResponse>(response, userResponse, 200);
        }
        else
            return new ApiResponse<UserResponse>(response, null, 500);
    }

    public async Task<ApiResponse<Boolean>> DeleteUserAsync(string userId)
    {
        var response = await _repository.DeleteUserAsync(userId);
        if (response.Equals(Common.Message.MESSAGE_USER_DELETE_FAIL))
            return new ApiResponse<bool>(response, false, 500);
        else if (response.Equals(Common.Message.VALIDATE_MESSAGE_USER_NOT_EXIST))
            return new ApiResponse<bool>(response, false, 400);
        else if (response.Equals(Common.Message.MESSAGE_USER_DELETE_SUCCESSFUL))
            return new ApiResponse<bool>(response, true, 200);
        else
            return new ApiResponse<bool>("", false, 500);
    }

    public async Task<ApiResponse<List<UserResponse>>> GetAllUserAsync()
    {
        var data = await _repository.GetAllUsersAsync();
        if (data.IsNullOrEmpty())
        {
            return new ApiResponse<List<UserResponse>>(Common.Message.MESSAGE_USER_LIST_EMPTY, null, 203);
        }
        var result = new List<UserResponse>();
        foreach (var item in data)
        {
            result.Add(new UserResponse
            {
                UserId = item.UserId.ToString(),
                UserName = item.UserName,
                Email = item.Email,
                DateCreate = item.DateCreate,
                IsSuspension = item.IsSuspension
            });
        }
        return new ApiResponse<List<UserResponse>>(Common.Message.MESSAGE_USER_GET_SUCCESSFUL, result, 200);
    }

    public Task<ApiResponse<UserResponse>> GetUserById(string userId)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<UserResponse>> UpdateUserAsync(UpdateUserRequest request)
    {
        throw new NotImplementedException();
    }
}
