using Models;
using Models.Request;
using Models.Response;
using Web_API.ResponseModel;

namespace Service;

public interface ISecureService
{
    Task<ApiResponse<UserResponse>> RegisterAsync(UserRegisterRequest request);
    Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request);
}
