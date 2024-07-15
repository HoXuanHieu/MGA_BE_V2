using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Entities;
using Models.Request;
using Models.Response;
using Service;
using Web_API.ResponseModel;

namespace Web_API;

[Route("[controller]")]
[ApiController]
public class SecureController : ControllerBase
{
    private readonly ISecureService _secureService;
    public SecureController(ISecureService secureService)
    {
        _secureService = secureService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> Register(UserRegisterRequest request)
    {
        var result = await _secureService.RegisterAsync(request);
        return StatusCode(result.Status, result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var result = await _secureService.LoginAsync(request);    
        return StatusCode(result.Status, result);
    }
}
