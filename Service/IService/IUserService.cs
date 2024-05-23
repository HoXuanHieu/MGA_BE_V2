using Models;
using Models.Response;

namespace Service;

public interface IUserService
{
    public Task<List<UserResponse>> GetAllUserAsync();
    public Task<bool> CreateUserAsync(CreateUserRequest request);
}
