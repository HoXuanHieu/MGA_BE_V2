using Models;
using Models.Response;
using Web_API.ResponseModel;

namespace Service;

public interface IUserService
{
    public Task<ApiResponse<List<UserResponse>>> GetAllUserAsync();
    public Task<ApiResponse<UserResponse>> CreateUserAsync(CreateUserRequest request);
    public Task<ApiResponse<UserResponse>> UpdateUserAsync(UpdateUserRequest request);
    public Task<ApiResponse<Boolean>> DeleteUserAsync(String userId);
    public Task<ApiResponse<UserResponse>> GetUserById(String userId);
}
