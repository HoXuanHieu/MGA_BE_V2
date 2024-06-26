using Models.Response;

namespace Models;

public class LoginResponse
{
    public string Token { get; set; }
    public UserResponse UserResponse { get; set; }
}
