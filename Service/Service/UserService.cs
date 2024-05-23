using Models;
using Models.Entities;
using Models.Response;
using Repositories;
using Service.Helper;
using System.Data;

namespace Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateUserAsync(CreateUserRequest request)
    {
        //String password = BcryptHelper.GeneratePassword();
        String password = "123456";
        var data = new UserEntity()
        {
            UserName = request.UserName,
            Email = request.Email,
            Password = BcryptHelper.HashPassword(password),
            Role = request.Role
        };
        var isSuccess = await _repository.CreateUserAsync(data);
        if (isSuccess) {
            //send email to user 
            //show log in text here
            return true;
        }
        return false;
    }

    public async Task<List<UserResponse>> GetAllUserAsync()
    {
        var data = await _repository.GetAllUsersAsync();
        var result = new List<UserResponse>();
        foreach(var item in data)
        {
            result.Add(new UserResponse
            {
                UserId = item.Id,
                UserName = item.UserName,
                Email = item.Email,
                DateCreate = item.DateCreate,
                IsSuspension = item.IsSuspension
            }); 
        }
        return result;
    }
}
